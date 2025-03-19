using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "Scriptable Objects/Map Data")]
public class MapData : ScriptableObject
{
    public int Width;
    public int Height;
    public TileData[] Tiles;

    [SerializeField]
    public TileData[] AvailableTiles;

    public TileData GetTile(int x, int y)
    {
        var index = GetIndex(x, y);
        if (index == -1) return null;
        return Tiles[index];
    }
    public int GetIndex(int x, int y)
    {
        return GetIndex(x, y, Width, Height);
    }
    public int GetIndex(int x, int y, int width, int height)
    {
        if (x < 0 || x >= width || y < 0 || y >= height) return -1;
        return x * width + y;
    }
    public (int x, int y) GetCoordinates(int index)
    {
        return GetCoordinates(index, Width, Height);
    }
    public (int x, int y) GetCoordinates(int index, int width, int height)
    {
        if (index < 0 || index >= width * height) return (-1, -1);
        return (index / width, index % width);
    }

    public void GenerateGrid()
    {
        Tiles = new TileData[Width * Height];
        if (AvailableTiles.Length > 0 && AvailableTiles[0] != null)
        {
            for (int i = 0; i < Tiles.Length; i++)
            {
                Tiles[i] = AvailableTiles[0];
            }
        }
    }
}