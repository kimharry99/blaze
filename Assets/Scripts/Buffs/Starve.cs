using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starve : Buff
{
	public override bool IsActivated
	{
		get
		{
			return GameManager.inst.Hunger <= 0;
		}
	}

	public override void Apply(int turn)
	{
		GameManager.inst.healthChangePerTurn -= 1;
		GameManager.inst.sanityChangePerTurn -= 2;
	}
}
