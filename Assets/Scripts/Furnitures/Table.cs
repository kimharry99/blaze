using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : Furniture
{
	public override FurnitureType type { get { return FurnitureType.Table; } }
	private int option = 0;

	public void UseFurniture()
	{
		switch (option)
		{
			case 0:
				if (GameManager.inst.CheckResource(food: 10))
				{
					GameManager.inst.UseResource(food: 10);
					GameManager.inst.ChangeHunger(30);
				}
				break;
			case 1:
				if (GameManager.inst.CheckResource(preserved: 10))
				{
					GameManager.inst.UseResource(preserved: 10);
					GameManager.inst.ChangeHunger(30);
				}
				break;
			case 2:
				if (GameManager.inst.CheckResource(water: 10))
				{
					GameManager.inst.UseResource(water: 10);
					GameManager.inst.ChangeThirst(30);
				}
				break;
			default:
				Debug.Log("There is no Option for " + option.ToString());
				break;
		}
	}

	public void SetOption(int opt)
	{
		option = opt;
	}

	public override void OpenFurnitureUI()
	{
		throw new System.NotImplementedException();
	}

	public override void OnUseButtonClicked()
	{
		throw new System.NotImplementedException();
	}
}
