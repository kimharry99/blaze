﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public static FurnitureType selectedFurniture;
	public int speed;
	public Animator playerAnimator;
	private float ladderMoveAmount = 2;


	private void Update()
	{
		if (PlayerState.curState != PlayerState.idle)
			return;
		if (GameManager.inst.IsGameOver)
			return;
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

#if UNITY_EDITOR
		#region Debug Commands
		if (Input.GetKeyDown(KeyCode.P))
		{
			GameManager.inst.GetResource(100000, 100000, 100000, 100000, 100000, 100000);
		}
		if (Input.GetKeyDown(KeyCode.N))
		{
			GameManager.inst.StartTask(null, 192, true);
		}
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
        if(Input.GetKeyDown(KeyCode.M))
        {
            GameManager.inst.ChangeHealth(100);
            GameManager.inst.ChangeSanity(100);
            GameManager.inst.ChangeHunger(100);
            GameManager.inst.ChangeThirst(100);
            GameManager.inst.ChangeEnergy(100);
        }
		#endregion
#endif
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.tag == "Ladder")
		{
			if (GameManager.inst.IsGameOver)
				return;
			if (PlayerState.curState != PlayerState.idle)
				return;
			if (Input.GetKey(KeyCode.W))
			{
				transform.position += new Vector3(0, 1, 0) * speed * Time.deltaTime;
				GetComponent<Rigidbody2D>().gravityScale = 0;
				//GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1f) * speed;
				gameObject.layer = LayerMask.NameToLayer("LadderPlayer");
				ladderMoveAmount -= speed * Time.deltaTime;
				if (ladderMoveAmount <= 0)
				{
					ladderMoveAmount = 2;
					GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
				}
				playerAnimator.Play("Ladder");
			}
			else if (Input.GetKey(KeyCode.S))
			{
				transform.position -= new Vector3(0, 1, 0) * speed * Time.deltaTime;
				GetComponent<Rigidbody2D>().gravityScale = 0;
				//GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1f) * speed;
				gameObject.layer = LayerMask.NameToLayer("LadderPlayer");
				ladderMoveAmount -= speed * Time.deltaTime;
				if (ladderMoveAmount <= 0)
				{
					ladderMoveAmount = 2;
					GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
				}
				playerAnimator.Play("Ladder");
			}
			else if (Input.GetKey(KeyCode.Space))
			{
				GetComponent<Rigidbody2D>().gravityScale = 5;
				gameObject.layer = LayerMask.NameToLayer("Default");
				playerAnimator.Play("Idle");
			}
			else if (gameObject.layer == LayerMask.NameToLayer("LadderPlayer"))
			{
				GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Ladder")
		{
			GetComponent<Rigidbody2D>().gravityScale = 5;
			gameObject.layer = LayerMask.NameToLayer("Default");
			playerAnimator.Play("Idle");
		}
	}
}
