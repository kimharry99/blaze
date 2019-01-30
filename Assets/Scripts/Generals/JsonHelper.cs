using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class JsonHelper : MonoBehaviour
{
	public static FurnitureUpgradeInfo LoadFurnitureUpgradeInfo(FurnitureType type)
	{
		Debug.Log(type.ToString());
;		string json = LoadJson("FurnitureUpgradeInfo/" + type.ToString());
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

}

[Serializable]
public class FurnitureUpgradeInfo
{
	public FurnitureType type = FurnitureType.None;
	public int[] wood = new int[3];
	public int[] components = new int[3];
	public int[] parts = new int[3]; 
}