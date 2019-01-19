using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
	public static PlayerStateIdle idle = new PlayerStateIdle();
	public static PlayerStateWork work = new PlayerStateWork();
	
	public static PlayerState curState = idle;

	protected abstract void Enter();
	protected abstract void Exit();
	public abstract void StateUpdate();

	public static void Transition(PlayerState nextState)
	{
		curState.Exit();
		curState = nextState;
		curState.Enter();
	}

	protected GameManager gm = GameManager.inst;
}

public class PlayerStateIdle : PlayerState
{
	protected override void Enter()
	{
		
	}

	protected override void Exit()
	{
		
	}

	public override void StateUpdate()
	{
		
	}
}

public class PlayerStateWork : PlayerState
{
	public static int remainedTurn;
	private float timer;
	private int passedTurn;

	protected override void Enter()
	{
		HomeUIManager.inst.OpenTurnPassUI(remainedTurn,"Working...");
		timer = 0.5f;
		passedTurn = 0;
	}

	protected override void Exit()
	{
		HomeUIManager.inst.CloseTurnPassUI();
		GameManager.inst.EndTask();
	}

	public override void StateUpdate()
	{
		timer -= Time.deltaTime;
		if (timer <= 0)
		{
			if (remainedTurn == 0)
			{
				Transition(idle);
			}
			TurnManager.inst.UseTurn(1);
			remainedTurn--;
			passedTurn++;
			HomeUIManager.inst.UpdateTurnPassUI(passedTurn, passedTurn + remainedTurn);
			timer = 0.5f;
		}
	}
}