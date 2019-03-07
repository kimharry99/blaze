using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wet : Buff
{
	public override bool IsActivated
	{
		get
		{
			if (GameManager.inst.IsOutside)
			{
				if (TurnManager.inst.Weather == Weather.Rain && MapManager.inst.tileInfos[MapManager.inst.curPosition].structureType == StructureType.None)
				{
					remainedTurn = 4;
				}
			}
			return remainedTurn > 0;
		}
	}

	public override void Apply(int turn)
	{
		GameManager.inst.sanityChangePerTurn -= 1;
	}

	public override void Init()
	{
		base.Init();
		TurnManager.inst.OnTurnPassed += OnTurnPassed;
	}

	protected override void OnTurnPassed(int turn)
	{
		if (IsActivated)
		{
			float rand = Random.Range(0f, 100f);
			if (rand < 3)
			{
				((Disease)GameManager.inst.GetBuff("Infection")).AddNewDisease();
			}
		}
		base.OnTurnPassed(turn);
	}
}
