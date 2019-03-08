using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquor : UncorruptibleFood
{
	public override void Use()
	{
		base.Use();
		GameManager.inst.GetBuff("Drunken").AddRemainedTurn(10);
		UIManager.inst.UpdateBuffUI(GameManager.inst.GetBuff("Drunken"));
	}
}
