using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Furniture
{
    public override FurnitureType type { get { return FurnitureType.Bed; } }

    int usingTurn=1;

    public override void OnUseButtonClicked()
    {
        GameManager.inst.StartTask(UseFurniture, usingTurn);
    }

    public void OnPlusButtonClicked()
    {
        usingTurn++;
        Debug.Log(usingTurn);
    }

    public void OnMinusButtonClicked()
    {
        usingTurn = Mathf.Max(1,usingTurn-1);
        Debug.Log(usingTurn);
    }

    private void UseFurniture()
    {
        GameManager.inst.Rest(usingTurn*4);
    }

}
