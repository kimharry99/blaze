using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Item : ScriptableObject
{
	public Texture2D itemImage;
	public int amount;
	public string itemName;
	public void LoadData(string json)
	{
		JsonUtility.FromJsonOverwrite(json, this);
	}
}