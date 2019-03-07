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
		string json = LoadJson("FurnitureUpgradeInfo/" + furniture.furnitureName);
		return JsonUtility.FromJson<FurnitureUpgradeInfo>(json);
	}

	public static void JsonToFile(string json, string path)
	{
#if UNITY_EDITOR
		path = Application.dataPath + "/Resources/Jsons/" + path;
		File.WriteAllText(path, json);
		if (Application.isEditor)
			return;
#endif
#if UNITY_STANDALONE
		string[] pathSubstrings = path.Split('/');
		path = pathSubstrings[pathSubstrings.Length - 1];
		path = Application.persistentDataPath + path;
		using (FileStream fs = new FileStream(path, FileMode.Create))
		{
			using (StreamWriter writer = new StreamWriter(fs))
			{
				writer.Write(json);
			}
		}
#endif
	}

	public static string LoadJson(string path)
	{
		return Resources.Load<TextAsset>("Jsons/" + path).text;
	}

	public static string LoadSavedJson(string path)
	{
		string[] pathSubstrings = path.Split('/');
		path = pathSubstrings[pathSubstrings.Length - 1];
		using (StreamReader r = new StreamReader(Application.persistentDataPath + path))
		{
			return r.ReadToEnd();
		}
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