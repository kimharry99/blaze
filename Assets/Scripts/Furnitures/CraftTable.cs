using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftTable : Furniture
{
	public override FurnitureType type { get { return FurnitureType.Craft; } }

	public override void OnUseButtonClicked()
	{
		throw new System.NotImplementedException();
	}

	// Start is called before the first frame update
	void Start()
    {
        Level = 1;
        GameManager.inst.furnitures[(int)type] = new infoFurniture(Level, false);
    }
}
