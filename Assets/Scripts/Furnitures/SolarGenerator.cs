using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarGenerator : Furniture
{
	public readonly int maxPower = 200;
	private readonly int[] chargePerSec = { 0, 1, 2, 3 };

	[SerializeField]
	private int curPower;
	public int CurPower { get { return curPower; } }

	public bool IsFullCharged { get { return curPower >= maxPower; } }
	public override void Init()
	{
		base.Init();
		curPower = 0;
		TurnManager.inst.OnTurnPassed += OnTurnPassed;
	}

	public override void OnTurnPassed(int turn)
	{
		curPower = Mathf.Min(200, curPower + chargePerSec[Level]);
		base.OnTurnPassed(turn);
	}

	public void Harvest()
	{
		curPower = 0;
		GameManager.inst.items["Battery"].amount++;
	}
}
