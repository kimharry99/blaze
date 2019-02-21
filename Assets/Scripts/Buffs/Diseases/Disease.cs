using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Disease : Buff
{
	[TextArea]
	[SerializeField]
	public string cureInfoString;
	public abstract void Cure();
}