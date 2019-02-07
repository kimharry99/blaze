using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	[SerializeField]
	private float speed = 5;
	private Vector3 preMousePos;

    private void Update()
    {
		if (Input.GetMouseButton(1))
			transform.Translate((preMousePos - Input.mousePosition) * 0.01f);
		preMousePos = Input.mousePosition;

		if (Input.GetKey(KeyCode.A))
			transform.position += Vector3.left * speed * Time.deltaTime;
		if (Input.GetKey(KeyCode.D))
			transform.position += Vector3.right * speed * Time.deltaTime;
		if (Input.GetKey(KeyCode.W))
			transform.position += Vector3.up * speed * Time.deltaTime;
		if (Input.GetKey(KeyCode.S))
			transform.position += Vector3.down * speed * Time.deltaTime;
		Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - Input.mouseScrollDelta.y, 1, 10);
	}
}
