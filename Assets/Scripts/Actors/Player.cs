using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
	public Item[] bag = new Item[20];
	public int BagSize
	{
		get
		{
			//TODO : Calculate Bagsize depends on bag level
			return 20;
		}
	}


	private void Start()
	{
		DontDestroyOnLoad(gameObject);
	}

	public override void GetDamaged(int damage)
	{
		base.GetDamaged(damage);
		if (Health <= 0)
		{
			//GameOver Routine
		}
	}
}
