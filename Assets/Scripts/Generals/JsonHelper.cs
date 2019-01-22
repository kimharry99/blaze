using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class JsonHelper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		for (int i = 0; i < 6; ++i)
		{
			FurnitureUpgradeInfo info = new FurnitureUpgradeInfo();
			info.type = (FurnitureType)i;
			info.wood[0] = 0;
			info.wood[1] = 0;
			info.wood[2] = 0;
			info.components[0] = 0;
			info.components[1] = 0;
			info.components[2] = 0;
			info.parts[0] = 0;
			info.parts[1] = 0;
			info.parts[2] = 0;
			File.WriteAllText(Application.dataPath + "/Resources/Jsons/FurnitureUpgradeInfo/" + ((FurnitureType)i).ToString() + ".json", JsonUtility.ToJson(info, true));
		}
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