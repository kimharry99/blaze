using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseResource : Item
{
	public bool CheckAmount(int amount)
	{
		return this.amount >= amount;
	}
	public void Use(int amount)
	{
		this.amount -= amount;
	}
}