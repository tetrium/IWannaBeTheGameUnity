using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    private float _musicVolume =-1;
    [Range(0,1)]
    [SerializeField] float musicVolume = 1;
    [Range(0, 1)]

    [SerializeField] float sfxVolume = 1;

    private float _sfxVolume = -1;


    private static AudioManager _instance;


    [SerializeField] AudioSource audioSourceMusic;

    [SerializeField] AudioSource audioSourceSfx;
    private  AudioClip currentMusic;
    public static AudioManager instance {

        get {
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType<AudioManager>();
                if (!_instance) {

                    var gameObject = Resources.Load("Audio/AudioManager") as GameObject;
                    gameObject = Instantiate(gameObject, Vector3.zero, Quaternion.identity);
                    _instance = gameObject.GetComponent< AudioManager>();
                }


                DontDestroyOnLoad(_instance);
            }


            return _instance;


        }
        
    
    }

    private void Awake()
    {
        if (musicVolume != _musicVolume)
        {
            _musicVolume = musicVolume;
            audioSourceMusic.volume = musicVolume;

        }
        if (sfxVolume != _sfxVolume)
        {
            _sfxVolume = sfxVolume;
            audioSourceSfx.volume = sfxVolume;
        }
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        }
    }


    public void PlaySfx(AudioClip audioClip) {
        if (audioClip != null)
        {
            audioSourceSfx.PlayOneShot(audioClip, audioClip.length);
        }
    }
    IEnumerator VolumeFadeUp() {
        audioSourceMusic.volume = 0;
        while (audioSourceMusic.volume < musicVolume) {
            audioSourceMusic.volume += 0.01f;
            yield return false;
        }
        audioSourceMusic.volume = musicVolume;

    }

    IEnumerator VolumeFadeDown() {
        var initialVol = audioSourceMusic.volume;
        audioSourceMusic.volume = initialVol;
        while (audioSourceMusic.volume > 0)
        {
            audioSourceMusic.volume -= 0.01f;
            yield return false;
        }
        /*audioSourceMusic.volume = 0;

        audioSourceMusic.clip = currentMusic;
        audioSourceMusic.loop = true;
        audioSourceMusic.Play();*/
    }

    IEnumerator ChangeMusicEffect() {
        Debug.Log("ChangeMusicEffect");
        yield return VolumeFadeDown();
        audioSourceMusic.Stop();

        audioSourceMusic.clip = currentMusic;
        audioSourceMusic.loop = true;
     //   audioSourceMusic.Play();
        yield return VolumeFadeUp();
       // audioSourceMusic.clip = currentMusic;
       // audioSourceMusic.loop = true;
        audioSourceMusic.Play();


    }

    void LateUpdate()
    {
        CheckVolume();


    }
    void CheckVolume() {
        if (musicVolume != _musicVolume)
        {
            _musicVolume = musicVolume;
            audioSourceMusic.volume = musicVolume;

        }
        if (sfxVolume != _sfxVolume)
        {
            _sfxVolume = sfxVolume;
            audioSourceSfx.volume = sfxVolume;
        }
    }

    public void StopMusic() {

        StartCoroutine(VolumeFadeDown());

    }

    public void PlayMusic(AudioClip audioClip) {
        if (audioClip!=null&& audioSourceMusic.clip != audioClip)
        {
            StopAllCoroutines();
            if (audioSourceMusic.clip != null)
            {
              
                StartCoroutine(ChangeMusicEffect());
               
            }
            else {
                StartCoroutine(VolumeFadeUp());
                audioSourceMusic.clip = audioClip;
                audioSourceMusic.loop = true;
                audioSourceMusic.Play();

            }


            currentMusic = audioClip;
            Debug.Log("currentMusic "+ currentMusic.name);


        }
        CheckVolume();
    }
}
