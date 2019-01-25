using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : SingletonBehaviour<MapManager>
{
	public Tilemap tileMap;
	public Vector3Int curPosition;


    // Start is called before the first frame update
    void Start()
    {
		//AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<HomeTile>(),"Assets/Tiles/HomeTile.asset");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
		{
			TileData data;
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3Int cellPos = tileMap.WorldToCell(mousePos);
		}
    }
	/*
	private void MapMaker(int size)
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
}
