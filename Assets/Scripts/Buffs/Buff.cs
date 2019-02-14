using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : ScriptableObject
{
	public Sprite buffImage;
	public int RemainedTurn { get; private set; }
	public int BuffCount { get; private set; }
	public string buffName;
	public string description;
	public abstract bool IsActivated { get; }
	public abstract void Init();
	public abstract void Remove();
	public abstract void Apply(int turn);
	protected virtual void OnTurnChanged(int turn) { }
}
