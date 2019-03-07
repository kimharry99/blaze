using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Disease : Buff
{
	[TextArea]
	[SerializeField]
	public string cureInfoString;
	public AudioClip cureSFX;
	public virtual void Cure()
	{
		SoundManager.inst.PlaySFX(cureSFX);
	}
	public abstract void AddNewDisease();
}