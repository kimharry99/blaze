using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorObject : FurnitureObject
{
	public override FurnitureType type { get { return FurnitureType.Door; } }

    private readonly int[] thiefPreventionRate = { 0, 50, 75 };

    public int ThiefPreventionRate { get { return thiefPreventionRate[Level-1]; } }

    private void Start()
    {
        Level = 1;
       //GameManager.inst.furnitures[(int)type] = new infoFurniture(Level, false);
    }

    public override void OnUseButtonClicked()
	{
		SceneManager.LoadScene("Outdoor");
	}
}
