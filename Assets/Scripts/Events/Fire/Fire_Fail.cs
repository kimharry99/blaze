using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fire_Fail : LogEvent
{
	public int turn = 4;
	public int health = -20, sanity = -10;
	public override void EventStart()
	{
		TurnManager.inst.UseTurn(turn);
		UIManager.inst.AddPlayerStatusResult(health: health, sanity: sanity);
		UIManager.inst.AddBuffResult("Burn");
	}

	public override List<UnityAction> GetActions()
	{
		return new List<UnityAction> { Confirm };
	}

	public void Confirm()
	{
		GameManager.inst.ChangePlayerStatus(health: health, sanity: sanity);
		((Disease)GameManager.inst.GetBuff("Burn")).AddNewDisease();
		MapManager mm = MapManager.inst;
		mm.structureTilemap.SetTile(mm.curPosition, mm.structureTiles[(int)StructureType.None]);
		EndEvent();
	}
}
