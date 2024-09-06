public class Rook : Chessman, ICastlingable
{
    private bool canUseCastling = true;

    public bool CanUseCastling()
    {
        return canUseCastling;
    }

    public void UpdateSatusCastling()
    {
        canUseCastling = false;
    }

    public Tile ReturnCastlingTile(Chessman selectedChessman)
    {
        IKingable selectedKing = selectedChessman.GetComponent<IKingable>();

        if(selectedKing == null)
        {
            return null;
        }

        if(selectedKing.CanUseCastling())
        {
            if (position.posZ > selectedChessman.GetPosition().posZ)
            {
                return boardManager.ReturnTile(position.posX, position.posZ + 1);
            }
            else
            {
                return boardManager.ReturnTile(position.posX, position.posZ - 1);
            }
        }

        return null;
    }
}
