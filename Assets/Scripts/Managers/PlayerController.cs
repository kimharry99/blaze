using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public static FurnitureType selectedFurniture;
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
			if (selectedFurniture != FurnitureType.None)
				HomeUIManager.inst.OpenFurnitureUI(selectedFurniture);
				
		}

		#region Debug Commands
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			TurnManager.inst.UseTurn(1);
		}
        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            TurnManager.inst.UseTurn(5);
        }
		if (Input.GetKeyDown(KeyCode.P))
		{
			GameManager.inst.GetResource(100000, 100000, 100000, 100000, 100000, 100000);
		}
        if(Input.GetKeyDown(KeyCode.M))
        {
            GameManager.inst.ChangeHealth(100);
            GameManager.inst.ChangeSanity(100);
            GameManager.inst.ChangeHunger(100);
            GameManager.inst.ChangeThirst(100);
            GameManager.inst.ChangeEnergy(100);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Health : " + GameManager.inst.Health + "\nMental : " + GameManager.inst.Sanity + "\nHunger : " + GameManager.inst.Hunger + "\nThirst : " + GameManager.inst.Thirst + "\nEnergy : " + GameManager.inst.Energy);
        }
        #endregion
    }
}
