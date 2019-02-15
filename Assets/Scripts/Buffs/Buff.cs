using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : ScriptableObject
{
	public Sprite buffImage;
	public int RemainedTurn { get; protected set; }
	public int BuffCount { get; protected set; }
	public string buffName;
	public string description;
	public abstract bool IsActivated { get; }
	public abstract void Apply(int turn);
	protected virtual void OnTurnChanged(int turn) { }
}
