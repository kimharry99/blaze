﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item : ScriptableObject
{
	public Texture2D itemImage;
	public int amount;
	public string itemIndexName;
	public string itemName;
	public string description;
	public void LoadData(string json)
	{
		JsonUtility.FromJsonOverwrite(json, this);
	}
	public virtual void Init()
	{
		amount = 0;
	}
}