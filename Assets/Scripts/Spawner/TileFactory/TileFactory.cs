using UnityEngine;

public class TileFactory : ITileFactory
{
    public Tile Create(TileSetting setting)
    {
        GameObject tile = Object.Instantiate(setting.TilePrefab);
        Tile tileComponent = tile.GetComponent<Tile>();
        return tileComponent;
    }
}