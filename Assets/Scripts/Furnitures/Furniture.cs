using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : ScriptableObject
{
	public string furnitureName;
	[SerializeField]
	private int level;
	public int Level {
		get {
			return level;
		}
		set
		{
			level = value;
			if (level > 0 && !GameManager.inst.IsOutside)
				GameObject.Find(furnitureName).GetComponent<SpriteRenderer>().sprite = furnitureSprites[level - 1];
		}
	}
	[SerializeField]
	protected AudioClip furnitureSFX, upgradeSFX;
	[SerializeField]
	protected Sprite[] furnitureSprites;

	public virtual void Init()
	{
		Level = 0;
	}

	public virtual void OnTurnPassed(int turn)
	{
        
	}

	public virtual void Upgrade()
	{
		SoundManager.inst.PlaySFX(upgradeSFX);
		GameManager.inst.StartTask(delegate { ++Level; }, 4);
	}

	public Sprite GetImage()
	{
		return furnitureSprites[Level - 1];
	}

	public Sprite GetImage(int level)
	{
		return furnitureSprites[level - 1];
	}
}
