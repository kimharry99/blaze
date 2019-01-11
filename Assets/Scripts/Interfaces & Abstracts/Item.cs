using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
	public string ItemName { get; private set; }
	public string Description { get; private set; }
	public float Weight { get; private set; }
	public int MaxStorableAmount { get; private set; } //Maximum storable amound per a bag cell

	public abstract void UseItem(int amount);
	public abstract void Discard(int amount);

	public virtual void Init(string itemName, string description)
	{
		ItemName = itemName;
		Description = description;
	}
}
