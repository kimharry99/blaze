using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 물을 넣어두면, 시간이 지나 깨끗한 물을 얻을 수 있다.


 날씨가 맑음 and 밤이 아닐 때, 완성까지 남은턴이 줄어든다.

 생산 중에는 레벨업을 할 수 없다. 생산을 취소해야 레벨업을 할 수 있다.

 취소시 자원은 돌려받을 수 없다.

 Level 1
업그레이드 시 목재 10, 잡동사니 20, 기계 부품 10 소모
물을 10 소모하고, 200턴 후 깨끗한 물 1개를 얻는다.

 Level 2
업그레이드 시 목재 20, 잡동사니 40, 기계 부품 10 소모
물을 10 소모하고, 150턴 후 깨끗한 물 1개를 얻는다.

 Level 3
업그레이드 시 목재 20, 잡동사니 50, 기계 부품 15 소모
물을 10 소모하고, 100턴 후 깨끗한 물 1개를 얻는다.
*/
public class SolarWaterPurifier : Furniture 
{
    private readonly int[] requiresTurn = { 0, 200, 150, 100 };

    public int RequiresTurn { get { return requiresTurn[level]; } } 
    public int water;
    public int cleanWater;
    public int turnLeft;

    public bool isUsing = false;

    public override void Init()
    {
        TurnManager.inst.OnTurnPassed += OnTurnPassed;
    }

    public override void OnTurnPassed(int turn)
    {
        if (!(turnLeft > 0))
            return;
        if (TurnManager.inst.Weather == Weather.Sun && TurnManager.inst.Turn >=24&&TurnManager.inst.Turn <72)
        {
            --turnLeft;
        }
        if (turnLeft == 0)
        {
            cleanWater++;
        }
    }

    public void InputWater()
    {
        GameManager.inst.UseResource(water: 10);
        TurnManager.inst.UseTurn(4);
        isUsing = true;
    }

    public void HarvestCleanWater()
    {
        TurnManager.inst.UseTurn(2);
        //아이템 얻기 깨끗한 물
        cleanWater = 0;
        isUsing = false;
    }

    public void CancelJob()
    {
        turnLeft = 0;
        isUsing = false;
    }
}
