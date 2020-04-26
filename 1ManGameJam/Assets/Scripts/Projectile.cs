using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int shooterLayer;
    public float DamageAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Damageable>() && collision.gameObject.layer != shooterLayer)
        {
            collision.gameObject.GetComponent<Damageable>().TakeDamage(DamageAmount);
            Explode();
        }
    }

    private void Explode()
    {
        // TODO: add effect
        Destroy(gameObject);
    }
}
