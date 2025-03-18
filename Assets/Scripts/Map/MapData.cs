using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "Scriptable Objects/Map Data")]
public class MapData : ScriptableObject
{
    public int width;
    public int height;
    public TileData[] tiles;

    [SerializeField]
    public TileData[] availableTiles;

    public TileData GetTile(int x, int y)
    {
        var index = GetIndex(x, y);
        if (index == -1) return null;
        return tiles[index];
    }
    public int GetIndex(int x, int y)
    {
        return GetIndex(x, y, width, height);
    }
    public int GetIndex(int x, int y, int width, int height)
    {
        if (x < 0 || x >= width || y < 0 || y >= height) return -1;
        return x * width + y;
    }
    public (int x, int y) GetCoordinates(int index)
    {
        return GetCoordinates(index, width, height);
    }
    public (int x, int y) GetCoordinates(int index, int width, int height)
    {
        if (index < 0 || index >= width * height) return (-1, -1);
        return (index / width, index % width);
    }

    public void GenerateGrid()
    {
        tiles = new TileData[width * height];
        if (availableTiles.Length > 0 && availableTiles[0] != null)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = availableTiles[0];
            }
        }
    }
}