using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : Buff
{
	public override bool IsActivated
	{
		get
		{
			return RemainedTurn > 0;
		}
	}

	public override void Apply(int turn)
	{
		GameManager.inst.healthChangePerTurn -= 1;
		GameManager.inst.sanityChangePerTurn -= 2;
	}

	public override void Init()
	{
		TurnManager.inst.OnTurnPassed += OnTurnPassed;
	}

	protected override void OnTurnPassed(int turn)
	{
		if (RemainedTurn > 0 && RemainedTurn - turn <= 0)
		{
			float rand = Random.Range(0, 100);
			if (rand < 50)
			{
				//TODO
			}
		}
		base.OnTurnPassed(turn);
	}
}
