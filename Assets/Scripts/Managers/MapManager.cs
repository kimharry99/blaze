using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

	public TileBase[] landTiles;
	public TileBase[] structureTiles;


	public Dictionary<Vector3Int, LandTileInfo> landTileInfos = new Dictionary<Vector3Int, LandTileInfo>();
	public Dictionary<Vector3Int, StructureTileInfo> structureTileInfos = new Dictionary<Vector3Int, StructureTileInfo>();

    private void Start()
    {
		//AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<BuildingTile>(),"Assets/Tiles/StructureTiles/BuldingTile.asset");
		
		//MapMaking(10);
		//SaveMapData();
		LoadMapData();
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
		for (int i = -size; i < size; ++i)
			for (int j = -size; j < size; ++j)
			{
				Vector3Int pos = new Vector3Int(i, j, 0);
				landTilemap.SetTile(pos, landTiles[Random.Range(0, 4)]);
				landTileInfos.Add(pos, landTilemap.GetTile<LandTile>(pos).GetLandTileInfo(pos));
				if (Random.Range(0, 100) < 30)
				{
					structureTilemap.SetTile(pos, structureTiles[Random.Range(1, 5)]);
					structureTileInfos.Add(pos, structureTilemap.GetTile<StructureTile>(pos).GetStructureTileInfo(pos));
				}
			}
		landTilemap.SetTile(Vector3Int.zero, landTiles[0]);
		structureTilemap.SetTile(Vector3Int.zero, structureTiles[0]);

		landTileInfos.Remove(Vector3Int.zero);
		structureTileInfos.Remove(Vector3Int.zero);

		landTileInfos.Add(Vector3Int.zero, landTilemap.GetTile<LandTile>(Vector3Int.zero).GetLandTileInfo(Vector3Int.zero));
		structureTileInfos.Add(Vector3Int.zero, structureTilemap.GetTile<StructureTile>(Vector3Int.zero).GetStructureTileInfo(Vector3Int.zero));
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
		Vector3 start = landTilemap.CellToWorld(curPosition);
		Vector3 end = landTilemap.CellToWorld(destPosition);
		StartCoroutine(CharacterMove(start, end));
		curPosition = destPosition;
		landTilemap.GetTile<LandTile>(curPosition).OnVisited();
		//UpdateTiles();
		//if (structureTilemap.GetTile<StructureTile>(curPOsition) != null)
		//	structureTilemap.GetTile<StructureTile>(curPOsition).OnVisited();
	}

	private IEnumerator CharacterMove(Vector3 start, Vector3 end)
	{
		Vector3 direction = end - start;
		for (float t = 0; t <= 1; t += 0.05f)
		{
			player.transform.position = start + direction * Vector2Utility.GetQuadricBeizerCurvePoint(Vector2.zero, Vector2.one, new Vector2(0, 1), new Vector2(0, 1), t).y;
			yield return null;
		}
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
		List<LandTileInfo> landInfos = landTileInfos.Values.ToList();
		List<StructureTileInfo> structureInfos = structureTileInfos.Values.ToList();

		MapInfo mapInfo = new MapInfo(landInfos, structureInfos, curPosition);
		JsonHelper.JsonToFile(JsonUtility.ToJson(mapInfo, true), "Save/Map.json");
	}

	public void LoadMapData()
	{
		MapInfo mapInfo = JsonUtility.FromJson<MapInfo>(JsonHelper.LoadJson("Save/Map"));

		foreach (var info in mapInfo.landTileInfos)
		{
			landTilemap.SetTile(info.position, landTiles[(int)info.type]);
			landTilemap.SetTileFlags(info.position, TileFlags.None);
			landTilemap.GetTile<LandTile>(info.position).Init();
			landTileInfos.Add(info.position, info);
		}

		foreach ( var info in mapInfo.structureTileInfos)
		{
			structureTilemap.SetTile(info.position, structureTiles[(int)info.type]);
			landTilemap.SetTileFlags(info.position, TileFlags.None);
			landTilemap.GetTile<LandTile>(info.position).Init();
			structureTileInfos.Add(info.position, info);
		}
		curPosition = mapInfo.curPosition;
	}
}

[System.Serializable]
public class MapInfo
{
	public Vector3Int curPosition;
	public List<LandTileInfo> landTileInfos;
	public List<StructureTileInfo> structureTileInfos;

	public MapInfo(List<LandTileInfo> landTileInfos, List<StructureTileInfo> structureTileInfos, Vector3Int curPosition)
	{
		this.curPosition = curPosition;
		this.landTileInfos = landTileInfos;
		this.structureTileInfos = structureTileInfos;
	}
}