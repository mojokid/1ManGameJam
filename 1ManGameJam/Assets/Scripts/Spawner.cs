using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public GameObject enemyPrefab;
    public GameObject bonusPrefab;
    public float timeForNextBonus = 5f;
    public float minBonusTime = 5f;
    public float maxBonusTime = 10f;


    public enum SpawnState
    {
        SPAWNING,
        WAITING,
        COUNTING
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public int enemyLevel;
        public int count;
    }

    [System.Serializable]
    public class Wave
    {
        public EnemyGroup[] enemyGroups;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;
    public int NextWave
    {
        get { return nextWave + 1; }
    }

    public float timeBetweenWaves = 5f;
    public float waveCountdown;
    private float searchCountdown = 1;
    public SpawnState state = SpawnState.COUNTING;

    void Start()
    {
        waveCountdown = timeBetweenWaves;
        StartCoroutine(BonusLoop());
    }

    private bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindObjectsOfType<Enemy>() == null)
            {
                return false;
            }
        }
        return true;
    }

    void Update()
    {
        if(state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
                return;
            }
            else return;
        }

        if(waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWaves(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    private void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
        if (nextWave + 1 > waves.Length -1)
        {
            nextWave = 0;
        }
        else
        {
            nextWave++;
        }
    }

    IEnumerator SpawnWaves(Wave _wave)
    {
        state = SpawnState.SPAWNING;
        for (int i = 0; i < _wave.enemyGroups.Length; i++)
        {
            for (int j = 0; j < _wave.enemyGroups[i].count; j++)
            {
                SpawnEnemy(_wave.enemyGroups[i].enemyLevel);
                yield return new WaitForSeconds(1f / _wave.rate);
            }
        }
        state = SpawnState.WAITING;
        yield break;
    }

    private void SpawnEnemy(int enemyLevel)
    {
        Transform SpawnPoint = SpawnPoints[UnityEngine.Random.Range(0, SpawnPoints.Length)];
        GameObject enemy = Instantiate(enemyPrefab, SpawnPoint.position, Quaternion.identity, SpawnPoint);
        enemy.GetComponent<Enemy>().SetEnemyLevel(enemyLevel);
    }

    private IEnumerator BonusLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeForNextBonus);
            SpawnBonus();
            timeForNextBonus = UnityEngine.Random.Range(minBonusTime, maxBonusTime);
        }
    }

    private void SpawnBonus()
    {
        Transform SpawnPoint = SpawnPoints[UnityEngine.Random.Range(0, SpawnPoints.Length)];
        GameObject bonus = Instantiate(bonusPrefab, SpawnPoint.position, Quaternion.identity, SpawnPoint);
        bonus.GetComponent<Bonus>().SetBonusType((Bonus.BonusType)UnityEngine.Random.Range(0f, Enum.GetNames(typeof(Bonus.BonusType)).Length));
    }
}
