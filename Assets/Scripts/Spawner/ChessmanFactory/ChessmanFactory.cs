using UnityEngine;

public class ChessmanFactory : IChessmanFactory
{
    public Chessman Create(ChessmanSetting setting)
    {
        GameObject chessman = Object.Instantiate(setting.ChessmanPrefab);
        Chessman chessmanComponent = chessman.GetComponent<Chessman>();
        chessmanComponent.Initialize(setting);
        return chessmanComponent;
    }
}