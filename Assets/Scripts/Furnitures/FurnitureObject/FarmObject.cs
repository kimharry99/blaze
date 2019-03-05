using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
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
*/
public class FarmObject : FurnitureObject
{
	public override FurnitureType type { get { return FurnitureType.Farm; } }
	public int[] turnLeft = new int[3];
	private FarmState[] state = new FarmState[3];
    private FarmCrops[] crops = new FarmCrops[3];

	private int selectedSlot = 0;
	private int option = 0;

	private void Start()
	{
        Level = 0;
		TurnManager.inst.OnTurnPassed += OnTurnPassed;
        //GameManager.inst.furnitures[(int)type] = new infoFurniture(Level, false);
    }

	private void OnTurnPassed(int turn)
	{
		for (int i = 0; i < Level; ++i)
		{
			if (state[i] != FarmState.Grow)
				return;
			turnLeft[i] -= turn;
			if (turnLeft[i] <= 0)
			{
				state[i] = FarmState.Harvest;
			}
		}
	}

	public void UseFurniture()
	{
        if (state[selectedSlot] == FarmState.Idle)
        {
            switch (option)
            {
                case 0: //상추
                    if (!GameManager.inst.CheckResource(water: 20, food: 5))
                        return;
                    if (Level < 1) 
                        return;
                    GameManager.inst.UseResource(water: 20, food: 5);
                    turnLeft[selectedSlot] = Random.Range(10, 16);
                    crops[selectedSlot] = FarmCrops.Lettuce;
                    break;
                case 1: //당근
                    if (!GameManager.inst.CheckResource(water: 10, food: 20))
                        return;
                    if (Level < 1)
                        return;
                    GameManager.inst.UseResource(water: 10, food: 20);
                    turnLeft[selectedSlot] = Random.Range(20, 26);
                    crops[selectedSlot] = FarmCrops.Carrot;
                    break;
                case 2: //콩
                    if (!GameManager.inst.CheckResource(water: 20, food: 10))
                        return;
                    if (Level < 2)
                        return;
                    GameManager.inst.UseResource(water: 20, food: 10);
                    turnLeft[selectedSlot] = Random.Range(15, 21);
                    crops[selectedSlot] = FarmCrops.Bean;
                    break;
                case 3: //감자
                    if (!GameManager.inst.CheckResource(water: 40, food: 50))
                        return;
                    if (Level < 3)
                        return;
                    GameManager.inst.UseResource(water: 40, food: 50);
                    turnLeft[selectedSlot] = Random.Range(50, 70);
                    crops[selectedSlot] = FarmCrops.Potato;
                    break;
                default:
                    Debug.Log("There is no Option for " + option.ToString());
                    return;
            }

            state[selectedSlot] = FarmState.Grow;
        }
        if(state[selectedSlot] == FarmState.Harvest)
        {
            switch(crops[selectedSlot])
            {
                case FarmCrops.Lettuce:
                    GameManager.inst.GetResource(food: 40);
                    break;
                case FarmCrops.Carrot:
                    GameManager.inst.GetResource(food: 70);
                    break;
                case FarmCrops.Bean:
                    GameManager.inst.GetResource(food: 70);
                    break;
                case FarmCrops.Potato:
                    GameManager.inst.GetResource(food: 200);
                    break;
                default:
                    Debug.Log("There is no crop for " + crops[selectedSlot].ToString());
                    return;
            }
        }
    }

	public override void OpenFurnitureUI()
	{
		furnitureUI.SetActive(true);
	}

	private void OnDestroy()
	{
		TurnManager.inst.OnTurnPassed -= OnTurnPassed;
	}

	public override void OnUseButtonClicked()
	{
		throw new System.NotImplementedException();
	}
}
