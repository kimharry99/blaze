using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneCameraMovement : MonoBehaviour
{
	private Vector3 point;
	private Vector3 velocity;
	private void Update()
	{
		float x = Random.Range(-1f, 1f);
		float y = Random.Range(-0.7f, 0.7f);
		point = new Vector3(x, y, transform.position.z);
		transform.position = Vector3.SmoothDamp(transform.position, point, ref velocity, 0.5f);
	}
}
