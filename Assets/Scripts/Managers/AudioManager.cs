using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private Sound[] sounds;
    private AudioSource[] source;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        AudioSourceAdd();
    }
    private void OnEnable()
    {
        StackController.OnStackCombo += IncreasePitch;
        StackController.OnStackComboReset += ResetPitch;
    }
    private void OnDisable()
    {
        StackController.OnStackCombo += IncreasePitch;
        StackController.OnStackComboReset += ResetPitch;
    }
    public void AudioSourceAdd()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].source = this.gameObject.AddComponent<AudioSource>();
            sounds[i].source.clip = sounds[i].clip;
            sounds[i].source.loop = sounds[i].loop;
            sounds[i].source.pitch = sounds[i].pitch;
            sounds[i].source.playOnAwake = false;
        }
    }
    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, x => x.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound not found!");
        }
        else
        {
            if (Array.Exists(sounds, element => element.name == name))
                Array.Find(sounds, sound => sound.name == name).source.Play();
        }
    }
    public void IncreasePitch()
    {
        if (Array.Exists(sounds, element => element.name == "Place"))
            Array.Find(sounds, sound => sound.name == "Place").source.pitch += 0.1f;
    }
    public void ResetPitch()
    {
        if (Array.Exists(sounds, element => element.name == "Place"))
        {
            var sound = Array.Find(sounds, sound => sound.name == "Place");
            sound.source.pitch=sound.pitch;
        }
    }
}



[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public AudioSource source;
    public bool loop;
    public float pitch;

}
