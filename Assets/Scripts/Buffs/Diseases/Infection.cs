using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infection : Disease
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
		remainedTurn = Mathf.Clamp(96, 192, 2 * remainedTurn);
	}

	public override void Apply(int turn)
	{
		GameManager.inst.healthChangePerTurn -= 2;
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
		if (GameManager.inst.Health >= 50)
		{
			base.OnTurnPassed(turn);
		}
	}
}
