using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
	public enum BonusType
	{
		Armor,
		FireRate,
		CannonUpgrade,
		Health,
		Life,
		Shield,
		Speed,
		ClearAll,
		FullRegen,
		CrazyBomb
	}

	public BonusType bonusType;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.GetComponent<BonusReceiver>())
		{
			collision.gameObject.GetComponent<BonusReceiver>().ReceiveBonus(bonusType);
			Pop();
		}
	}

	private void Pop()
	{
		//TODO: play sound
		Destroy(gameObject);
	}

	public void SetBonusType(BonusType _bonusType)
	{
		bonusType = _bonusType;
		//TODO: add text reference, and change color
	}
}
