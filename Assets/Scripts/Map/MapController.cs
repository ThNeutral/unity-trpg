using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;

    [SerializeField]
    private Sprite sprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            var mousePos = Input.mousePosition;
            var worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            var tilePos = tilemap.WorldToCell(worldPos);
            if (tilemap.GetTile(tilePos) != null) 
            {
                var newTile = ScriptableObject.CreateInstance<Tile>();
                newTile.sprite = sprite;
                tilemap.SetTile(tilePos, newTile);
            }
        }
    }
}
