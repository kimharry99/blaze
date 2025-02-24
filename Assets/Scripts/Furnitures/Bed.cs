﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Furniture
{
    private readonly int[] energyPerTurn = { 4, 5, 6 };
    private readonly int[] cureTurnLeft = { 5, 4, 3 };
    private readonly int[] healTurnLeft = { 999, 10, 6 };

    public int EnergyPerTurn { get { return energyPerTurn[Level - 1]; } }
    public int CureTurnLeft { get { return cureTurnLeft[Level - 1]; } }
    public int HealTurnLeft { get { return healTurnLeft[Level - 1]; } }

    public int usingTurn = 1;

	public override void Init()
	{
		Level = 1;
	}

	public void PlusTurn()
    {
        usingTurn++;
    }

    public void MinusTurn()
    {
        usingTurn = Mathf.Max(1, usingTurn - 1);
    }

    public void UseBed()
    {
		GameManager.inst.isSleeping = true;
        GameManager.inst.StartTask(
		delegate
		{
			GameManager.inst.ChangeEnergy(usingTurn * (EnergyPerTurn));
			if (GameManager.inst.CheckStatus(health: 0, mental: 0, hunger: 0, thirst: 0, energy: 0))
			{
				GameManager.inst.ChangeHealth(usingTurn / CureTurnLeft);
			}
			GameManager.inst.ChangeSanity(usingTurn / HealTurnLeft);
			GameManager.inst.isSleeping = false;
		}
		, usingTurn, true);
    }
}

