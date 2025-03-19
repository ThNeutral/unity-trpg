using UnityEngine;

[CreateAssetMenu(fileName = "SpriteData", menuName = "Scriptable Objects/Sprite Data")]
public class SpriteData : ScriptableObject
{
    private static readonly int TILE_SIZE = 100;
    private Sprite sprite;
    public Color Color;
    public float SizeScale = 1f;

    public Sprite GetSprite()
    {
        if (sprite == null)
        {
            var spriteSize = (int)Mathf.Floor(TILE_SIZE * SizeScale);
            var texture = new Texture2D(spriteSize, spriteSize);
            var pixels = new Color[spriteSize * spriteSize];
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = Color;
            }
            texture.SetPixels(pixels);
            texture.Apply();

            sprite = Sprite.Create(texture, new Rect(0, 0, spriteSize, spriteSize), new Vector2(0.5f, 0.5f));
        }
        return sprite;
    }
}