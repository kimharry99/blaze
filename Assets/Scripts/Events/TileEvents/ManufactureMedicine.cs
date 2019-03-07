using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ManufactureMedicine : LogEvent
{
	public int minBandage = 1, maxBandange = 5;
	public int minAntibiotics = 1, maxAntibiotics = 3;
	public int minAntiseptic = 1, maxAntiseptic = 3;
	public int minPainkiller = 3, maxPainKiller = 5;
	private int bandage, antibiotics, antiseptic, painkiller;

	public override void EventStart()
	{
		bandage = Random.Range(minBandage, maxBandange);
		antibiotics = Random.Range(minAntibiotics, maxAntibiotics);
		antiseptic = Random.Range(minAntiseptic, maxAntiseptic);
		painkiller = Random.Range(minPainkiller, maxPainKiller);
		UIManager.inst.AddItemResult("Bandage", bandage);
		UIManager.inst.AddItemResult("Antibiotics", antibiotics);
		UIManager.inst.AddItemResult("Antiseptic", antiseptic);
		UIManager.inst.AddItemResult("Painkiller", painkiller);
		UIManager.inst.AddPlayerStatusResult(energy: -20);
		TurnManager.inst.UseTurn(4);
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction> { Confirm };
	}

	private void Confirm()
	{
		Dictionary<string, Item> items = GameManager.inst.items;
		items["Bandage"].amount += bandage;
		items["Antibiotics"].amount += antibiotics;
		items["Antiseptic"].amount += antiseptic;
		items["Painkiller"].amount += painkiller;
		GameManager.inst.ChangeEnergy(-20);
		MapManager.inst.GetCurrentTileInfo().isHarvested = true;
		OutdoorUIManager.inst.UpdateTileInfoPanel();
		EndEvent();
	}
}
