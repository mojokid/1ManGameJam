using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public float health = 100f;
    public float armor = 1f;
    public bool isShielded = false;

    public event Action<float> OnHealthChanged;
    public event Action OnPlayerDead;

    public void TakeDamage(float damage)
    {

        float damageToDeal = damage / armor;
        if (!isShielded)
        {
            if (gameObject.tag == "Player") OnHealthChanged(damageToDeal);
            //TODO: if enemy, add points
            //TODO: add point functionality

            health -= damageToDeal;
            if (health <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        if (gameObject.tag == "Player") OnPlayerDead();
        else AudioManager.Instance.PlaySound(0);
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

    public void Regen(float amount)
    {
        float healthToRegen = Mathf.Clamp(health + amount, 0, 100) - health;
        health += healthToRegen;
        OnHealthChanged(-healthToRegen);
    }
}
