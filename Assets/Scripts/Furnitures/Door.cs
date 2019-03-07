using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Furniture
{
    private readonly int[] thiefPreventionRate = { 0, 50, 75 };
	private readonly int powerUsePerSecond = 1;
	private readonly int maxPowerStorage = 500;
	public int curPower;

	public int ThiefPreventionRate {
		get {
			int level = this.Level;
			if (level > 2 && curPower <= 0)
				level = 2;
			return thiefPreventionRate[level - 1];
		}
	}

	public override void Init()
	{
		Level = 1;
		TurnManager.inst.OnTurnPassed += OnTurnPassed;
	}

	public override void OnTurnPassed(int turn)
	{
		if (Level > 2)
			curPower = Mathf.Max(curPower - powerUsePerSecond, 0);
	}

	public void GoOutside()
    {
        SceneManager.LoadScene("Outdoor");
    }
}
