using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.Events;

public class JsonHelper : MonoBehaviour
{
	public static FurnitureUpgradeInfo LoadFurnitureUpgradeInfo(Furniture furniture)
	{
;		string json = LoadJson("FurnitureUpgradeInfo/" + furniture.furnitureName);
		return JsonUtility.FromJson<FurnitureUpgradeInfo>(json);
	}

	public static void JsonToFile(string json, string path)
	{
		File.WriteAllText(Application.dataPath + "/Resources/Jsons/" + path, json);
	}

	public static string LoadJson(string path)
	{
		return Resources.Load<TextAsset>("Jsons/" + path).text;
	}

	public static List<string> LoadJsonAll(string path)
	{
		List<string> jsons = new List<string>();
		foreach(var textAsset in Resources.LoadAll<TextAsset>("Jsons/" + path))
		{
			jsons.Add(textAsset.text);
		}
		return jsons;
	}

}

[Serializable]
public class FurnitureUpgradeInfo
{
	public FurnitureType type = FurnitureType.None;
	public int[] wood = new int[3];
	public int[] components = new int[3];
	public int[] parts = new int[3]; 
}