using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    [SerializeField]
    private MapData mapData;

    [SerializeField]
    private Tilemap terrainTilemap;

    [SerializeField] 
    private SpriteData highlightSprite;

    [SerializeField] 
    private SpriteData characterSprite;

    [SerializeField]
    private bool buildTilemapOnStart = true;

    private MapRenderer mapRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mapRenderer = FindFirstObjectByType<MapRenderer>();
        if (buildTilemapOnStart)
        {
            BuildMap();
        }
        
    }

    [ContextMenu("Build Map")]
    public void BuildMap()
    {
        mapRenderer.BuildTilemap(mapData);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            var mousePos = Input.mousePosition;
            var worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            var tilePos = terrainTilemap.WorldToCell(worldPos);
            mapRenderer.HighlightPath(highlightSprite, new Vector3Int[] {tilePos});
        } 
        if (Input.GetKey(KeyCode.Mouse1))
        {
            var mousePos = Input.mousePosition;
            var worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            var tilePos = terrainTilemap.WorldToCell(worldPos);
            mapRenderer.RenderCharacter(characterSprite, tilePos);

        }
    }
}
