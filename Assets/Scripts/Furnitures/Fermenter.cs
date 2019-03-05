using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fermenter : Furniture
{
    private int remainedTurn = 0;
    private bool isUsing = false;
    public int[] neededTurn;

    public int[] maxBattery = new int[3];
    public int remainedBattery = 0;
    public int useBattery = 0;

    public void Use()
    {
        if(isUsing)
        {
            CancelProduction();
            return;
        }

        if (!GameManager.inst.CheckResource(water: 20, food: 20)) return;

        GameManager.inst.UseResource(water: 20, food: 20);
        remainedTurn = neededTurn[level];
        TurnManager.inst.OnTurnPassed += OnTurnPassed;
        isUsing = true;

        HomeUIManager.inst.fermenterUseText.text = "Cancel";
    }

    public override void OnTurnPassed(int turn)
    {
        if (remainedBattery > 0)
        {
            remainedTurn -= turn;
            remainedBattery -= 1;
            if (remainedTurn <= 0)
            {
                remainedTurn = 0;
                TurnManager.inst.OnTurnPassed -= OnTurnPassed;
                GameManager.inst.items["Alcohol"].amount += 2;
                HomeUIManager.inst.fermenterUseText.text = "Do";
            }
        }
        else
        {
            
        }
    }

    public override void Upgrade()
    {
        if (isUsing) return;
        base.Upgrade();
    }

    public void CancelProduction()
    {
        TurnManager.inst.OnTurnPassed -= OnTurnPassed;
        isUsing = false;
        remainedTurn = 0;
        HomeUIManager.inst.fermenterUseText.text = "Do";
    }

    public void PlusChargeAmount()
    {
        useBattery = Mathf.Min(useBattery + 1, maxBattery[level] - remainedBattery);
    }

    public void MinusChargeAmount()
    {
        useBattery = Mathf.Max(0, useBattery - 1);
    }

    public void Charge()
    {
        GameManager.inst.items["Battery"].amount -= useBattery;
        remainedBattery += useBattery;
        useBattery = 0;
    }
}