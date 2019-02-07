using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LogEventAssetMaker
{
	[MenuItem("Assets/Create/LogEvent Asset")]
	public static void CreateMyAsset()
	{
		LogEvent asset = ScriptableObject.CreateInstance<LogEvent>();

		AssetDatabase.CreateAsset(asset, "Assets/NewScripableObject.asset");
		AssetDatabase.SaveAssets();

		EditorUtility.FocusProjectWindow();

		Selection.activeObject = asset;
	}
}