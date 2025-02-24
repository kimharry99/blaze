﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OutdoorUIManager : SingletonBehaviour<OutdoorUIManager>
{
	#region TileInfo UI
	public GameObject TileInfoPanel;
	public Text TileTypeText;
	public Slider resourceSlider;
	#endregion

	#region Action UI
	public GameObject ActionPanel;
	#endregion

	#region Prefabs
	public GameObject ActionOptionButton;
	#endregion

	public UnityAction act;

	public void UpdateTileInfoPanel()
	{
		foreach (Transform child in ActionPanel.transform)
		{
			Destroy(child.gameObject);
		}


		TileInfo info = MapManager.inst.tileInfos[MapManager.inst.curPosition];
		LandTile landTile = MapManager.inst.landTilemap.GetTile<LandTile>(MapManager.inst.curPosition);
		StructureTile structureTile = MapManager.inst.structureTilemap.GetTile<StructureTile>(MapManager.inst.curPosition);
		TileTypeText.text = landTile.tileName + "/" + structureTile.tileName;
		resourceSlider.value = (info.water + info.food + info.preserved + info.wood + info.components + info.parts) / 20f;

		StructureTile tile = MapManager.inst.structureTilemap.GetTile<StructureTile>(MapManager.inst.curPosition);
		if (tile == null)
			return;
		int i = 0;
		foreach(var action in tile.GetTileActions())
		{
			GameObject btn = Instantiate(ActionOptionButton, ActionPanel.transform);
			btn.GetComponent<Button>().onClick.AddListener(action);
			btn.GetComponentInChildren<Text>().text = tile.actionNames[i++];
		}
	}
}
