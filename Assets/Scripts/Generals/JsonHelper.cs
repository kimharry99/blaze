using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class JsonHelper : MonoBehaviour
{
	public static FurnitureUpgradeInfo LoadFurnitureUpgradeInfo(FurnitureType type)
	{
		string json = File.ReadAllText(Application.dataPath + "/Resources/Jsons/FurnitureUpgradeInfo/" + type.ToString() + ".json");
		return JsonUtility.FromJson<FurnitureUpgradeInfo>(json);
	}

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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