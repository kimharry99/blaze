using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform player;
	public float smoothTime = 0.3f;
	private Vector3 velocity;
	private Vector3 offset;

    void Start()
    {
		offset = transform.position - player.position;
    }

    void Update()
    {
		transform.position = Vector3.SmoothDamp(transform.position, player.position + offset, ref velocity, smoothTime);
    }
}
