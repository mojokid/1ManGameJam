using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Sprite[] EnemySprites;
    private FireManager fireManager;
    private Damageable damageable;

    private void Awake()
    {
        fireManager = GetComponentInChildren<FireManager>();
        damageable = GetComponent<Damageable>();
    }

    public void SetEnemyLevel(int level)
    {
        GetComponent<SpriteRenderer>().sprite = EnemySprites[level];
        damageable.health *= (level + 1);
        for (int i = 0; i < level; i++)
        {
            fireManager.UpgradeLevel();
        }

        //TODO: change amplitude according to level
        //GetComponent<AutoMovement>().maxX = 0.88f * (float)level;
    }
}
