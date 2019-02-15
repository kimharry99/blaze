using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssetMakerWindow : EditorWindow
{
	MonoScript script;
	string assetName;

	[MenuItem("Editors/어셋 만들기")]
	static void Init()
	{
		// Get existing open window or if none, make a new one:
		AssetMakerWindow window = (AssetMakerWindow)GetWindow(typeof(AssetMakerWindow));
		window.name = "어셋 만들기";
		window.Show();
	}

	void OnGUI()
	{
		GUILayout.Label("스크립트", EditorStyles.helpBox);
		script = EditorGUILayout.ObjectField(script, typeof(MonoScript), false) as MonoScript;

		GUILayout.Label("어셋 이름", EditorStyles.helpBox);
		assetName = EditorGUILayout.TextField("어셋 이름", assetName);

		if (GUILayout.Button("생성"))
		{
			AssetDatabase.CreateAsset(CreateInstance(script.GetClass()), "Assets/" + assetName + ".asset");
			AssetDatabase.Refresh();
		}
	}
}