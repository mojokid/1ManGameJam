using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System;

[System.Serializable]
public class Sound
{
    [SerializeField]
    public AudioClip audioClip;
    [SerializeField]
    public string name;
    
}

public class AudioManager : MonoBehaviour
{

    public Sound[] Sounds;
    public static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }
    public StudioEventEmitter emitter;
    private AudioSource audioSource;
    public Controller player;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);

        emitter = GetComponent<StudioEventEmitter>();
        audioSource = GetComponent<AudioSource>();
        InitPlayer();
    }

    private void InitPlayer()
    {
        player = FindObjectOfType<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeMusicAccordingToEnemies();
        ChangeMusicAccordingToHeight();
        ChangeMusicAccordingToRight();
    }

    private void ChangeMusicAccordingToRight()
    {
        if (player)
        {
            float NormalizedRight = (player.gameObject.transform.position.x - player.boundary.xMin) / (player.boundary.xMax - player.boundary.xMin);
            emitter.SetParameter("Right", NormalizedRight);
        }
        else
        {
            InitPlayer();
        }
    }

    private void ChangeMusicAccordingToHeight()
    {
        if (player)
        {
            float NormalizedHeight = (player.gameObject.transform.position.y - player.boundary.yMin) / (player.boundary.yMax - player.boundary.yMin);
            emitter.SetParameter("Height", NormalizedHeight);
        }
        else
        {
            InitPlayer();
        }
    }

    private void ChangeMusicAccordingToEnemies()
    {
        emitter.SetParameter("Enemies", GameObject.FindObjectsOfType<Enemy>().Length);
    }

    public void PlaySound(int soundToPlay)
    {
        audioSource.pitch = UnityEngine.Random.Range(0.1f, 2f);
        audioSource.PlayOneShot(Sounds[soundToPlay].audioClip,0.5f);
    }

    public void PlaySound(int soundToPlay, float volume)
    {
        audioSource.pitch = UnityEngine.Random.Range(0.1f, 2f);
        audioSource.PlayOneShot(Sounds[soundToPlay].audioClip, volume);
        AudioClip myClip;
    }

}
