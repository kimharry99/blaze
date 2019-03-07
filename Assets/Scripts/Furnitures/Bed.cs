using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Furniture
{
    private readonly int[] energyPerTurn = { 4, 5, 6 };
    private readonly int[] cureTurnLeft = { 5, 4, 3 };
    private readonly int[] healTurnLeft = { 999, 10, 6 };

    public int EnergyPerTurn { get { return energyPerTurn[level - 1]; } }
    public int CureTurnLeft { get { return cureTurnLeft[level - 1]; } }
    public int HealTurnLeft { get { return healTurnLeft[level - 1]; } }

    public int usingTurn = 1;

	public override void Init()
	{
		level = 1;
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
        GameManager.inst.StartTask(null, usingTurn);
        GameManager.inst.ChangeEnergy(usingTurn * (EnergyPerTurn));
        if (GameManager.inst.CheckStatus(health: 0, mental: 0, hunger: 0, thirst: 0, energy: 0))
        {
            GameManager.inst.ChangeHealth(usingTurn / CureTurnLeft);
        }
        GameManager.inst.ChangeSanity(usingTurn / HealTurnLeft);
    }


}

