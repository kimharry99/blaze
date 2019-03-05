using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public static FurnitureType selectedFurniture;
	public int speed;
	public Animator playerAnimator;

	private void Update()
	{
		playerAnimator.SetBool("IsWalking", false);
		if (Input.GetKey(KeyCode.A))
		{
			transform.position -= new Vector3(1, 0, 0) * speed * Time.deltaTime;
			GetComponent<SpriteRenderer>().flipX = true;
			playerAnimator.SetBool("IsWalking", true);
		}
		if (Input.GetKey(KeyCode.D))
		{
			transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
			GetComponent<SpriteRenderer>().flipX = false;
			playerAnimator.SetBool("IsWalking", true);
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
		if (Input.GetKeyDown(KeyCode.Insert))
		{
			TurnManager.inst.UseTurn(96);
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
		if (Input.GetKeyDown(KeyCode.N))
		{
			GameManager.inst.StartTask(null, 192, true);
		}
        #endregion
    }
}
