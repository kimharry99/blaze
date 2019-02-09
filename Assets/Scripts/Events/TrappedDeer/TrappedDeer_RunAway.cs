using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrappedDeer_RunAway : LogEvent
{
	public int turn = 8;
	public int health = -20;

	public override void EventStart()
	{
		TurnManager.inst.UseTurn(turn);
		UIManager.inst.AddPlayerStatusResult(health: health);
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction> { Confirm };
	}

	private void Confirm()
	{
		GameManager.inst.HealthDamaged(-health);
		EndEvent();
	}
}
