using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : Disease
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
		remainedTurn += 12;
		UIManager.inst.UpdateBuffUI(this);
	}

	public override void Apply(int turn)
	{
		GameManager.inst.healthChangePerTurn -= 1;
		GameManager.inst.sanityChangePerTurn -= 2;
	}

	public override void Cure()
	{
		remainedTurn = 0;
	}

	public override void Init()
	{
		base.Init();
		TurnManager.inst.OnTurnPassed += OnTurnPassed;
	}

	protected override void OnTurnPassed(int turn)
	{
		if (remainedTurn > 0 && remainedTurn - turn <= 0)
		{
			float rand = Random.Range(0, 100);
			if (rand < 50)
			{
				((Disease)GameManager.inst.GetBuff("InfectionRisk")).AddNewDisease();
			}
		}
		base.OnTurnPassed(turn);
	}
}
