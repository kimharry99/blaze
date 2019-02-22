using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrappedDeer_Reality : LogEvent
{
	public int turn = 4;
	public int food = 30;
	public int sanity = -5;

	public override void EventStart()
	{
		TurnManager.inst.UseTurn(turn);
		UIManager.inst.AddResourceResult(food: food);
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction> { Confirm };
	}

	private void Confirm()
	{
		GameManager.inst.GetResource(food: food);
		GameManager.inst.ChangeSanity(sanity);
		EndEvent();
	}
}
