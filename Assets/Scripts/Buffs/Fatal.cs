using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fatal : Buff
{
	public override bool IsActivated
	{
		get
		{
			return GameManager.inst.Health <= 25;
		}
	}

	public override void Apply(int turn)
	{
		GameManager.inst.turnPenaltyConst += 0.5f;
	}
}
