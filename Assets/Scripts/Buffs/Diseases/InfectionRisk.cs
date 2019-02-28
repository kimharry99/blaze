using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionRisk : Disease
{
	public override bool IsActivated
	{
		get
		{
			return remainedTurn > 0;
		}
	}

	public override void AddNewDisease()
	{
		if (remainedTurn > 0)
			remainedTurn /= 2;
		else
			remainedTurn = 48;
		UIManager.inst.UpdateBuffUI(this);
	}

	public override void Apply(int turn)
	{
		
	}

	public override void Cure()
	{
		remainedTurn = 0;
	}

	public override void Init()
	{
		TurnManager.inst.OnTurnPassed += OnTurnPassed;
	}

	protected override void OnTurnPassed(int turn)
	{
		if (remainedTurn > 0 && remainedTurn - turn <= 0)
		{
			((Disease)GameManager.inst.GetBuff("Infection")).AddNewDisease();
		}
		base.OnTurnPassed(turn);
	}
}
