using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : Furniture
{
	private readonly int[] woodPerTurn = { 1, 2, 3 };
	private readonly int[] maxCapacity = { 30, 80, 200 };

    private int option = 0;
    private int selectLevel = 0;

    public int MaxCapacity { get { return maxCapacity[selectLevel]; } }

	public void UseFurniture()
	{
		/*
        if (Level == 0) return;
        switch(option)
        {
            case 0: //전원 차단
                foreach (var type in Enum.GetValues(typeof(FurnitureType)))
                {
                    GameManager.inst.furnitures[(int)type].isRun = false;
                }
                break;
            case 1:
            case 2:
            case 3:
                if(option <= Level && selectLevel != option)
                {
                    selectLevel = option;
                }
                break;
            default:
                Debug.Log("There is no Option for " + option.ToString());
                break;
        }
		*/
	}

    public void SetOption(int opt)
    {
        option = opt;
    }

 //   public void OnTurnPassed(int turn)
	//{
 //       if (!GameManager.inst.CheckResource(wood: woodPerTurn[selectLevel]))
 //       {
 //           foreach (var type in Enum.GetValues(typeof(FurnitureType)))
 //           {
 //               //GameManager.inst.furnitures[(int)type].isRun = false;
 //           }
 //       }
 //       else
 //       {
 //           GameManager.inst.UseResource(wood: woodPerTurn[selectLevel]);
 //       }
	//}
}
