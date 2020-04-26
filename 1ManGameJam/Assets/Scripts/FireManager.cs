using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    public GameObject[] projectiles;
    public Cannon[] cannons;
    public float fireRate = 1;
    public bool isCrazy = false;

    private void Awake()
    {
        cannons = GetComponentsInChildren<Cannon>();
    }

    void Start()
    {
        StartCoroutine(ShotLoop());
    }

    private IEnumerator ShotLoop()
    {
        while(cannons.Length > 0)
        {
            yield return new WaitForSeconds(1 / fireRate);
            foreach (var cannon in cannons)
            {
                cannon.Shoot();
            }
        }
        yield return null;
    }

    public void UpgradeLevel()
    {
        foreach (var cannon in cannons)
        {
            cannon.level++;
            if (cannon.level >= 0)
            {
                if (projectiles.Length > cannon.level) cannon.projectile = projectiles[cannon.level];
                else
                {
                    cannon.damageMultiplier += 0.1f;
                }
            }
        }
    }

    internal void setCrazy(bool setTo)
    {
        isCrazy = setTo;
        foreach (var cannon in cannons)
        {
            cannon.isCrazy = setTo;
        }
    }
}
