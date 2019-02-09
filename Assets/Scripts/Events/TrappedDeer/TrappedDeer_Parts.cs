using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrappedDeer_Parts : LogEvent
{
	public int turn = 8;
	public int components = 20;
	public int parts = 10;

	public override void EventStart()
	{
		TurnManager.inst.UseTurn(turn);
		UIManager.inst.AddResourceResult(components:components, parts:parts);
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction> { Confirm };
	}

	private void Confirm()
	{
		GameManager.inst.GetResource(components:components, parts:parts);
		EndEvent();
	}

}
