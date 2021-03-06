﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusReceiver : MonoBehaviour
{
    private Damageable damageable;
    private FireManager fireManager;
    private Controller controller;
    public float shieldTime = 5f;
    public float speedTime = 5f;
    public float crazyBombTime = 5f;
    public GameObject shield;

    private void Awake()
    {
        damageable = GetComponent<Damageable>();
        fireManager = GetComponentInChildren<FireManager>();
        controller = GetComponent<Controller>();
    }

    private void Start()
    {
        shield.SetActive(false);
    }

    internal void ReceiveBonus(Bonus.BonusType bonusType)
    {
        switch (bonusType)
        {
            case Bonus.BonusType.Armor:
                damageable.UpgradeArmor();
                break;
            case Bonus.BonusType.FireRate:
                fireManager.fireRate *= 1.5f;
                break;
            case Bonus.BonusType.CannonUpgrade:
                fireManager.UpgradeLevel();
                break;
            case Bonus.BonusType.Health:
                damageable.Regen(20f);
                break;
            case Bonus.BonusType.Life:
                GameManager.Instance.GainLife();
                break;
            case Bonus.BonusType.Shield:
                StartCoroutine(Shield(shieldTime));
                break;
            case Bonus.BonusType.Speed:
                StartCoroutine(SpeedBoost());
                break;
            case Bonus.BonusType.ClearAll:
                foreach (var enemy in GameObject.FindObjectsOfType<Enemy>())
                {
                    enemy.GetComponent<Damageable>().Die();
                }
                break;
            case Bonus.BonusType.FullRegen:
                damageable.Regen(100f);
                break;
            case Bonus.BonusType.CrazyBomb:
                StartCoroutine(CrazyBomb(crazyBombTime));
                break;
            default:
                break;
        }
    }

    private IEnumerator SpeedBoost()
    {
        controller.speedX *= 1.5f;
        controller.speedY *= 1.5f;
        yield return new WaitForSeconds(speedTime);
        controller.speedX /= 1.5f;
        controller.speedY /= 1.5f;
        yield return null;
    }

    public IEnumerator Shield(float _shieldTime)
    {
        yield return new WaitUntil(() => !damageable.isShielded);
        damageable.isShielded = true;
        shield.SetActive(true);
        yield return new WaitForSeconds(_shieldTime);
        if(damageable) damageable.isShielded = false;
        if (shield) shield.SetActive(false);
        yield return null;
    }

    public IEnumerator CrazyBomb(float _crazyBombTime)
    {
        yield return new WaitUntil(() => !fireManager.isCrazy);
        fireManager.setCrazy(true);
        yield return new WaitForSeconds(_crazyBombTime);
        if (fireManager) fireManager.setCrazy(false);
        yield return null;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            damageable.Die();
        }
    }

}
