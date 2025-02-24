﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class MapManager : SingletonBehaviour<MapManager>
{
	public Tilemap landTilemap;
	public Tilemap structureTilemap;

	public Vector3Int curPosition;
	private Vector3Int destPosition;
	private Vector3Int? coloredPosition = null;

	public GameObject player;

	public TileBase[] landTiles;
	public TileBase[] structureTiles;

	public Dictionary<Vector3Int, TileInfo> tileInfos = new Dictionary<Vector3Int, TileInfo>();

	private Color BaseTileColor = Color.white;

	public Animator playerAnimator;

	public int SightDistance
	{
		get
		{
			DayNight dayNight = TurnManager.inst.DayNight;
			Weather weather = TurnManager.inst.Weather;
			return 3 + (int)dayNight + (int)weather;
		}
	}

	private bool isMoving = false;


	private List<Vector3Int> coloredPos = new List<Vector3Int>();
	private List<Vector3Int> visitedPos = new List<Vector3Int> { Vector3Int.zero };

	[SerializeField]
	private AudioClip walkSFX;

	protected override void Awake()
	{
		if (inst != this)
		{
			Destroy(gameObject);
			return;
		}
		LoadMapData();
		foreach (var pos in landTilemap.cellBounds.allPositionsWithin)
		{
			if (!tileInfos.ContainsKey(pos))
				continue;
			landTilemap.SetColor(pos, Color.black);
			structureTilemap.SetColor(pos, Color.black);
		}
	}

	private void Start()
    {
		UpdateTiles();
		OutdoorUIManager.inst.UpdateTileInfoPanel();
		TurnManager.inst.OnTurnPassed += OnTurnPassed;
	}

	private void Update()
	{
		if (GameManager.inst.IsGameOver)
		{
			if (EventSystem.current != null)
				EventSystem.current.enabled = false;
			return;
		}
		if (isMoving)
			return;
		if (EventSystem.current.IsPointerOverGameObject())
			return;
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3Int cellPos = landTilemap.WorldToCell(mousePos);
		if(cellPos != coloredPosition)
		{
			if (coloredPosition != null)
			{
				landTilemap.SetColor((Vector3Int)coloredPosition, tileInfos[(Vector3Int)coloredPosition].TileColor);
				structureTilemap.SetColor((Vector3Int)coloredPosition, tileInfos[(Vector3Int)coloredPosition].TileColor);
			}

			if (cellPos == curPosition)
			{
				landTilemap.SetColor(cellPos, Color.yellow);
				structureTilemap.SetColor(cellPos, Color.yellow);
				coloredPosition = cellPos;
			}
			else if (IsNearByTile(cellPos))
			{
				landTilemap.SetColor(cellPos, Color.blue);
				structureTilemap.SetColor(cellPos, Color.blue);
				coloredPosition = cellPos;
			}
			else if (IsNearByTile(cellPos, SightDistance))
			{
				landTilemap.SetColor(cellPos, Color.red);
				structureTilemap.SetColor(cellPos, Color.red);
			}
			if (tileInfos.ContainsKey(cellPos))
				coloredPosition = cellPos;
			else
				coloredPosition = null;
		}

		if (Input.GetMouseButtonDown(0))
		{
			if (IsNearByTile(cellPos) && cellPos != curPosition)
				MoveTo(cellPos);
		}
    }

	private void OnDestroy()
	{
		TurnManager.inst.OnTurnPassed -= OnTurnPassed;
	}

	#region Map Data Functions
	private void MapMaking(int size)
	{
		//landTilemap.size = new Vector3Int(size, size, 1);
		TileInfo info;
		for (int i = -size; i < size; ++i)
			for (int j = -size; j < size; ++j)
			{
				Vector3Int pos = new Vector3Int(i, j, 0);
				if (!IsNearByTile(pos, size - 1))
					continue;
				LandType landType = (LandType)Random.Range(0, 4);
				StructureType structureType;
				if (Random.Range(0, 100) < 30)
				{
					structureType = (StructureType)Random.Range(1, 8);
				}
				else
				{
					structureType = StructureType.None;
				}

				info = new TileInfo(pos, landType, structureType);

				landTilemap.SetTile(pos, landTiles[(int)landType]);
				landTilemap.SetTileFlags(pos, TileFlags.None);

				structureTilemap.SetTile(pos, structureTiles[(int)structureType]);
				structureTilemap.SetTileFlags(pos, TileFlags.None);

				switch (structureType)
				{
					case StructureType.None:
						info.Init(water: Random.Range(0, 11), food: Random.Range(0, 11));
						break;
					case StructureType.Home:
						info.Init(isVisited: true);
						break;
					case StructureType.Building:
						info.Init(water: Random.Range(0, 11), food: Random.Range(0, 11), wood: Random.Range(0, 6), components: Random.Range(0, 16), parts: Random.Range(0, 6));
						break;
					case StructureType.Store:
						info.Init(water: Random.Range(0, 11), food: Random.Range(0, 11), preserved: Random.Range(0, 11), wood: Random.Range(0, 6), components: Random.Range(0, 6));
						break;
					case StructureType.Factory:
						info.Init(preserved: Random.Range(0, 11), components: Random.Range(0, 11), parts: Random.Range(0, 21));
						break;
					case StructureType.Forest:
						info.Init(water: Random.Range(0, 21), food: Random.Range(0, 21), wood: Random.Range(10, 31));
						break;
				}

				tileInfos.Add(pos, info);
			}

		info = new TileInfo(Vector3Int.zero, LandType.Road, StructureType.Home);
		info.Init(isVisited: true);

		landTilemap.SetTile(Vector3Int.zero, landTiles[0]);
		landTilemap.SetTileFlags(Vector3Int.zero, TileFlags.None);

		structureTilemap.SetTile(Vector3Int.zero, structureTiles[0]);
		structureTilemap.SetTileFlags(Vector3Int.zero, TileFlags.None);

		tileInfos.Remove(Vector3Int.zero);
		tileInfos.Add(Vector3Int.zero, info);
	}

	private void InitMapData()
	{
		MapInfo mapInfo = JsonUtility.FromJson<MapInfo>(JsonHelper.LoadJson("Save/Map"));
		foreach (var info in mapInfo.tileInfos)
		{
			landTilemap.SetTile(info.position, landTiles[(int)info.landType]);
			landTilemap.SetTileFlags(info.position, TileFlags.None);

			structureTilemap.SetTile(info.position, structureTiles[(int)info.structureType]);
			structureTilemap.SetTileFlags(info.position, TileFlags.None);

			tileInfos.Add(info.position, info);
		}
		curPosition = mapInfo.curPosition;
	}

	public void SaveMapData()
	{
		List<TileInfo> infos = tileInfos.Values.ToList();

		MapInfo mapInfo = new MapInfo(infos, curPosition);
		JsonHelper.SaveJson(JsonUtility.ToJson(mapInfo, true), "Map.json");
	}

	public void LoadMapData()
	{
		if (!File.Exists(Application.persistentDataPath + "Map.json"))
		{
			MapMaking(20);
			SaveMapData();
			return;
		}
		MapInfo mapInfo = JsonUtility.FromJson<MapInfo>(JsonHelper.LoadSavedJson("Map.json"));
		foreach (var info in mapInfo.tileInfos)
		{
			landTilemap.SetTile(info.position, landTiles[(int)info.landType]);
			landTilemap.SetTileFlags(info.position, TileFlags.None);

			structureTilemap.SetTile(info.position, structureTiles[(int)info.structureType]);
			structureTilemap.SetTileFlags(info.position, TileFlags.None);

			tileInfos.Add(info.position, info);
		}
		curPosition = mapInfo.curPosition;
	}
	#endregion

	#region Utility Functions
	public bool IsNearByTile(Vector3Int tilePosition, int distance = 1)
	{
		return curPosition.GetTileDistance(tilePosition) <= distance;

		/*
		return Vector3Int.Distance(curPosition, tilePosition) == 1 ||
			curPosition + new Vector3Int((Mathf.Abs(curPosition.y) % 2) * 2 - 1, 1, 0) == tilePosition ||
			curPosition + new Vector3Int((Mathf.Abs(curPosition.y) % 2) * 2 - 1, -1, 0) == tilePosition;
		*/
	}

	public TileInfo GetCurrentTileInfo()
	{
		return tileInfos[curPosition];
	}
	#endregion

	private void OnTurnPassed(int turn)
	{
		UpdateTiles();
	}

	private void UpdateTiles()
	{
		foreach (var pos in coloredPos)
		{
			landTilemap.SetColor(pos, Color.black);
			structureTilemap.SetColor(pos, Color.black);
		}

		coloredPos.RemoveAll((a) => true);

		foreach (var pos in landTilemap.cellBounds.allPositionsWithin)
		{
			if (!tileInfos.ContainsKey(pos))
				continue;
			if (!visitedPos.Contains(pos) && !IsNearByTile(pos, SightDistance))
				continue;
			coloredPos.Add(pos);
			TileInfo info = tileInfos[pos];
			landTilemap.SetColor(pos, info.TileColor);
			structureTilemap.SetColor(pos, info.TileColor);
		}
	}

	#region Character Move Functions
	private void MoveTo(Vector3Int tilePosition)
	{
		destPosition = tilePosition;
		int moveCost = landTilemap.GetTile<LandTile>(tilePosition).MoveCost;
		player.GetComponent<SpriteRenderer>().flipX = curPosition.y > tilePosition.y;
		playerAnimator.SetBool("IsWalking", true);
		SoundManager.inst.PlaySFX(walkSFX);
		GameManager.inst.StartTask(Move, moveCost);
		isMoving = true;
	}

	private void Move()
	{
		Vector3 start = landTilemap.CellToWorld(curPosition);
		Vector3 end = landTilemap.CellToWorld(destPosition);
		StartCoroutine(CharacterMove(start, end));
		curPosition = destPosition;

		OutdoorUIManager.inst.UpdateTileInfoPanel();
	}

	private IEnumerator CharacterMove(Vector3 start, Vector3 end)
	{
		Vector3 direction = end - start;
		for (float t = 0; t <= 1; t += 0.025f)
		{
			player.transform.position = start + direction * Vector2Utility.GetQuadricBeizerCurvePoint(Vector2.zero, Vector2.one, new Vector2(0, 1), new Vector2(0, 1), t).y;
			yield return null;
		}
		landTilemap.GetTile<LandTile>(curPosition).OnVisited(curPosition);
		structureTilemap.GetTile<StructureTile>(curPosition).OnVisited(curPosition);
		tileInfos[curPosition].OnVisited();
		visitedPos.Add(curPosition);
		isMoving = false;
		playerAnimator.SetBool("IsWalking", false);
		UpdateTiles();
	}
	#endregion
}

[System.Serializable]
public class MapInfo
{
	public Vector3Int curPosition;
	public List<TileInfo> tileInfos;

	public MapInfo(List<TileInfo> tileInfos, Vector3Int curPosition)
	{
		this.curPosition = curPosition;
		this.tileInfos = tileInfos;
	}
}