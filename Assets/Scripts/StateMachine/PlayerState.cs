using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
	public static PlayerStateIdle idle = new PlayerStateIdle();
	public static PlayerStateWork work = new PlayerStateWork();
	public static PlayerStateWait wait = new PlayerStateWait();

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
	private float timePerTurn;
	private float timer;
	private int passedTurn;

	protected override void Enter()
	{
		Debug.Log(GameManager.inst.turnPassTime);
		TurnManager.inst.StartCoroutine(TurnPassRoutine(remainedTurn, GameManager.inst.turnPassTime / remainedTurn));
	}

	protected override void Exit()
	{
		GameManager.inst.EndTask();
	}

	public override void StateUpdate()
	{

	}

	private IEnumerator TurnPassRoutine(int remainedTurn, float timer)
	{
		if (GameManager.inst.IsGameOver)
		{
			Transition(idle);
			yield break;
		}
		RectTransform minuteHand = UIManager.inst.minuteHand;
		RectTransform hourHand = UIManager.inst.hourHand;
		SkyScript sky = Object.FindObjectOfType<SkyScript>();

		for (float i = timer; i > 0; i -= Time.deltaTime)
		{
			minuteHand.Rotate(0, 0, -90 * Time.deltaTime / timer);
			hourHand.Rotate(0, 0, -7.5f * Time.deltaTime / timer);
			if (Object.FindObjectOfType<SkyScript>() != null)
			{
				sky.sunLight.transform.Rotate(3.75f * Time.deltaTime / timer, 0, 0);
				sky.skyObject.transform.Translate(sky.SkyMovement() * Time.deltaTime / timer);
				sky.playerSpotlight.intensity += sky.SpotLightItensityDelta() * Time.deltaTime / timer;
			}

			yield return null;
		}
		TurnManager.inst.UseTurn(1);
		if (remainedTurn > 1)
			TurnManager.inst.StartCoroutine(TurnPassRoutine(remainedTurn - 1, timer));
		else
			Transition(idle);

	}
}

public class PlayerStateWait : PlayerState
{
	public override void StateUpdate()
	{
		
	}

	protected override void Enter()
	{
		
	}

	protected override void Exit()
	{
		
	}
}