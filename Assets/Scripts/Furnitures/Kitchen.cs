using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kitchen : Furniture
{
    public int selectedRecipie = -1; //None
    public int[] usingResource = new int[4];
    

    public void Awake()
    {
        furnitureName = "Kitchen";
        SetButtons();
    }

    public void SetButtons()
    {
        for (int i = 0; i < 7; i++)
            HomeUIManager.inst.kitchenButtons[i].interactable = false;
    }

    public override void Upgrade()
    {
        base.Upgrade();
        if(level >= 1)
        {
            for (int i = 0; i < 3; i++)
                HomeUIManager.inst.kitchenButtons[i].interactable = true;
        }
        if (level >= 2)
        {
            for (int i = 4; i < 5; i++)
                HomeUIManager.inst.kitchenButtons[i].interactable = true;
        }
        if (level >= 3)
        {
            for (int i = 5; i < 7; i++)
                HomeUIManager.inst.kitchenButtons[i].interactable = true;
        }
    }

    public void SelectRecipie(int recipie)
    {
        selectedRecipie = recipie;
        for (int i = 0; i < 4; i++)
            usingResource[i] = 0;
        switch (selectedRecipie)
        {
            case -1://초기화
                break;
            case 0: //조약한 식사
                usingResource[0] = 30;
                usingResource[2] = 5;
                break;
            case 1: //시원한 물
                usingResource[2] = 20;
                break;
            case 2: //보존음식 
                usingResource[0] = 10;
                usingResource[3] = 1;
                break;
            case 3: //평범한 식사
                usingResource[0] = 50;
                usingResource[2] = 10;
                break;
            case 4: //호화로운 식사
                usingResource[0] = 70;
                usingResource[2] = 20;
                break;
            case 5: //보양식
                usingResource[0] = 70;
                usingResource[2] = 40;
                break;
            case 6: //달콤한 간식
                usingResource[0] = 50;
                usingResource[1] = 30;
                break;
            default:
                Debug.Log("There is no Option for " + selectedRecipie.ToString());
                break;

        }
    }

    public void CookFood()
    {
		SoundManager.inst.PlaySFX(furnitureSFX);
        GameManager.inst.UseResource(water: usingResource[2], food: usingResource[0], preserved: usingResource[1], components: usingResource[3]);
        switch (selectedRecipie)
        {
            case -1://None
                break;
            case 0: //조약한 식사
                TurnManager.inst.UseTurn(3);
                GameManager.inst.ChangeHunger(30);
                GameManager.inst.ChangeSanity(-5);
                break;
            case 1: //시원한 물
                TurnManager.inst.UseTurn(1);
                GameManager.inst.ChangeThirst(30);
                break;
            case 2: //보존음식
                TurnManager.inst.UseTurn(1);
                GameManager.inst.GetResource(preserved: 10);
                break;

            case 3: //평범한 식사
                TurnManager.inst.UseTurn(3);
                GameManager.inst.ChangeHunger(40);
                break;
            case 4: //호화로운 식사
                TurnManager.inst.UseTurn(3);
                GameManager.inst.ChangeHunger(40);
                GameManager.inst.ChangeSanity(10);
                break;
            case 5: //보양식
                TurnManager.inst.UseTurn(3);
                GameManager.inst.ChangeHunger(40);
                GameManager.inst.ChangeHealth(10);
                GameManager.inst.ChangeSanity(5);
                break;
            case 6: //달콤한 간식
                GameManager.inst.ChangeHunger(20);
                GameManager.inst.ChangeSanity(20);
                break;
            default:
                Debug.Log("There is no Option for " + selectedRecipie.ToString());
                break;
        }
    }
}
