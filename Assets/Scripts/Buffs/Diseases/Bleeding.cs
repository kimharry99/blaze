using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleeding : Disease
{
	public override bool IsActivated
	{
		get
		{
			return buffCount > 0;
		}
	}

	public override void AddNewDisease()
	{
		if (buffCount == 0)
			remainedTurn = 4;
		++buffCount;
		UIManager.inst.UpdateBuffUI(this);
	}

	public override void Apply(int turn)
	{
		GameManager.inst.healthChangePerTurn -= buffCount;
	}

	public override void Cure()
	{
		buffCount--;
		if (buffCount == 0)
			remainedTurn = 0;
	}

	public override void Init()
	{
		base.Init();
		TurnManager.inst.OnTurnPassed += OnTurnPassed;
	}

	protected override void OnTurnPassed(int turn)
	{
		remainedTurn = Mathf.Max(remainedTurn - turn, 0);
		if (remainedTurn <= 0)
		{
			buffCount = Mathf.Max(buffCount - 1, 0);
			remainedTurn = 4;
		}
	}
}
