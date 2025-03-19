using UnityEngine;
using UnityEditor;
using System.Linq;
using System;

[CustomEditor(typeof(MapData))]
public class MapDataEditor : Editor
{
    private int selectedTileIndex;
    private int previousWidth;
    private int previousHeight;
    private bool isFirstRun = true;
    public override void OnInspectorGUI()
    {
        var map = (MapData)target;

        if (isFirstRun)
        {
            previousHeight = map.Height;
            previousWidth = map.Width;
            isFirstRun = false;
            map.Tiles ??= new TileData[0];
            map.AvailableTiles ??= new TileData[0];
        }

        map.Width = EditorGUILayout.IntField("Width", map.Width);
        map.Height = EditorGUILayout.IntField("Height", map.Height);

        EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(map.AvailableTiles)), true);
        serializedObject.ApplyModifiedProperties();

        if (map.AvailableTiles != null && map.AvailableTiles.Length > 0)
        {
            selectedTileIndex = EditorGUILayout.Popup(
                "Available Tiles", 
                selectedTileIndex, 
                map.AvailableTiles.Select((tile) => tile.name).ToArray()
            );
        }
        else
        {
            EditorGUILayout.HelpBox("Available Tiles list must be non empty", MessageType.Warning);
            return;
        }

        if (GUILayout.Button("Generate Grid"))
        {
            previousWidth = map.Width;
            previousHeight = map.Height;
            map.GenerateGrid();
            EditorUtility.SetDirty(map);
        }

        if (map.Tiles == null) return;


        EditorGUILayout.BeginHorizontal();
        for (int tileIndex = 0; tileIndex < map.Tiles.Length; tileIndex++)
        {
            var (x, y) = map.GetCoordinates(tileIndex, previousWidth, previousHeight);
            if (x == -1 || y == -1) continue;
            if (y == 0) EditorGUILayout.BeginVertical();

            var index = Array.IndexOf(map.AvailableTiles, map.Tiles[tileIndex]);
            if (GUILayout.Button(index.ToString(), GUILayout.Width(30), GUILayout.Height(30)))
            {
                map.Tiles[tileIndex] = map.AvailableTiles[selectedTileIndex];
                EditorUtility.SetDirty(map);
            }

            if (y == previousHeight - 1) EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();
    }
}