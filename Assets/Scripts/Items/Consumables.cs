using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumables : Item
{
	public int Amount { get; private set; }

	public override void Init(string itemName, string description)
	{
		base.Init(itemName, description);
		Amount = 0; 
	}

	public override void UseItem(int amount)
	{
		throw new System.NotImplementedException();
	}
}
