using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
	public static PlayerStateIdle idle;
	public static PlayerStateWork work;
	
	public static PlayerState curState;

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

	protected override void Enter()
	{
		HomeUIManager.inst.OpenTurnPassUI(remainedTurn,"Working...");
		timer = 0.5f;
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
			remainedTurn--;
			if (remainedTurn == 0)
			{

			}
			timer = 0.5f;
		}
	}
}