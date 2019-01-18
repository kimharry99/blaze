using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : Furniture
{
	protected override FurnitureType type { get { return FurnitureType.Table; } }
	private int option = 0;

	/// <summary>
	/// True, use food.
	/// False, use preserved food
	/// </summary>
	private bool isFood = true;

	public void UseFurniture()
	{
		switch (option)
		{
			case 0:
				if (isFood) {
					if (GameManager.inst.UseResource(food: 10))
					{
						GameManager.inst.Eat(30);
					}
				}
				else {
					if (GameManager.inst.UseResource(preserved: 10))
					{
						GameManager.inst.Eat(30);
					}
				}
				break;
			case 1:
				if (GameManager.inst.UseResource(water: 10))
				{
					GameManager.inst.Drink(30);
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

	public void SetFood(bool food)
	{
		isFood = food;
	}

	public override void OpenFurnitureUI()
	{
		throw new System.NotImplementedException();
	}
}
