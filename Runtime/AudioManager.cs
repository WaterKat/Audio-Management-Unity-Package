using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaterKat.AudioManagement
{
    [CreateAssetMenu(menuName = "WaterKat/AudioManager", order = -950)]

    public class AudioManager : ScriptableObject
    {
        private static List<AudioManager> _audioManagers;
        public static List<AudioManager> AllAudioManagers;

    }
}
