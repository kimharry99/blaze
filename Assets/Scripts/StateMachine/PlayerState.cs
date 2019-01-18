using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{ 
	protected abstract void Enter();
	protected abstract void Exit();
	protected abstract void StateUpdate();

	public static PlayerState curState;

	public void Transition(PlayerState nextState)
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

	protected override void StateUpdate()
	{
		
	}
}

public class PlayerStateWorking : PlayerState
{
	protected override void Enter()
	{
		HomeUIManager.inst.OpenTurnPassUI(10,"Working...");
	}

	protected override void Exit()
	{
		throw new System.NotImplementedException();
	}

	protected override void StateUpdate()
	{
		throw new System.NotImplementedException();
	}
}