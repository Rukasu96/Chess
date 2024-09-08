using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private TurnManager turnManager;

    private List<Tile> availableTiles = new List<Tile>();
    private Chessman currentChessman;
    private IEnPassantable enPassantableChessman;

    private void UpdateMove(Tile destinationTile, Chessman movingChessman)
    {
        if (currentChessman is IEnPassantable && (destinationTile.PositionOnGrid.posX - currentChessman.GetPosition().posX == 2
           || currentChessman.GetPosition().posX - destinationTile.PositionOnGrid.posX == 2))
        {
            enPassantableChessman = currentChessman.GetComponent<IEnPassantable>();
            enPassantableChessman.UpdateEnPassantTileStatus();
        }

        boardManager.UpdateBoard(destinationTile, movingChessman);
        movingChessman.UpdatePositionOnGrid(destinationTile.PositionOnGrid.posX, destinationTile.PositionOnGrid.posZ);
        movingChessman.SetChessmanOnTile(destinationTile);
        boardManager.ChangeBoardTileColor(availableTiles);
        availableTiles.Clear();

        if (currentChessman is IBonusMoveable)
        {
            currentChessman.GetComponent<IBonusMoveable>().UpdateSatusBonusMove();
        }

        if (currentChessman.GetComponent<IEnPassantable>() != enPassantableChessman)
        {
            enPassantableChessman = null;
        }
    }

    private void SetTilesAvailableToMove()
    {
        AddChessmanAvailableTilesToMove();
        boardManager.ChangeBoardTileColor(availableTiles);
    }

    private void AddChessmanAvailableTilesToMove()
    {
        var chessmanPostion = currentChessman.GetPosition();
        var chessmanMovePatterns = currentChessman.ChessmanSettings.MovePatterns;
        var chessmanCombatPatterns = currentChessman.ChessmanSettings.CombatPatterns;

        if (currentChessman.ChessmanSettings.IsMovingFullHorizontalAndVertical && currentChessman.ChessmanSettings.IsMovingFullSidelong)
        {
            AddHorizontalAndVerticalTilesToList(chessmanPostion, chessmanMovePatterns);
            AddSidelongTilesToList(chessmanPostion, chessmanMovePatterns);
        }
        else if (currentChessman.ChessmanSettings.IsMovingFullHorizontalAndVertical)
        {
            AddHorizontalAndVerticalTilesToList(chessmanPostion, chessmanMovePatterns);
        }
        else if (currentChessman.ChessmanSettings.IsMovingFullSidelong)
        {
            AddSidelongTilesToList(chessmanPostion, chessmanMovePatterns);
        }
        else
        {
            AddMovePatternTiles(chessmanPostion, chessmanMovePatterns);
        }

        if (chessmanCombatPatterns.Count > 0)
        {
            AddCombatPatternTiles(chessmanPostion, chessmanCombatPatterns);
        }
    }

    private void AddHorizontalAndVerticalTilesToList(PositionOnGrid currentChessmanPosition, List<PositionOnGrid> chessmanMovePatterns)
    {
        Tile availableTile;
        int modifierX = 1;
        int modifierZ = 0;
        int movePatternValue = chessmanMovePatterns[0].posX;

        for (int i = 0; i < 4; i++)
        {
            for (int x = 0; x < movePatternValue; x++)
            {
                if (i == 0 || i == 2)
                {
                    availableTile = boardManager.ReturnTile(currentChessmanPosition.posX + modifierX, currentChessmanPosition.posZ + modifierZ);
                }
                else
                {
                    availableTile = boardManager.ReturnTile(currentChessmanPosition.posX - modifierX, currentChessmanPosition.posZ - modifierZ);
                }

                if (availableTile == null)
                {
                    break;
                }

                if (availableTile.Chessman != null)
                {
                    if (availableTile.Chessman.GetTeamColor() != turnManager.ActivePlayer)
                    {
                        availableTiles.Add(availableTile);
                        break;
                    }
                    else if (availableTile.Chessman is IKingable && currentChessman is ICastlingable)
                    {
                        if (availableTile.Chessman.GetComponent<IKingable>().CanUseCastling())
                        {
                            availableTiles.Add(availableTile);
                            break;
                        }
                        break;
                    }
                    else
                    {
                        break;
                    }
                }

                if (i >= 2)
                {
                    modifierZ++;
                }
                else
                {
                    modifierX++;
                }

                availableTiles.Add(availableTile);
            }

            if (i == 1)
            {
                modifierX = 0;
                modifierZ = 1;
            }
            else if (i >= 1)
            {
                modifierZ = 1;
                movePatternValue = chessmanMovePatterns[0].posZ;
            }
            else
            {
                modifierX = 1;
            }
        }
    }

    private void AddSidelongTilesToList(PositionOnGrid currentChessmanPosition, List<PositionOnGrid> chessmanMovePatterns)
    {
        Tile availableTile;
        int movePatternValue = currentChessman.ChessmanSettings.MovePatterns[0].posX;

        for (int i = 0; i < 4; i++)
        {
            int modifierX = 0;
            int modifierZ = 0;

            for (int x = 0; x < movePatternValue; x++)
            {
                switch (i)
                {
                    case 0:
                        modifierX++;
                        modifierZ++;
                        break;
                    case 1:
                        modifierX--;
                        modifierZ++;
                        break;
                    case 2:
                        modifierX++;
                        modifierZ--;
                        break;
                    case 3:
                        modifierX--;
                        modifierZ--;
                        break;
                    default:
                        break;
                };

                availableTile = boardManager.ReturnTile(currentChessmanPosition.posX + modifierX, currentChessmanPosition.posZ + modifierZ);

                if (availableTile == null)
                {
                    break;
                }

                if (availableTile.Chessman != null)
                {
                    if (availableTile.Chessman.GetTeamColor() != turnManager.ActivePlayer)
                    {
                        availableTiles.Add(availableTile);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }

                availableTiles.Add(availableTile);
            }
        }
    }

    private void AddMovePatternTiles(PositionOnGrid currentChessmanPosition, List<PositionOnGrid> chessmanMovePatterns)
    {
        Tile availableTile;
        IBonusMoveable bonusMoveable = currentChessman.GetComponent<IBonusMoveable>();

        foreach (var movePattern in chessmanMovePatterns)
        {
            int patternPosX = turnManager.ActivePlayer == TeamColor.white ? movePattern.posX : -movePattern.posX;
            int patternPosZ = turnManager.ActivePlayer == TeamColor.white ? movePattern.posZ : -movePattern.posZ;
            availableTile = boardManager.ReturnTile(currentChessmanPosition.posX + patternPosX, currentChessmanPosition.posZ + patternPosZ);

            if (availableTile != null)
            {
                if (availableTile.Chessman == null || (availableTile.Chessman.GetTeamColor() != turnManager.ActivePlayer && !(currentChessman is Pawn)))
                {
                    availableTiles.Add(availableTile);
                }
            }
        }

        if (bonusMoveable != null && bonusMoveable.HasBonusMove())
        {
            var bonusPattern = bonusMoveable.ReturnBonusMovePattern();
            availableTile = boardManager.ReturnTile(currentChessmanPosition.posX + bonusPattern.posX, currentChessmanPosition.posZ + bonusPattern.posZ);
            if (availableTile.Chessman == null)
            {
                availableTiles.Add(availableTile);
            }
        }
    }

    private void AddCombatPatternTiles(PositionOnGrid currentChessmanPosition, List<PositionOnGrid> chessmanCombatPatterns)
    {
        foreach (var combatPattern in chessmanCombatPatterns)
        {
            int patternPosX = turnManager.ActivePlayer == TeamColor.white ? combatPattern.posX : -combatPattern.posX;
            int patternPosZ = turnManager.ActivePlayer == TeamColor.white ? combatPattern.posZ : -combatPattern.posZ;
            Tile availableTile = boardManager.ReturnTile(currentChessmanPosition.posX + patternPosX, currentChessmanPosition.posZ + patternPosZ);

            if (availableTile != null)
            {
                if (availableTile.Chessman == null && availableTile.IsEnPassantTile && availableTile.TeamColor != turnManager.ActivePlayer)
                {
                    availableTiles.Add(availableTile);
                }
                else if (availableTile.Chessman != null && availableTile.Chessman.GetTeamColor() != turnManager.ActivePlayer)
                {
                    availableTiles.Add(availableTile);
                }
            }
        }
    }

    public void BackToDefault()
    {
        currentChessman = null;
        boardManager.ChangeBoardTileColor(availableTiles);
        availableTiles.Clear();
    }
    
    public void SetCurrentChessman(Chessman selectedChessman)
    {
        if(selectedChessman == null)
        {
            return;
        }

        currentChessman = selectedChessman;
        SetTilesAvailableToMove();
    }

    public void MoveChessman(Tile selectedTile)
    {
        if (currentChessman is ICastlingable && selectedTile.Chessman is IKingable)
        {
            Tile destinationTileRook;

            if (currentChessman.GetPosition().posZ == 0)
            {
                destinationTileRook = boardManager.ReturnTile(selectedTile.PositionOnGrid.posX, selectedTile.PositionOnGrid.posZ - 1);
            }
            else
            {
                destinationTileRook = boardManager.ReturnTile(selectedTile.PositionOnGrid.posX, selectedTile.PositionOnGrid.posZ + 1);
            }
            UpdateMove(destinationTileRook, currentChessman);

            Tile castlingTile = currentChessman.GetComponent<ICastlingable>().ReturnCastlingTile(selectedTile.Chessman);
            currentChessman.GetComponent<ICastlingable>().UpdateSatusCastling();
            selectedTile.Chessman.GetComponent<IKingable>().UpdateCastlingStatus();
            UpdateMove(castlingTile, selectedTile.Chessman);
        }
        else
        {
            UpdateMove(selectedTile, currentChessman);
        }
    }
}
