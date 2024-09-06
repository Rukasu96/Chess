using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private TeamSettings[] teamsOrder;
    private TeamColor activePlayer;
    private TurnController turnController;
    
    public TeamColor ActivePlayer => activePlayer;

    private void Awake()
    {
        turnController = GetComponent<TurnController>();
        activePlayer = teamsOrder[0].TeamColor;
    }

    public void UpdatePlayer()
    {
        activePlayer = turnController.ChangePlayer(teamsOrder);
        boardManager.RotateBoard();
    }
}
