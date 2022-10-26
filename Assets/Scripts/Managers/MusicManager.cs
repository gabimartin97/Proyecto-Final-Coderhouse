using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MusicManager : MonoBehaviour
{

    // Start is called before the first frame update
    private static MusicManager instance;
    private static AudioSource music;
    [SerializeField] private AudioClip[] musicClips;

   
    public enum globalMusic
    {
        ambience1,
        deathMusic,
        winMusic,
        bossMusic,
        count
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {
        PlayerBehaviour.OnDead += OnplayerDeath;
        BossBehaviour.OnDead += OnBossDeath;
        BossFightTrigger.OnBossFightStart += OnBossFight;


        music = GetComponent<AudioSource>();
        music.clip = musicClips[(int)globalMusic.ambience1];
        music.loop = true;
        music.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnplayerDeath()
    {
        music = GetComponent<AudioSource>();
        music.clip = musicClips[(int)globalMusic.deathMusic];
        music.loop = false;
        music.Play();
    }

    void OnBossDeath()
    {
        music = GetComponent<AudioSource>();
        music.clip = musicClips[(int)globalMusic.winMusic];
        music.loop = false;
        music.Play();
    }

    void OnBossFight()
    {
        music = GetComponent<AudioSource>();
        music.clip = musicClips[(int)globalMusic.bossMusic];
        music.loop = true;
        music.Play();
    }
}
