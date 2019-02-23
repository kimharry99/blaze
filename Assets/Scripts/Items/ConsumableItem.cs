using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConsumableItem : Item
{
	public abstract bool IsUsable { get; }
	public abstract void Use(int amount);
}
