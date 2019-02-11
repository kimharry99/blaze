using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Furniture
{
	public override FurnitureType type { get { return FurnitureType.Door; } }

    private readonly int[] thiefPreventionRate = { 0, 50, 75 };

    public int ThiefPreventionRate { get { return thiefPreventionRate[Level]; } }

    private void Start()
    {
        Level = 0;
    }

    public override void OnUseButtonClicked()
	{
		SceneManager.LoadScene("Outdoor");
	}
}
