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

    // Start is called before the first frame update
    void Start()
    {
		//AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<FieldTile>(),"Assets/Tiles/FieldTile.asset");
		ColoringMovableTiles();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3Int cellPos = landTilemap.WorldToCell(mousePos);
			if (IsNearByTile(cellPos))
				MoveTo(cellPos);
		}
    }
	/*
	private void MapMaking(int size)
	{
		tileMap.size = new Vector3Int(size,size,size);
		for (int i = 0; i < size; ++i)
			for (int j = 0; j < size; ++j)
				for (int k = 0; k < size; ++k)
				{
					tileMap.SetTile(new Vector3Int(i,j,k),1)
				}
	}
	*/

	public bool IsNearByTile(Vector3Int tilePosition)
	{
		Debug.Log(curPosition);
		Debug.Log(tilePosition);
		Debug.Log(Vector3Int.Distance(curPosition, tilePosition));
		Debug.Log(-1 % 2);
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
		//if (structureTilemap.GetTile<StructureTile>(curPOsition) != null)
		//	structureTilemap.GetTile<StructureTile>(curPOsition).OnVisited();
	}

	private void ColoringMovableTiles()
	{
		//TODO
	}
}
