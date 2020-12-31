using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] AudioClip musicLevel;
    private void Awake()
    {
        AudioManager.instance.PlayMusic(musicLevel);
    }

}
