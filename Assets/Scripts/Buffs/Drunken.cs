using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drunken : Buff
{
	public override bool IsActivated { get { return remainedTurn > 0; } }

	public override void Apply(int turn)
	{
		GameManager.inst.sanityChangePerTurn += 2;
		GameManager.inst.turnPenaltyConst += 0.25f;
	}
	public override void Init()
	{
		base.Init();
		TurnManager.inst.OnTurnPassed += OnTurnPassed;
	}
}
