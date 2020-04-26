using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public float health = 100f;
    public float armor = 1f;

    public void TakeDamage(float damage)
    {
        float damageToDeal = damage / armor;
        health -= damageToDeal;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Explode();
    }

    private void Explode()
    {
        // TODO: add some explosion
        Destroy(gameObject);
    }
}
