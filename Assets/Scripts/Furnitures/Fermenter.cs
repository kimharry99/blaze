using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fermenter : Furniture
{
    private int remainedTurn = 0;
    private bool isUsing = false;
    public int[] neededTurn;

    public int[] maxBattery = new int[3];
    public int remainedPower = 0;

    public void Use()
    {
        if(isUsing)
        {
            CancelProduction();
            return;
        }

        if (!GameManager.inst.CheckResource(water: 20, food: 20)) return;

        GameManager.inst.UseResource(water: 20, food: 20);
        remainedTurn = neededTurn[Level-1];
        TurnManager.inst.OnTurnPassed += OnTurnPassed;
        isUsing = true;

        HomeUIManager.inst.fermenterUseText.text = "취소하기";
    }

    public override void OnTurnPassed(int turn)
    {
        if (!isUsing) return;
        if (remainedPower > 0)
        {
            remainedTurn -= 1;
            remainedPower -= 1;
            if (remainedTurn <= 0)
            {
                remainedTurn = 0;
                GameManager.inst.items["Alcohol"].amount += 2;
                HomeUIManager.inst.fermenterUseText.text = "발효하기";
                isUsing = false;
                return;
            }
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
        HomeUIManager.inst.fermenterUseText.text = "취소하기";
    }

    public void Charge()
    {
        GameManager.inst.items["Battery"].amount --;
        remainedPower = Mathf.Min(600, remainedPower + 200);
    }
}