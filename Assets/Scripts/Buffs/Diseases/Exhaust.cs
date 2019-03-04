using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exhaust : Buff
{
	public override bool IsActivated
	{
		get { return GameManager.inst.Energy <= 0; }
	}

	public override void Apply(int turn)
	{
		GameManager.inst.turnPenaltyConst += 0.5f;
	}
}
