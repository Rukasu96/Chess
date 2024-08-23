using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoardSettings", menuName = "BoardData")]
public class BoardSettings : ScriptableObject
{
    public Tile tilePrefab;
    public TeamSettings[] teamSettings;
    public int width;
    public int height;
    public float tileOffsetPosition;
    public float BoardPositionX;
    public float BoardPositionZ;
}
