using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFurniture : Furniture
{
	protected override FurnitureType type{ get { return FurnitureType.Bed; } }

	public override void OnUseButtonClicked()
	{
		GameManager.inst.StartTask(UseFurniture, 10);
	}

	private void UseFurniture()
	{
		GameManager.inst.GetResource(water: 10);
	}
}
