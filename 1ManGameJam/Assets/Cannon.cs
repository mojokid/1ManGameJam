using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject projectile;
    public int level;
    public float damageMultiplier = 1;

    public void Shoot()
    {
        if (level >= 0)
        {
            GameObject shot = Instantiate(projectile, transform.position, transform.rotation);
            shot.GetComponent<Projectile>().DamageAmount *= damageMultiplier;
            shot.GetComponent<Projectile>().shooterLayer = gameObject.layer;
            shot.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, 1000f));
        }
    }
}
