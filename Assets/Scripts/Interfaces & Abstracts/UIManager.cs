using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIManager : SingletonBehaviour<UIManager>
{
	public abstract void Init();
	public abstract void OnDestroy();
}
