using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class ControlaAudio : MonoBehaviour
{
    public AudioSource meuAudioSouce;
    public AudioClip MusicaDoBoss;
    public static AudioSource instancia;

    void Awake()
    {
        meuAudioSouce = GetComponent<AudioSource>();
        instancia = meuAudioSouce;
    }
    private void Update()
    {
        if (ControlaPause.JogoPausado)
        {
            meuAudioSouce.mute = true;
        }
        else
        {
            meuAudioSouce.mute = false;
        }
    }

    public void MusicaBoss()
    {
        meuAudioSouce.clip = MusicaDoBoss;
        meuAudioSouce.Play();
    }
}
