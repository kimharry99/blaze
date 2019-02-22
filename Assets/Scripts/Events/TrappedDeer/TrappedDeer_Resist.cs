using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrappedDeer_Resist : LogEvent
{
	public int turn = 4;
	public int food = 30;
	public int health = -10;
	public int energy = -10;
	public int sanity = -10;

	public override void EventStart()
	{
		TurnManager.inst.UseTurn(turn);
		UIManager.inst.AddResourceResult(food: food);
		UIManager.inst.AddPlayerStatusResult(health: health, energy: energy, sanity:sanity);
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction> { Confirm };
	}

	private void Confirm()
	{
		GameManager.inst.GetResource(food: food);
		GameManager.inst.ChangeHealth(health);
		GameManager.inst.ChangeSanity(sanity);
		GameManager.inst.ChangeEnergy(energy);
		EndEvent();
	}
}
