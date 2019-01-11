using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public static Furniture selectedFurniture = null;
	public int speed;

	private void Update()
	{
		if (Input.GetKey(KeyCode.A))
		{
			transform.position -= new Vector3(1, 0, 0) * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.D))
		{
			transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
		}
		if (Input.GetKeyDown(KeyCode.E))
		{
			if (selectedFurniture != null)
				selectedFurniture.UseFurniture();
		}

		#region Debug Commands
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			GameManager.inst.UseTurn(1);
		}
		if (Input.GetKeyDown(KeyCode.P))
		{
			GameManager.inst.GetResource(100000, 100000, 100000, 100000, 100000, 100000);
		}
		#endregion
	}
}
