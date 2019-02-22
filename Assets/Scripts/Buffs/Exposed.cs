using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exposed : Buff
{
	public override bool IsActivated
	{
		get
		{
			return TurnManager.inst.DayNight != DayNight.Night &&
				TurnManager.inst.Weather == Weather.Sun &&
				GameManager.inst.IsOutside;
		}
	}

	public override void Apply(int turn)
	{
		GameManager.inst.healthChangePerTurn -= 1;
		float rand = Random.Range(0, 100);
		if (rand < 2)
		{
			((Disease)GameManager.inst.GetBuff("Burn")).AddNewDisease();
		}
	}
}
