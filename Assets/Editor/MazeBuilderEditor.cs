using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MazeBuilderScript))]
public class MazeBuilderEditor : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector ();

		MazeBuilderScript myScript = (MazeBuilderScript)target;
		if (GUILayout.Button ("Build Maze")) {
			myScript.BuildMaze ();
		}

	}

}
