using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
	public int MaxHealth { get; protected set; }
    public int Health { get; protected set; }
	public virtual int Attack { get; protected set; }
	public virtual int Defend { get; protected set; }

	public virtual void GetDamaged(int damage)
	{
		Health -= Mathf.Max(1, damage - Defend);
	}
}
