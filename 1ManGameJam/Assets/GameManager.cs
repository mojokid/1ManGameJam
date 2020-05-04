using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject gameOverCanvas;
    public bool stoppingTime = false;

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
        //DontDestroyOnLoad(gameObject);
        //TODO: Find a way to destroy on load, and still run the OnEnable method
    }

    public void OnEnable()
    {
        if(playerSpaceship)
        {
            playerSpaceship.GetComponent<Damageable>().OnPlayerDead += HandlePlayerDeath;
            playerSpaceship.GetComponent<Damageable>().OnHealthChanged += HandleDamage;
        }
        gameOverCanvas.SetActive(false);
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

    internal void GainLife()
    {
        currentLives++;
        livesText.text = livesText.text = "LIVES: " + currentLives;
    }

    IEnumerator NewSpaceShip()
    {
        yield return new WaitForSeconds(2f);
        playerSpaceship = Instantiate(spaceshipPrefab);
        StartCoroutine(playerSpaceship.GetComponent<BonusReceiver>().Shield(2f));
        healthBar.value = playerSpaceship.GetComponent<Damageable>().health;
        playerSpaceship.GetComponent<Damageable>().OnPlayerDead += HandlePlayerDeath;
        playerSpaceship.GetComponent<Damageable>().OnHealthChanged += HandleDamage;
        yield return null;
    }

    private void GameOver()
    {
        stoppingTime = true;
        gameOverCanvas.SetActive(true);
        StartCoroutine(StopTime());
    }

    IEnumerator StopTime()
    {
        float startTime = Time.time;

        while(stoppingTime == true)
        {
            Time.timeScale = Mathf.SmoothStep(1, 0.02f, (Time.time - startTime) * 3);
            yield return null;
        }
        yield return null;
    }

    public void Restart()
    {
        stoppingTime = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        gameOverCanvas.SetActive(false);
    }
}
