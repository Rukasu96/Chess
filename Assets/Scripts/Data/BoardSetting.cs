using UnityEngine;

[CreateAssetMenu(fileName = "BoardSettings", menuName = "BoardData")]
public class BoardSetting : ScriptableObject
{
    public Tile TilePrefab;
    public TeamSettings[] TeamSettings;
    public int Width;
    public int Height;
    public float TileOffsetPosition;
    public float BoardPositionX;
    public float BoardPositionZ;
}