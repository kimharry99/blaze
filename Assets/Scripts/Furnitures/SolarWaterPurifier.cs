using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarWaterPurifier : Furniture 
{
    private readonly int[] requiresTurn = { 0, 200, 150, 100 };

    public int RequiresTurn { get { return requiresTurn[level]; } } 
    public int cleanWater;
    public int turnLeft;

    public bool isUsing = false;

    public override void Init()
    {
        TurnManager.inst.OnTurnPassed += OnTurnPassed;
    }

    public override void OnTurnPassed(int turn)
    {
        if (!(turnLeft > 0))
            return;
        if (TurnManager.inst.Weather == Weather.Sun && TurnManager.inst.Turn >=24&&TurnManager.inst.Turn <72&&isUsing)
        {
            --turnLeft;
        }
        if (turnLeft == 0)
        {
            cleanWater++;
        }
    }

    public void InputWater()
    {
        GameManager.inst.UseResource(water: 10);
        TurnManager.inst.UseTurn(4);
        turnLeft = RequiresTurn;
        isUsing = true;
    }

    public void HarvestCleanWater()
    {
        isUsing = false;
        TurnManager.inst.UseTurn(2);
        GameManager.inst.items["CleanWater"].amount += cleanWater;
        cleanWater = 0;
    }

    public void CancelJob()
    {
        turnLeft = 0;
        isUsing = false;
    }
}
