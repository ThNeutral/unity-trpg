using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Tilemaps;

public class MapRenderer : MonoBehaviour
{
    [SerializeField]
    private Tilemap terrainTilemap;

    [SerializeField] 
    private Tilemap charactersTilemap;

    [SerializeField]
    private Tilemap effectsTilemap;

    public void BuildTilemap(MapData mapData)
    {
        terrainTilemap.ClearAllTiles();

        if (mapData == null) return;

        for (int y = 0; y < mapData.Height; y++)
        {
            for (int x = 0; x < mapData.Width; x++)
            {
                TileData tileData = mapData.GetTile(x, y);
                if (tileData != null)
                {
                    Tile tile = ScriptableObject.CreateInstance<Tile>();
                    tile.sprite = tileData.SpriteData.GetSprite();
                    terrainTilemap.SetTile(new Vector3Int(x, -y, 0), tile);
                }
            }
        }
    }

    public void RenderCharacter(SpriteData characterSprite, Vector3Int pos)
    {
        charactersTilemap.ClearAllTiles();
        if (terrainTilemap.HasTile(pos))
        {
            var tile = ScriptableObject.CreateInstance<Tile>();
            tile.sprite = characterSprite.GetSprite();
            charactersTilemap.SetTile(pos, tile);
        }
    }

    public void HighlightPath(SpriteData highlightedSprite, IEnumerable<Vector3Int> tilesPos)
    {
        effectsTilemap.ClearAllTiles();
        foreach (var tilePos in tilesPos)
        {
            if (terrainTilemap.HasTile(tilePos))
            {
                var tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = highlightedSprite.GetSprite();
                effectsTilemap.SetTile(tilePos, tile);
            }
        }
    }
}
