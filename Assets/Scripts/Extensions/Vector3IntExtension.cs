using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3IntExtension
{
	public static int GetTileDistance(this Vector3Int a, Vector3Int b)
	{
		int dx = b.x - a.x;     // signed deltas
		int dy = b.y - a.y;
		int x = Mathf.Abs(dx);  // absolute deltas
		int y = Mathf.Abs(dy);
		
		if ((dx < 0) ^ ((a.y & 1) == 1))
			x = Mathf.Max(0, x - (y + 1) / 2);
		else
			x = Mathf.Max(0, x - (y) / 2);
		return x + y;
	}
}
