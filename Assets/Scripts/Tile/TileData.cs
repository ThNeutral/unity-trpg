using UnityEngine;

[CreateAssetMenu(fileName = "TileData", menuName = "Scriptable Objects/Tile Data")]
public class TileData : ScriptableObject
{
    public bool IsWalkable;
    public SpriteData SpriteData;
}