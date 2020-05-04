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
		AudioSource audioSource = GetComponent<AudioSource>();
		audioSource.pitch = UnityEngine.Random.Range(0.7f, 1.3f);
		audioSource.PlayOneShot(audioSource.clip);
		GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<CircleCollider2D>().enabled = false;

		StartCoroutine(DestroyAfter(2));
	}

	private IEnumerator DestroyAfter(float seconds)
	{
		Text text = GetComponentInChildren<Text>();
		Vector3 startScale = transform.localScale;
		float startTime = Time.time;
		while (Time.time <= startTime + seconds)
		{
			transform.localScale = Vector3.Lerp(startScale, startScale * 4, Time.time - startTime);
			text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.SmoothStep(1, 0, Time.time - startTime));
			yield return null;
		}
		Destroy(gameObject);
		yield return null;
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
