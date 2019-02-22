using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : Furniture
{
	public override FurnitureType type { get { return FurnitureType.Kitchen; } }
    private int option = 0;
    int usingTurn = 0;

    public void UseFurniture()
    {
        switch (option)
        {
            case 0: //조약한 식사
                if (GameManager.inst.CheckResource(food: 30, water: 5))
                {
                    GameManager.inst.UseResource(food: 30, water: 5);
                    GameManager.inst.Eat(30);
                    GameManager.inst.Heal(-5);
                }
                break;
            case 1: //시원한 물
                if (GameManager.inst.CheckResource(water: 20))
                {
                    GameManager.inst.UseResource(water: 20);
                    GameManager.inst.Drink(30);
                }
                break;
            case 2: //보존음식 10 *기획 에러*
                if (GameManager.inst.CheckResource(food: 10, components: 1))
                {
                    GameManager.inst.UseResource(food: 10, components: 1);
                }
                break;
            case 3: //평범한 식사
                if (GameManager.inst.CheckResource(food: 50, water: 10))
                {
                    GameManager.inst.UseResource(food: 50, water: 10);
                    GameManager.inst.Eat(40);
                }
                break;
            case 4: //호화로운 식사
                if (GameManager.inst.CheckResource(food: 70, water: 20))
                {
                    GameManager.inst.UseResource(food: 70, water: 20);
                    GameManager.inst.Eat(40);
                    GameManager.inst.Heal(10);
                }
                break;
            case 5: //보양식
                if (GameManager.inst.CheckResource(food: 70, water: 40))
                {
                    GameManager.inst.UseResource(food: 70, water: 40);
                    GameManager.inst.Eat(40);
                    GameManager.inst.Cure(10);
                    GameManager.inst.Heal(5);
                }
                break;
            case 6: //달콤한 간식
                if (GameManager.inst.CheckResource(food: 50, water: 30))
                {
                    GameManager.inst.UseResource(food: 50, water: 30);
                    GameManager.inst.Eat(20);
                    GameManager.inst.Heal(20);
                }
                break;
            default:
                Debug.Log("There is no Option for " + option.ToString());
                break;
        }
    }

    public void SetOption(int opt)
    {
        option = opt;
    }

    public void setTurn()
    {
        switch (option)
        {
            case 0:
            case 3:
                usingTurn = 3;
                break;
            case 1:
            case 2:
                usingTurn = 1;
                break;
            case 4:
            case 5:
            case 6:
                usingTurn = 0;
                break;
            default:
                Debug.Log("There is no Option for " + option.ToString());
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
	{
        Level = 1;
        GameManager.inst.furnitures[(int)type] = new infoFurniture(Level, false);
    }

	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		throw new System.NotImplementedException();
	}

	public override void OpenFurnitureUI()
	{
		base.OpenFurnitureUI();
	}

	public override void OnUseButtonClicked()
	{
        if (option >= 3 && Level < 2) return;
        if (option >= 4 && Level < 3) return;
        setTurn();
        GameManager.inst.StartTask(UseFurniture, usingTurn);
    }
}