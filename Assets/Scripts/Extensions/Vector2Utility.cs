using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector2Utility
{
	public static Vector2 GetQuadricBeizerCurvePoint(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
	{
		float u = 1.0f - t;
		float t2 = t * t;
		float u2 = u * u;
		float u3 = u2 * u;
		float t3 = t2 * t;


		Vector2 result = p0 * Mathf.Pow(u, 3) + p1 * (3 * Mathf.Pow(u, 2) * t) + p2 * (3 * u * Mathf.Pow(t, 2)) + p3 * Mathf.Pow(t, 3);
		return result;
	}
}
