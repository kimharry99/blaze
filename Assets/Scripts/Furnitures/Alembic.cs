using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alembic : Furniture
{
    public int selectedRecipie = -1; //None

    public int remainedBattery = 0;
    public int useBattery = 0;
    public int[] maxBattery = new int[3];

    private int remainedTurn = 0;
    private bool isUsing = false;
    public int[] neededTurn;

    public void SelectRecipie(int recipie)
    {
        selectedRecipie = recipie;
    }

    public void Use()
    {
        if (isUsing)
        {
            CancelProduction();
            return;
        }

        switch(selectedRecipie)
        {
            case 0:
                Debug.Log("물");
                if (!GameManager.inst.CheckResource(water: 15)) return;
                GameManager.inst.UseResource(water: 15);
                break;
            case 1:
                Debug.Log("알콜");
                if (GameManager.inst.items["Alcohol"].amount <= 0) return;
                GameManager.inst.items["Alcohol"].amount--;
                break;
            default:
                Debug.Log("Alembic Select Fail.");
                return;
        }

        remainedTurn = neededTurn[selectedRecipie * 3 + (Level - 1)];
        isUsing = true;

        HomeUIManager.inst.alembicUseText.text = "작업취소";
    }

    public void CancelProduction()
    {
        TurnManager.inst.OnTurnPassed -= OnTurnPassed;
        isUsing = false;
        remainedTurn = 0;
        HomeUIManager.inst.alembicUseText.text = "증류하기";
    }

    public override void OnTurnPassed(int turn)
    {
        if (!isUsing) return;
        if (remainedBattery > 0)
        {
            remainedTurn -= 1;
            remainedBattery -= 1;
            if (remainedTurn <= 0)
            {
                remainedTurn = 0;

                switch(selectedRecipie)
                {
                    case 0:
                        GameManager.inst.items["Water"].amount++;
                        break;
                    case 1:
                        GameManager.inst.items["Water"].amount++;
                        GameManager.inst.items["Alcohol"].amount++;
                        break;
                    default:
                        Debug.Log("Alembic Item Error.");
                        break;
                }

                HomeUIManager.inst.alembicUseText.text = "증류하기";
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

    public void PlusChargeAmount()
    {
        useBattery = Mathf.Min(useBattery + 1, maxBattery[Level-1] - remainedBattery);
    }

    public void MinusChargeAmount()
    {
        useBattery = Mathf.Max(0, useBattery - 1);
    }

    public void Charge()
    {
        if (remainedBattery >= 600)
            return;
        GameManager.inst.items["Battery"].amount -= 1;
        remainedBattery = Mathf.Min(maxBattery[Level-1], remainedBattery + 200);
        useBattery = 0;
    }
}