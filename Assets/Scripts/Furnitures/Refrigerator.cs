using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refrigerator : Furniture
{
    public override FurnitureType type { get { return FurnitureType.Refrigerator; } }

    private readonly int[] powerConsumption = { 0, 15, 40, 60 };
    private readonly int[] lostFoodRate = { 20, 10, 5, 2 };

    public int PowerConsumption { get { return powerConsumption[Level]; } }
    public int LostFoodRate { get { return lostFoodRate[Level]; } }

    public override void OnUseButtonClicked()
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        Level = 0;
    }

    private void UseFurniture()
    {
        throw new System.NotImplementedException();
    }
}
