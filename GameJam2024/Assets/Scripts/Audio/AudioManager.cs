using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] playerSounds;
    public Sound[] enemySounds;
    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            SetSoundValues(s);
        }

        foreach (Sound s in enemySounds)
        {
            SetSoundValues(s);
        }

        foreach (Sound s in playerSounds)
        {
            SetSoundValues(s);
        }
    }
    void Start()
    {
        Instance = this;
    }

    public void Play(string name, string soundType="sounds")
    {
        Sound s;
        switch (soundType)
        {
            case "sounds":
                s = Array.Find(sounds, sounds => sounds.name == name);
                s.source.Play();
                break;
            case "player":
                s = Array.Find(playerSounds, playerSounds => playerSounds.name == name);
                s.source.Play();
                break;
            case "enemy":
                s = Array.Find(enemySounds, enemySounds => enemySounds.name == name);
                s.source.Play();
                break;
        }
    }

    private void SetSoundValues(Sound s)
    {
        s.source = gameObject.AddComponent<AudioSource>();
        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
    }
}
