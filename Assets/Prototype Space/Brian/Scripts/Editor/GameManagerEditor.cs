using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor: Editor{
    public override void OnInspectorGUI(){
        DrawDefaultInspector();
        GameManager manager = (GameManager) target;
        if(GUILayout.Button("StartGame")){
            manager.StartGame();
        }
        if(GUILayout.Button("EndGame")){
            manager.StartGame();
        }
    }
}