using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Furniture
{
    public override FurnitureType type { get { return FurnitureType.Bed; } }

    private readonly int[] energyPerTurn = { 4, 5, 6};
    private readonly int[] cureTurnLeft = { 5, 4, 3 };
    private readonly int[] healTurnLeft = { 999,10,6 };

    public int EnergyPerTurn { get { return energyPerTurn[Level-1]; } }
    public int CureTurnLeft { get { return cureTurnLeft[Level-1]; } }
    public int HealTurnLeft { get { return healTurnLeft[Level-1]; } }

    int usingTurn=1;


	private void Start()
	{
		Level = 1;
        GameManager.inst.furnitures[(int)type] = new infoFurniture(Level, false);
    }

	public override void OnUseButtonClicked()
    {
        GameManager.inst.StartTask(UseFurniture, usingTurn, true);
    }

    public void OnPlusButtonClicked()
    {
        usingTurn++;
        Debug.Log("Level : " + Level+ "\nUsingTurn : " + usingTurn);
    }

    public void OnMinusButtonClicked()
    {
        usingTurn = Mathf.Max(1, usingTurn - 1);
        Debug.Log("Level : " + Level + "\nUsingTurn : " + usingTurn);
    }

    private void UseFurniture()
    {
        GameManager.inst.Rest(usingTurn*(EnergyPerTurn));
        if (GameManager.inst.CheckStatus(health: 0, mental: 0, hunger: 0, thirst: 0, energy: 0))
        {
            GameManager.inst.Cure(usingTurn / CureTurnLeft);
        }
        GameManager.inst.Heal(usingTurn / HealTurnLeft);
    }
}
