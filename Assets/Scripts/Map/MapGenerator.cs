using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private MapData mapData;

    [SerializeField]
    private Tilemap tilemap;

    [ContextMenu("Build Tilemap")]
    public void BuildTilemap()
    {
        tilemap.ClearAllTiles();

        if (mapData == null) return;

        for (int y = 0; y < mapData.height; y++)
        {
            for (int x = 0; x < mapData.width; x++)
            {
                TileData tileData = mapData.GetTile(x, y);
                if (tileData != null)
                {
                    Tile tile = ScriptableObject.CreateInstance<Tile>();
                    tile.sprite = tileData.sprite;
                    tilemap.SetTile(new Vector3Int(x, -y, 0), tile);
                }
            }
        }
    }
}
