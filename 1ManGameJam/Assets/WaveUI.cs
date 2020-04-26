using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    public Spawner spawner;
    public Text waveCountDownText;
    public Text waveCountText;

    void Update()
    {
        switch (spawner.state)
        {
            case Spawner.SpawnState.SPAWNING:
                UpdateSpawningUI();
                break;
            case Spawner.SpawnState.WAITING:
                break;
            case Spawner.SpawnState.COUNTING:
                UpdateCountingUI();
                break;
            default:
                break;
        }


    }

    private void UpdateCountingUI()
    {
        if (waveCountText.transform.parent.gameObject.activeSelf == true)
            waveCountText.transform.parent.gameObject.SetActive(false);
        if (!waveCountDownText.IsActive()) waveCountDownText.gameObject.SetActive(true);
        waveCountDownText.text = ((int)spawner.waveCountdown).ToString();
    }

    private void UpdateSpawningUI()
    {
        if (waveCountText.transform.parent.gameObject.activeSelf == false)
            waveCountText.transform.parent.gameObject.SetActive(true);
        if (waveCountDownText.IsActive()) waveCountDownText.gameObject.SetActive(false);
        waveCountText.text = spawner.NextWave.ToString();
    }
}
