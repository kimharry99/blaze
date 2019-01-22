using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Furniture
{
    protected override FurnitureType type { get { return FurnitureType.Bed; } }

    int usingTurn=1;

    public override void OnUseButtonClicked()
    {
        GameManager.inst.StartTask(UseFurniture, usingTurn);
    }

    public void OnPlusButtonClicked()
    {
        usingTurn++;
    }

    public void OnMinusButtonClicked()
    {
        usingTurn = Mathf.Min(0,usingTurn-1);
    }

    private void UseFurniture()
    {
        GameManager.inst.Rest(usingTurn*4);
    }

}
