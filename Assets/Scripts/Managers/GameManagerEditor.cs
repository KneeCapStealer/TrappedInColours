using Unity;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameManager gameManager = (GameManager)target;
        if (GUILayout.Button("Paint Frames 1"))
        {
            gameManager.DebugPaint(0, Color.white);
        }

        if (GUILayout.Button("Paint Frames 2"))
        {
            gameManager.DebugPaint(1, Color.white);
        }
        GUILayout.Space(15);
        if (GUILayout.Button("Clear Frames 1"))
        {
            gameManager.DebugPaint(0, Color.red);
        }

        if (GUILayout.Button("Clear Frames 2"))
        {
            gameManager.DebugPaint(1, Color.red);
        }
    }
}