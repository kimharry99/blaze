using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : ScriptableObject
{
	public Texture buffTexture;
	public int RemainedTurn { get; protected set; }
	public int BuffCount { get; protected set; }
	public string buffName;
	[TextArea]
	public string description;
	public abstract bool IsActivated { get; }
	public abstract void Apply(int turn);
	protected virtual void OnTurnChanged(int turn) { }
}
