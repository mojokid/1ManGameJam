using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bonus : MonoBehaviour
{
	public Text bonusText;

	private void Awake()
	{
		bonusText = GetComponentInChildren<Text>();
	}

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
		bonusText.text = _bonusType.ToString();
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

		switch (_bonusType)
		{
			case BonusType.Armor:
				spriteRenderer.color = Color.grey;
				break;
			case BonusType.FireRate:
				spriteRenderer.color = Color.yellow;
				break;
			case BonusType.CannonUpgrade:
				spriteRenderer.color = Color.red;
				break;
			case BonusType.Health:
				spriteRenderer.color = Color.white;
				break;
			case BonusType.Life:
				spriteRenderer.color = Color.cyan;
				break;
			case BonusType.Shield:
				spriteRenderer.color = Color.black;
				break;
			case BonusType.Speed:
				spriteRenderer.color = Color.blue;
				break;
			case BonusType.ClearAll:
				spriteRenderer.color = Color.magenta;
				break;
			case BonusType.FullRegen:
				spriteRenderer.color = new Color(0.5f, 0.3f, 0.7f);
				break;
			case BonusType.CrazyBomb:
				spriteRenderer.color = new Color(0.1f, 0.2f, 0.6f);
				break;
			default:
				break;
		}
	}
}
