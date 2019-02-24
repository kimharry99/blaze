using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : ScriptableObject
{
	public string furnitureName;
	public int level;

	public virtual void Init()
	{

	}

	public virtual void OnTurnPassed(int turn)
	{

	}

	public virtual void Upgrade()
	{
		++level;
	}
}
