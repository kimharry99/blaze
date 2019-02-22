using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fullness : Buff
{
	public override bool IsActivated
	{
		get
		{
			return GameManager.inst.Hunger >= 80;
		}
	}

	public override void Apply(int turn)
	{
		GameManager.inst.turnPenaltyConst -= 0.25f;
	}
}
