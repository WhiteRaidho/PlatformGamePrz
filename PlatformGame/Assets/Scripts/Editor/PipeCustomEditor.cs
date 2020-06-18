using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Pipe))]
public class PipeCutomEditor : Editor
{
    Pipe pipe;

    private void OnEnable()
    {
        pipe = (Pipe)target;
    }

    private void OnSceneGUI()
    {
        for(int i = 0; i < pipe.nodes.Count - 1; i++)
        {
            Handles.color = Color.red;
            //Handles.SphereCap(0, pipe.nodes[i], Quaternion.identity, 0.2f);
            Handles.color = Color.cyan;
            Handles.DrawLine(pipe.nodes[i], pipe.nodes[i + 1]);
        }
    }
}
