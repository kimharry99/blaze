using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FurnitureType
{
    Craft,
    Kitchen,
	Bed,
    Door,
    Farm,
	Bucket,
    Generator,
    Refrigerator,
    Bag,
	Table,
    SolarWaterPurifier,
    Alembic,
    Farmenter,
    SolarGenerator,
	None = -1
}

public class FurnitureObject : MonoBehaviour
{
	public GameObject furnitureUI;

	protected virtual void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			OpenFurnitureUI();
			furnitureUI.transform.GetChild(0).gameObject.SetActive(true);
			furnitureUI.transform.GetChild(1).gameObject.SetActive(true);
			int level = GameManager.inst.furnitures[name].Level;
			if (level < 1)
			{
				furnitureUI.transform.GetChild(0).transform.localPosition = new Vector3(0, 0, 0);
				furnitureUI.transform.GetChild(1).gameObject.SetActive(false);
			}
			else if (level > 2)
			{
				furnitureUI.transform.GetChild(0).gameObject.SetActive(false);
				furnitureUI.transform.GetChild(1).transform.localPosition = new Vector3(0, 0, 0);
			}
			else
			{
				furnitureUI.transform.GetChild(0).transform.localPosition = new Vector3(-0.5f, 0, 0);
				furnitureUI.transform.GetChild(1).transform.localPosition = new Vector3(0.5f, 0, 0);
			}
		}
	}

	protected virtual void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			CloseFurnitureUI();
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
}
