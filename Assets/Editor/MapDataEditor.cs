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
            previousHeight = map.height;
            previousWidth = map.width;
            isFirstRun = false;
        }

        map.width = EditorGUILayout.IntField("Width", map.width);
        map.height = EditorGUILayout.IntField("Height", map.height);

        EditorGUILayout.PropertyField(serializedObject.FindProperty("availableTiles"), true);
        serializedObject.ApplyModifiedProperties();

        if (map.availableTiles != null && map.availableTiles.Length > 0)
        {
            selectedTileIndex = EditorGUILayout.Popup(
                "Available Tiles", 
                selectedTileIndex, 
                map.availableTiles.Select((tile) => tile.name).ToArray()
            );
        }
        else
        {
            EditorGUILayout.HelpBox("Available Tiles list must be non empty", MessageType.Warning);
            return;
        }

        if (GUILayout.Button("Generate Grid"))
        {
            previousWidth = map.width;
            previousHeight = map.height;
            map.GenerateGrid();
            EditorUtility.SetDirty(map);
        }

        if (map.tiles == null) return;


        EditorGUILayout.BeginHorizontal();
        for (int tileIndex = 0; tileIndex < map.tiles.Length; tileIndex++)
        {
            var (x, y) = map.GetCoordinates(tileIndex, previousWidth, previousHeight);
            if (x == -1 || y == -1) continue;
            if (y == 0) EditorGUILayout.BeginVertical();

            var index = Array.IndexOf(map.availableTiles, map.tiles[tileIndex]);
            if (GUILayout.Button(index.ToString(), GUILayout.Width(30), GUILayout.Height(30)))
            {
                map.tiles[tileIndex] = map.availableTiles[selectedTileIndex];
                EditorUtility.SetDirty(map);
            }

            if (y == previousHeight - 1) EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();
    }
}