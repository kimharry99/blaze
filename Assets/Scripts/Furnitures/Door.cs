using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Furniture
{
	public override FurnitureType type { get { return FurnitureType.Door; } }

	public override void OnUseButtonClicked()
	{
		SceneManager.LoadScene("Outdoor");
	}
}
