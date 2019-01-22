using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class FurnitureUpgradeInfoWindow : EditorWindow
{
	FurnitureType type;
	int level;
	int wood = 0;
	int components = 0;
	int parts = 0;

	// Add menu named "My Window" to the Window menu
	[MenuItem("Editors/가구 업그레이드 정보")]
	static void Init()
	{
		// Get existing open window or if none, make a new one:
		FurnitureUpgradeInfoWindow window = (FurnitureUpgradeInfoWindow)EditorWindow.GetWindow(typeof(FurnitureUpgradeInfoWindow));
		window.name = "가구 업그레이드 정보 관리";
		window.Show();
	}

	void OnGUI()
	{
		GUILayout.Label("인덱싱 정보", EditorStyles.boldLabel);
		type = (FurnitureType)EditorGUILayout.EnumPopup("가구 타입",type);
		level = EditorGUILayout.IntSlider("Level", level, 0, 2);

		GUILayout.Label("내용", EditorStyles.boldLabel);
		wood = EditorGUILayout.IntField("Wood", wood);
		components = EditorGUILayout.IntField("Components", components);
		parts = EditorGUILayout.IntField("Parts", parts);

		if (GUILayout.Button("불러오기"))
		{
			string json = File.ReadAllText(Application.dataPath + "/Resources/Jsons/FurnitureUpgradeInfo/" + type.ToString() + ".json");
			FurnitureUpgradeInfo info = JsonUtility.FromJson<FurnitureUpgradeInfo>(json);
			wood = info.wood[level];
			components = info.components[level];
			parts = info.parts[level];
			AssetDatabase.Refresh();
		}

		if (GUILayout.Button("편집"))
		{
			string json = File.ReadAllText(Application.dataPath + "/Resources/Jsons/FurnitureUpgradeInfo/" + type.ToString() + ".json");
			FurnitureUpgradeInfo info = JsonUtility.FromJson<FurnitureUpgradeInfo>(json);
			info.wood[level] = wood;
			info.components[level] = components;
			info.parts[level] = parts;
			File.WriteAllText(Application.dataPath + "/Resources/Jsons/FurnitureUpgradeInfo/" + type.ToString() + ".json", JsonUtility.ToJson(info, true));
			AssetDatabase.Refresh();
		}
	}
}
