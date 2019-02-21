﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : ScriptableObject
{
	public Texture buffTexture;
	[SerializeField]
	public int remainedTurn;
	public int buffCount;
	public string buffIndexName;
	public string buffName;
	[TextArea]
	public string description;
	public abstract bool IsActivated { get; }
	public abstract void Apply(int turn);
	public virtual void Init() { }
	protected virtual void OnTurnPassed(int turn)
	{
		remainedTurn = Mathf.Max(remainedTurn - turn, 0);
	}

	public void SetRemainedTurn(int turn)
	{
		remainedTurn = turn;
	}

	public void AddBuffCount(int count)
	{
		buffCount += count;
	}

	public void LoadData(string json)
	{
		JsonUtility.FromJsonOverwrite(json, this);
	}
}
