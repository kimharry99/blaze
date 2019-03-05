using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Theif : LogEvent
{
	private int severity;
	private int food, preserved, water, wood, components, parts;
	private int healthLoss;
	public override void EventStart()
	{
		severity = TurnManager.inst.Day;
		float resourceLossRate = Mathf.Min(20, severity) / 100f;
		healthLoss = severity > 20 ? -Mathf.Min(50, severity) : 0;
		food = -Mathf.CeilToInt(GameManager.inst.items["Food"].amount * resourceLossRate);
		preserved = -Mathf.CeilToInt(GameManager.inst.items["Preserved"].amount * resourceLossRate);
		water = -Mathf.CeilToInt(GameManager.inst.items["Water"].amount * resourceLossRate);
		wood = -Mathf.CeilToInt(GameManager.inst.items["Wood"].amount * resourceLossRate);
		components = -Mathf.CeilToInt(GameManager.inst.items["Components"].amount * resourceLossRate);
		parts = -Mathf.CeilToInt(GameManager.inst.items["Parts"].amount * resourceLossRate);
		Debug.Log(severity + " " + food + " " + preserved + " " + water + " " + wood + " " + components + " " + parts);

		UIManager.inst.AddResourceResult(food, preserved, water, wood, components, parts);
		UIManager.inst.AddPlayerStatusResult(health: healthLoss);
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction> { Confirm };
	}
	
	public void Confirm()
	{
		GameManager.inst.GetResource(water, food, preserved, wood, components, parts);
		GameManager.inst.ChangeHealth(healthLoss);
		EndEvent();
	}
}
