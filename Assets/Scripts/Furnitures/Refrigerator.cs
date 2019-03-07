using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refrigerator : Furniture
{
    private readonly int[] powerConsumption = { 0, 1, 2, 2 };
    private readonly int[] lostFoodRate = { 40, 25, 15, 5 };
    private readonly int[] maxCapacity = { 0, 200, 500, 800 };

    public bool isTurnOff = false;
    public int power;

    public int PowerConsumption { get { return powerConsumption[isTurnOff ? 0 : Level]; } }
    public int LostFoodRate { get { return lostFoodRate[isTurnOff ? 0 : Level]; } }
    public int MaxCapacity { get { return maxCapacity[isTurnOff ? 0 : Level]; } }

    
    public override void Init()
    {
        TurnManager.inst.OnTurnPassed += OnTurnPassed;
		Level = 0;
    }

    public override void OnTurnPassed(int turn)
    {
        if (TurnManager.inst.Turn == 0)
        {
            isTurnOff = false;
        }
        power = Mathf.Max(0, power - PowerConsumption);
        if (power == 0)
        {
            isTurnOff = true;
        }
    }


    public void UseBattery()
    {
        GameManager.inst.items["Battery"].amount--;
        power = Mathf.Min(MaxCapacity, power + 200);
    }
}
