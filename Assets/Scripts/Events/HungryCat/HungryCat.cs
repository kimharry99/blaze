using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HungryCat : LogEvent
{
	public int food = 20, preserved = 10, water = 10;

	public override void EventStart()
	{
		
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction>
		{
			HungryCat_Food,
			HungryCat_Preserved,
			HungryCat_Water,
			HungryCat_Ignore
		};
	}

	private void HungryCat_Food()
	{
		if (!GameManager.inst.CheckResource(food: food))
			return;
		TurnManager.inst.UseTurn(1);
		GameManager.inst.UseResource(food: food);
		NextEvent("HungryCat_Happy");
	}
	
	private void HungryCat_Preserved()
	{
		if (!GameManager.inst.CheckResource(preserved: preserved))
			return;
		TurnManager.inst.UseTurn(2);
		GameManager.inst.UseResource(preserved: preserved);
		NextEvent("HungryCat_Happy");
	}

	private void HungryCat_Water()
	{
		if (!GameManager.inst.CheckResource(water: water))
			return;
		TurnManager.inst.UseTurn(1);
		GameManager.inst.UseResource(water: water);
		NextEvent("HungryCat_Happy");
	}

	private void HungryCat_Ignore()
	{
		float rand = Random.Range(0, 100);
		if (rand < 90)
			NextEvent("HungryCat_Sad");
		else
			NextEvent("HungryCat_Dead");
	}
}
