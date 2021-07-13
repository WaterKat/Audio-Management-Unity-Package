using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaterKat.AudioManagement
{
    public abstract class AudioBlock : ScriptableObject
    {
        private static List<AudioBlock> audioBlocks;
        public static AudioBlock[] AllAudioBlocks { get { return audioBlocks.ToArray(); } }
    }
}
