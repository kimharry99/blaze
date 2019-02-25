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
	None = -1
}

public abstract class FurnitureObject : MonoBehaviour
{
	public abstract FurnitureType type { get; }
	public int Level { get; protected set; }
    public bool IsRun { get; set; }
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

	public virtual void Upgrade()
	{
		++Level;
        setInfo();
	}

    public void setInfo()
    {
        //GameManager.inst.furnitures[(int)type].level = Level;
        //GameManager.inst.furnitures[(int)type].isRun = IsRun;
    }
}
