﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FurnitureType
{
	Table,
	Craft,
	Bed,
	Farm,
	Kitchen,
	Bucket,
	Door,
	None = -1
}

public abstract class Furniture : MonoBehaviour
{
	protected abstract FurnitureType type { get; }
	public int Level { get; protected set; }
	public GameObject furnitureUI;

	protected virtual void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			PlayerController.selectedFurniture = type;
			//For Debug
			GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
		}
	}

	protected virtual void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			PlayerController.selectedFurniture = FurnitureType.None;
			//For Debug
			GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
		}

	}

	public virtual void OpenFurnitureUI()
	{
		furnitureUI.SetActive(true);
	}

	public virtual void CloseFurnitureUI()
	{
		furnitureUI.SetActive(false);
	}

	public abstract void OnUseButtonClicked();
}
