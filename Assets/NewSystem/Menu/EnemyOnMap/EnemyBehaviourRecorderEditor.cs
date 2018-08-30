using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyBehaviourRecorder))]
public class EnemyBehaviourRecorderEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EnemyBehaviourRecorder myScript = (EnemyBehaviourRecorder)target;
        if (GUILayout.Button("Record"))
        {
            myScript.StartRecording();
        }

        if (GUILayout.Button("Stop"))
        {
            myScript.StopRecoring();
        }
    }
}
