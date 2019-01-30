using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : SingletonBehaviour<MapManager>
{
	public Tilemap landTilemap;
	public Tilemap structureTilemap;
	public Vector3Int curPosition;
	private Vector3Int destPosition;

	public GameObject player;

	public TileBase[] tiles;

    private void Start()
    {
		//AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<FieldTile>(),"Assets/Tiles/FieldTile.asset");

		MapMaking(50);
		SaveMapData();
		//LoadMapData();
		//UpdateTiles();
	}

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3Int cellPos = landTilemap.WorldToCell(mousePos);
			if (IsNearByTile(cellPos))
				MoveTo(cellPos);
		}
    }
	
	private void MapMaking(int size)
	{
		landTilemap.size = new Vector3Int(size, size, 1);
		for (int i = 0; i < size; ++i)
			for (int j = 0; j < size; ++j)
				landTilemap.SetTile(new Vector3Int(i, j, 0), tiles[0]);
	}

	public bool IsNearByTile(Vector3Int tilePosition)
	{
		return Vector3Int.Distance(curPosition, tilePosition) == 1 ||
			curPosition + new Vector3Int((Mathf.Abs(curPosition.y) % 2) * 2 - 1, 1, 0) == tilePosition ||
			curPosition + new Vector3Int((Mathf.Abs(curPosition.y) % 2) * 2 - 1, -1, 0) == tilePosition;
	}

	private void MoveTo(Vector3Int tilePosition)
	{
		destPosition = tilePosition;
		int moveCost = landTilemap.GetTile<LandTile>(tilePosition).MoveCost;
		GameManager.inst.StartTask(Move, moveCost);
	}

	private void Move()
	{
		curPosition = destPosition;
		player.transform.position = landTilemap.CellToWorld(curPosition);
		landTilemap.GetTile<LandTile>(curPosition).OnVisited();
		//UpdateTiles();
		//if (structureTilemap.GetTile<StructureTile>(curPOsition) != null)
		//	structureTilemap.GetTile<StructureTile>(curPOsition).OnVisited();
	}

	private void UpdateTiles()
	{
		foreach (var pos in landTilemap.cellBounds.allPositionsWithin)
		{
			LandTile tile = landTilemap.GetTile<LandTile>(pos);
			if (!IsNearByTile(pos))
			{
				if (!tile.IsVisited)
				{
					landTilemap.SetColor(pos, Color.black);
				}
				else
				{
					landTilemap.SetColor(pos, Color.blue);
				}
			}
			else
			{
				if (!tile.IsVisited)
				{
					landTilemap.SetColor(pos, Color.red);
				}
				else
				{
					landTilemap.SetColor(pos, Color.white);
				}
			}
		}
	}

	public void SaveMapData()
	{
		List<LandTileInfo> infos = new List<LandTileInfo>();
		foreach (var pos in landTilemap.cellBounds.allPositionsWithin)
		{
			LandTile tile = landTilemap.GetTile<LandTile>(pos);
			if (tile != null)
			{
				infos.Add(tile.GetLandTileInfo(pos));
			}
		}
		MapInfo mapInfo = new MapInfo(infos);
		JsonHelper.JsonToFile(JsonUtility.ToJson(mapInfo, true), "Save/Map.json");
	}

	public void LoadMapData()
	{
		MapInfo mapInfo = JsonUtility.FromJson<MapInfo>(JsonHelper.LoadJson("Save/Map.json"));
		foreach (var info in mapInfo.landTileInfos)
		{
			landTilemap.SetTile(info.position, tiles[0]);
			landTilemap.SetTileFlags(info.position, TileFlags.None);
			landTilemap.GetTile<LandTile>(info.position).Init();
		}
	}
}

[System.Serializable]
public class MapInfo
{
	public List<LandTileInfo> landTileInfos;
	public MapInfo(List<LandTileInfo> landTileInfos)
	{
		this.landTileInfos = landTileInfos;
	}
}