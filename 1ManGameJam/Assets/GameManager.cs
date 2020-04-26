using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public GameObject spaceshipPrefab;
    public GameObject playerSpaceship;
    public Transform playerSpawnPoint;
    public Text livesText;
    public int currentLives = 3;
    public Slider healthBar;


    private void Awake()
    {
        if( _instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void OnEnable()
    {
        if(playerSpaceship)
        {
            playerSpaceship.GetComponent<Damageable>().OnPlayerDead += HandlePlayerDeath;
            playerSpaceship.GetComponent<Damageable>().OnHealthChanged += HandleDamage;
        }
    }

    private void HandleDamage(float damage)
    {
        if(playerSpaceship)
        {
            healthBar.value -= damage;
        }
    }

    private void HandlePlayerDeath()
    {
        if(playerSpaceship)
        {
            playerSpaceship.GetComponent<Damageable>().OnHealthChanged -= HandleDamage;
            playerSpaceship.GetComponent<Damageable>().OnPlayerDead -= HandlePlayerDeath;
        }
        currentLives--;
        if(currentLives < 0)
        {
            GameOver();
        }
        else
        {
            livesText.text = "LIVES: " + currentLives;
            StartCoroutine(NewSpaceShip());
        }
    }

    IEnumerator NewSpaceShip()
    {
        yield return new WaitForSeconds(2f);
        playerSpaceship = Instantiate(spaceshipPrefab);
        healthBar.value = playerSpaceship.GetComponent<Damageable>().health;
        playerSpaceship.GetComponent<Damageable>().OnPlayerDead += HandlePlayerDeath;
        playerSpaceship.GetComponent<Damageable>().OnHealthChanged += HandleDamage;
        yield return null;
    }

    private void GameOver()
    {
        //TODO: GAME OVER
    }
}
