using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materials : Item
{
	public int Amount { get; private set; }

	public override void Init(string itemName, string description)
	{
		base.Init(itemName, description);
		Amount = 0;
	}

	public override void UseItem(int amount)
	{
		Amount -= amount;
	}

	public override void Discard(int amount)
	{
		throw new System.NotImplementedException();
	}
}
