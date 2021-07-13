using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

namespace WaterKat.AudioManagement
{
    [CreateAssetMenu(menuName = "WaterKat/AudioReference", order = -950)]
    public class AudioReference : AudioBlock
    {
        public AudioBoolVariable mute;
        public AudioFloatVariable volume;
        [Space(10)]
        public AudioReferenceData dataBlock;
        public AudioCodeBlockFloat testBlock;
    }
    [Serializable]
    public class AudioReferenceData
    {
        public AudioClip clip;
        [Space(5)]
        public AudioMixerGroup outputAudioMixerGroup;
        public bool mute;
        public bool bypassEffects;
        public bool bypassListenerEffects;
        public bool bypassReverbZones;
        public bool playOnAwake;
        public bool loop;

        [Space(5)]
        [Range(0, 256)] public int priority;

        [Space(5)]
        [Range(0, 1)] public float volume;

        [Space(5)]
        [Range(-3, 3)] public float pitch;

        [Space(5)]
        [Range(-0, 1)] public float panStereo;

        [Space(5)]
        [Range(0, 1)] public float spatialBlend;

        [Space(5)]
        [Range(0, 1.1f)] public float reverbZoneMix;
    }
    [Serializable]
    public class AudioCodeBlockBool
    {
        public enum codeblockConditions
        {
            Passthrough,
            Constant,
            OnPlay,
            PerFrame,
            OnCommand,
            BeforeTime,
            AfterTime,
        }

        public enum codeblockOps
        {
            Override,
            Add,
            Substract,
            Multiply,
            Divide,
        }

        public enum codeblockValues
        {
            Constant,
            TimeCurve,
            RandomRange,
        }

        public codeblockConditions conditions;
        public codeblockOps operators;
        public codeblockValues values;

        //public AudioReferenceData 
        public AudioCodeBlockFloat parent;

        public bool GetValue()
        {
            return false;
        }
    }
    public class AudioCodeBlockFloat
    {
        public enum codeblockConditions
        {
            Passthrough,
            Constant,
            OnPlay,
            PerFrame,
            OnCommand,
            BeforeTime,
            AfterTime,
        }

        public enum codeblockOps
        {
            Override,
            Add,
            Substract,
            Multiply,
            Divide,
        }

        public enum codeblockValues
        {
            Constant,
            TimeCurve,
            RandomRange,
        }

        public codeblockConditions conditions;
        public codeblockOps operators;
        public codeblockValues values;

        //public AudioReferenceData 
        public AudioCodeBlockFloat parent;

        public float GetValue()
        {
            if (parent == null)
            {
                return 3;
            }
            return -1;
        }

        internal float Calculate(float tempValue)
        {
            switch (conditions)
            {
                case codeblockConditions.Passthrough:
                    return tempValue;
                    break;
                case codeblockConditions.Constant:
                    break;
                case codeblockConditions.OnPlay:
                    break;
                case codeblockConditions.PerFrame:
                    break;
                case codeblockConditions.OnCommand:
                    break;
                case codeblockConditions.BeforeTime:
                    break;
                case codeblockConditions.AfterTime:
                    break;
                default:
                    break;
            }
            return tempValue;
        }
    }
    [Serializable]
    public class AudioFloatVariable
    {
        public float Value;
        public List<AudioCodeBlockFloat> audioCodeBlocks;
        public float GetModdedValue()
        {
            float tempValue = Value;
            foreach (AudioCodeBlockFloat codeBlock in audioCodeBlocks)
            {
                tempValue = codeBlock.Calculate(tempValue);
            }
            return tempValue;
        }
    }
    [Serializable]
    public class AudioBoolVariable
    {
        public bool Value;
        public List<AudioCodeBlockBool> audioCodeBlocks;
    }
}
