using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapManager))]
public class MapManagerEditor: Editor{
    public override void OnInspectorGUI(){
        DrawDefaultInspector();
        MapManager manager = (MapManager) target;
        if(GUILayout.Button("Generate Map")){
            manager.GenerateMap();
        }
        if(GUILayout.Button("Destroy Map")){
            manager.DestroyMap();
        }
    }
}