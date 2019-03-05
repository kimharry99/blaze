using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum FarmState
{
    Idle,
    Grow,
    Harvest
}

enum FarmCrops
{
    Lettuce,
    Carrot,
    Bean,
    Potato,
    None = -1
}
public class Farm : Furniture
{
    public int[] turnLeft = new int[3];
    public int[] turnTaken = new int[3];        //turn turnLeft/turnTaken
    public int[] state = new int[3];
    public int[] crops = new int[3];

    public int selectedSlot = -1;   //no slot selected
    private int option = 0;

    public override void Init()
    {
        TurnManager.inst.OnTurnPassed += OnTurnPassed;
    }

    public override void OnTurnPassed(int turn)
    {
        for (int i = 0; i < level; ++i)
        {
            if (state[i] == (int)FarmState.Idle) { 
                return;
            }
            turnLeft[i] -= turn;
            if (turnLeft[i] <= 0)
            {
                state[i] = (int)FarmState.Harvest;
            }
            if (turnLeft[i] <= -192)
            {
            state[i] = (int)FarmState.Grow;
            turnLeft[i] = turnTaken[i];
            }
        }
    }

    public void PlantCrops(int crop)
    {
		SoundManager.inst.PlaySFX(furnitureSFX);
        switch (crop)
        {
            case 0: //상추
                if (!GameManager.inst.CheckResource(water: 20, food: 5))
                    return;
                if (level < 1)
                    return;
                GameManager.inst.UseResource(water: 20, food: 5);
                turnTaken[selectedSlot] = Random.Range(10, 16);
                turnLeft[selectedSlot] = turnTaken[selectedSlot];
                crops[selectedSlot] = (int)FarmCrops.Lettuce;
                break;
            case 1: //당근
                if (!GameManager.inst.CheckResource(water: 10, food: 20))
                    return;
                if (level < 1)
                    return;
                GameManager.inst.UseResource(water: 10, food: 20);
                turnTaken[selectedSlot] = Random.Range(20, 26);
                turnLeft[selectedSlot] = turnTaken[selectedSlot];
                crops[selectedSlot] = (int)FarmCrops.Carrot;
                break;
            case 2: //콩
                if (!GameManager.inst.CheckResource(water: 20, food: 10))
                    return;
                if (level < 2)
                    return;
                GameManager.inst.UseResource(water: 20, food: 10);
                turnTaken[selectedSlot] = Random.Range(15, 21);
                turnLeft[selectedSlot] = turnTaken[selectedSlot];
                crops[selectedSlot] = (int)FarmCrops.Bean;
                break;
            case 3: //감자
                if (!GameManager.inst.CheckResource(water: 40, food: 50))
                    return;
                if (level < 3)
                    return;
                GameManager.inst.UseResource(water: 40, food: 50);
                turnTaken[selectedSlot] = Random.Range(50, 70);
                turnLeft[selectedSlot] = turnTaken[selectedSlot];
                crops[selectedSlot] = (int)FarmCrops.Potato;
                break;
            default:
                Debug.Log("There is no Option for " + option.ToString());
                return;
        }
        state[selectedSlot] = (int)FarmState.Grow;

    }

    public void CancelCrops()
    {
        turnTaken[selectedSlot] = 0;
        turnLeft[selectedSlot] = 0;
        crops[selectedSlot] = (int)FarmCrops.None;
        state[selectedSlot] = (int)FarmState.Idle;
    }

    public void HarvestCrops(int crop)
    {
        switch (crop)
        {
            case (int)FarmCrops.Lettuce:
                GameManager.inst.GetResource(food: 40);
                break;
            case (int)FarmCrops.Carrot:
                GameManager.inst.GetResource(food: 70);
                break;
            case (int)FarmCrops.Bean:
                GameManager.inst.GetResource(food: 70);
                break;
            case (int)FarmCrops.Potato:
                GameManager.inst.GetResource(food: 200);
                break;
            default:
                Debug.Log("There is no crop for " + crops[selectedSlot].ToString());
                return;
        }
        CancelCrops();
    }
}
