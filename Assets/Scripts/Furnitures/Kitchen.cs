using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : Furniture
{
	public int selectedRecipie = -1; //None
	public int[] usingResource = new int[4];

	[SerializeField]
	private AudioClip boilSFX, cookSFX;

	public override void Init()
	{
		Level = 1;
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
		if (selectedRecipie != 1)
		{
			SoundManager.inst.PlaySFX(cookSFX);
		}
		else
		{
			SoundManager.inst.PlaySFX(boilSFX);
		}

		GameManager.inst.UseResource(water: usingResource[2], food: usingResource[0], preserved: usingResource[1], components: usingResource[3]);
		switch (selectedRecipie)
		{
			case -1://None
				break;
			case 0: //조약한 식사
				GameManager.inst.StartTask(((Food)GameManager.inst.items["CoarseMeal"]).AddNewFood, 3);
				break;
			case 1: //시원한 물
				GameManager.inst.StartTask(((Food)GameManager.inst.items["CleanWater"]).AddNewFood, 1);
				break;
			case 2: //보존음식
				GameManager.inst.StartTask(delegate { GameManager.inst.GetResource(preserved: 10); }, 1);
				break;

			case 3: //평범한 식사
				GameManager.inst.StartTask(((Food)GameManager.inst.items["Meal"]).AddNewFood, 3);
				break;
			case 4: //호화로운 식사
				GameManager.inst.StartTask(((Food)GameManager.inst.items["FineMeal"]).AddNewFood, 3);
				break;
			case 5: //보양식
				GameManager.inst.StartTask(((Food)GameManager.inst.items["HealthyMeal"]).AddNewFood, 3);
				break;
			case 6: //달콤한 간식
				GameManager.inst.StartTask(((Food)GameManager.inst.items["Snack"]).AddNewFood, 3);
				break;
			default:
				Debug.Log("There is no Option for " + selectedRecipie.ToString());
				break;
		}
	}
}
