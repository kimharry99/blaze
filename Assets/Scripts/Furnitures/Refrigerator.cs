using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refrigerator : Furniture
{
    public override FurnitureType type { get { return FurnitureType.Refrigerator; } }

    private readonly int[] powerConsumption = { 0, 15, 40, 60 };
    private readonly int[] lostFoodRate = { 20, 10, 5, 2 };

    private bool isTurnOff = false;

    public int PowerConsumption { get { return powerConsumption[isTurnOff ? 0 : Level - 1]; } }
    public int LostFoodRate { get { return lostFoodRate[isTurnOff ? 0 : Level - 1]; } }


    public override void OnUseButtonClicked()
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        TurnManager.inst.OnTurnPassed += OnTurnPassed;
        Level = 1;
        GameManager.inst.furnitures[(int)type] = new infoFurniture(Level, false);
    }

    private void UseFurniture()
    {
        throw new System.NotImplementedException();
    }

    public void OnTurnPassed(int turn)
    {
        if(turn%96 == 0)
        {
            isTurnOff = false;
        }
        else
        {
            isTurnOff = isTurnOff || !GameManager.inst.furnitures[(int)type].isRun;
        }
    }

}
