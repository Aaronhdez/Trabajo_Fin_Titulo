using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public void PlaySound(AudioSource clip) {
        clip.Play();
    }
}
