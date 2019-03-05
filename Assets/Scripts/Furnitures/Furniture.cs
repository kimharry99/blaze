using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : ScriptableObject
{
	public string furnitureName;
	public int level;
	[SerializeField]
	protected AudioClip furnitureSFX;

	public virtual void Init()
	{
		level = 0;
	}

	public virtual void OnTurnPassed(int turn)
	{
        
	}

	public virtual void Upgrade()
	{
		++level;
	}
}
