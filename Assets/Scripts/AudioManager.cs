using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public AudioClip laser;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // FUNCION PLAY: REPRODUCE UN SONIDO 
    public void Play(AudioClip audio, AudioSource audioSource, float volum)
    {

        // AGREGAMOS EL COMPONENTE AUDIOSOURCE AL GAMEOBJECT DATALOGIC
        //AudioSource audioSource = gameObject.AddComponent<AudioSource> ();
        // CARGAMOS EL CLIP
        audioSource.clip = audio;
        // PONEMOS EL VOLUMEN A TOPE
        audioSource.volume = volum;
        // REPRODUCIMOS EL SONIDO
        audioSource.Play();
        // DESTRUIMOS EL AUDIOSOURCE UNA VEZ ACABADO EL SONIDO
        Destroy(audioSource, audio.length);
    }

    public void PlayLoop(AudioClip audio, AudioSource audioSource, float volum)
    {

        // AGREGAMOS EL COMPONENTE AUDIOSOURCE AL GAMEOBJECT DATALOGIC
        //AudioSource audioSource = gameObject.AddComponent<AudioSource> ();
        // CARGAMOS EL CLIP
        audioSource.clip = audio;
        audioSource.loop = true;
        // PONEMOS EL VOLUMEN A TOPE
        audioSource.volume = volum;
        // REPRODUCIMOS EL SONIDO
        audioSource.Play();

    }
}
