using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftTable : Furniture
{
    public int[] usingResource = new int[3];
    public int selectRecipie = -1;
    public override void Init()
    {
        Level = 1;
    }

    public void SelectRecipie(int option)
    {
        switch (option)
        {
            case 0:
                usingResource[0] = 10;
                selectRecipie = option;
                break;
            case 1:
                usingResource[0] = 10;
                usingResource[1] = 1;
                selectRecipie = option;
                break;
            case 2:
                usingResource[0] = 10;
                usingResource[2] = 3;
                break;
            default:
                break;
        }
    }

    public void MakeMedicine()
    {
        switch (selectRecipie)
        {
            case 0:
                GameManager.inst.StartTask(null, 2);
                GameManager.inst.UseResource(components: usingResource[0]);
                GameManager.inst.items["Bandage"].amount += 2;
                break;
            case 1:
                GameManager.inst.StartTask(null, 4);
                GameManager.inst.UseResource(components: usingResource[0]);
                GameManager.inst.items["Alcohol"].amount-=usingResource[1];
                GameManager.inst.items["Antiseptic"].amount += 2;
                break;
            case 2:
                GameManager.inst.StartTask(null, 4);
                GameManager.inst.UseResource(components: usingResource[0]);
                GameManager.inst.items["Herb"].amount -= usingResource[2];
                GameManager.inst.items["Antibiostic"].amount += 2;
                break;
            default:
                break;
        }

    }
}
