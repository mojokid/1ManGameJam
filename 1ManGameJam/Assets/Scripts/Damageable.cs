using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public float health = 100f;
    public float armor = 1f;
    public bool isShielded = false;

    public void TakeDamage(float damage)
    {
        float damageToDeal = damage / armor;
        if (!isShielded)
        {
            health -= damageToDeal;
            if (health <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        Explode();
    }

    private void Explode()
    {
        // TODO: add some explosion
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damageable otherGuy = collision.gameObject.GetComponent<Damageable>();
        float damageToDeal = Mathf.Min(otherGuy.health, health);
        damageToDeal *= otherGuy.armor;
        if (!isShielded)
        {
            if (collision.gameObject.layer != gameObject.layer && otherGuy)
            {
                StartCoroutine(DealDamageWithDelay(damageToDeal, otherGuy));
            }
        }
        else
            otherGuy.Die();
    }

    internal void UpgradeArmor()
    {
        armor *= 1.2f;
    }

    private IEnumerator DealDamageWithDelay(float damageToDeal, Damageable dealTo)
    {
        yield return new WaitForSeconds(0.02f);
        if (dealTo) dealTo.TakeDamage(damageToDeal);
        yield return null;
    }
}
