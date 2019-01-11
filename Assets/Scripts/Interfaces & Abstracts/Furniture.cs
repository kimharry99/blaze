using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FurnitureType
{
	Table,
	Craft,
	Bed,
	Farm,
	Kitchen,
	Bucket,
	Door
}

public abstract class Furniture : MonoBehaviour
{
	public int Level { get; protected set; }
	public abstract void UseFurniture();
	protected virtual void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			PlayerController.selectedFurniture = this;
			//For Debug
			GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
		}
	}

	protected virtual void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			PlayerController.selectedFurniture = null;
			GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
		}

	}
}
