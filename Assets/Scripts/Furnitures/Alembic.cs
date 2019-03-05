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

    public void Awake()
    {
        HomeUIManager.inst.alembicAlcoholButton.interactable = false;
    }

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
                if (!GameManager.inst.CheckResource(water: 15)) return;
                GameManager.inst.UseResource(water: 15);
                break;
            case 1:
                if (GameManager.inst.items["Alcohol"].amount <= 0) return;
                GameManager.inst.items["Alcohol"].amount--;
                break;
            default:
                Debug.Log("Alembic Select Fail.");
                return;
        }

        remainedTurn = neededTurn[selectedRecipie * 3 + (level - 1)];
        TurnManager.inst.OnTurnPassed += OnTurnPassed;
        isUsing = true;

        HomeUIManager.inst.alembicUseText.text = "Cancel";
        HomeUIManager.inst.alembicWaterButton.interactable = false;
        HomeUIManager.inst.alembicAlcoholButton.interactable = false;
    }

    public void CancelProduction()
    {
        TurnManager.inst.OnTurnPassed -= OnTurnPassed;
        isUsing = false;
        remainedTurn = 0;
        HomeUIManager.inst.alembicUseText.text = "Do";
        HomeUIManager.inst.alembicWaterButton.interactable = level > 0;
        HomeUIManager.inst.alembicAlcoholButton.interactable = level > 1;
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

                HomeUIManager.inst.alembicUseText.text = "Do";
                HomeUIManager.inst.alembicWaterButton.interactable = level > 0;
                HomeUIManager.inst.alembicAlcoholButton.interactable = level > 1;
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
        if(level >= 1)
        {
            HomeUIManager.inst.alembicWaterButton.interactable = true;
        }
        if(level >= 2)
        {
            HomeUIManager.inst.alembicAlcoholButton.interactable = true;
        }
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