using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaterKat.AudioManagement
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceLink : MonoBehaviour
    {
        private AudioSource audioSource;
        private AudioReference audioReference;
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

    }
}
