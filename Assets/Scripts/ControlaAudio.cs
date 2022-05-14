using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaAudio : MonoBehaviour
{
    private AudioSource meuAudioSource;
    public static AudioSource instance;
    public static ControlaAudio instancia;
    // Start is called before the first frame update
    void Awake()
    {
        this.meuAudioSource = GetComponent<AudioSource>();
        instance = this.meuAudioSource;
        instancia = this;
    }

    public void PlayOneShot(AudioClip audio){
        if (audio != null && this.meuAudioSource != null) {
            this.meuAudioSource.PlayOneShot(audio);
        }
    }

}
