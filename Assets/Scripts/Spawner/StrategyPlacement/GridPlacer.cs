using UnityEngine;

public class GridPlacer : MonoBehaviour
{
   public void SetGridPosition(Tile[][] chessBoard, Tile[] tilePrefabs, float offSetPosition)
    {
        for (int i = 0; i < chessBoard.Length; i++)
        {
            for (int j = 0; j < chessBoard.Length; j++)
            {
                tilePrefabs[j + i * chessBoard.Length].transform.position = new Vector3(i * offSetPosition, 0, j * offSetPosition);
                tilePrefabs[j + i * chessBoard.Length].PositionOnGrid.posX = i;
                tilePrefabs[j + i * chessBoard.Length].PositionOnGrid.posZ = j;
                chessBoard[i][j] = tilePrefabs[j + i*chessBoard.Length];
            }
        }
    }
}
