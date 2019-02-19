using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dehydration : Buff
{
	public override bool IsActivated
	{
		get
		{
			return GameManager.inst.Thirst <= 0;
		}
	}

	public override void Apply(int turn)
	{
		GameManager.inst.healthChangePerTurn -= 4;
		GameManager.inst.sanityChangePerTurn -= 1;
	}
}
