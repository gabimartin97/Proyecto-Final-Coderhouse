using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static SoundsManager instance;
    private static AudioSource sound;
    public enum globalSounds
    {
        monsterDeath,
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
        sound = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(AudioClip clip, float pitch = 1f)
    {

        sound.clip = clip;
        sound.pitch = pitch;
        sound.Play();

        return;
    }

}
