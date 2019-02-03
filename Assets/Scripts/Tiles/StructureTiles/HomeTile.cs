using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class HomeTile : StructureTile
{
	public override StructureType Type { get { return StructureType.Home; } }

	public override int RestAmount { get { return 30; } }

	public override void OnVisited()
	{
		throw new System.NotImplementedException();
	}

	public override List<UnityAction> GetTileActions()
	{
		List<UnityAction> actions = new List<UnityAction>();
		actions.Add(ReturnToHome);
		return actions;
	}

	public void ReturnToHome()
	{
		MapManager.inst.SaveMapData();
		SceneManager.LoadScene("Home");
	}
}
