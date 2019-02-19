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
					Debug.Log("A");
					RemainedTurn = 4;
				}
			}
			Debug.Log(RemainedTurn);
			return RemainedTurn > 0;
		}
	}

	public override void Apply(int turn)
	{
		GameManager.inst.sanityChangePerTurn -= 1;
	}

	public override void Init()
	{
		TurnManager.inst.OnTurnPassed += OnTurnPassed;
	}
}
