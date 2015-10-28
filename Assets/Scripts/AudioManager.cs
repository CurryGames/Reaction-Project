using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public AudioClip laser;
    public ArcadeLogic arlogic;
    private AudioSource audioSor;
    private float myPitch;

	// Use this for initialization
	void Start () {
        arlogic = GetComponent<ArcadeLogic>();
        audioSor = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        //myPitch = arlogic.pitchRate;
        //audioSor.pitch = arlogic.pitchRate;
        audioSor.pitch += 0.0083f * Time.deltaTime;
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
