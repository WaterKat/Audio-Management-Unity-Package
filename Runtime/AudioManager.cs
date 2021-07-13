using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

using UnityEditor;


namespace WaterKat.AudioManagement
{
    [CreateAssetMenu(fileName = "WaterKat Audio Manager", menuName = "WaterKat/Audio Manager", order = -950)]

    public sealed class AudioManager : ScriptableObject
    {
        #region "Singleton Logic"
        private static readonly object _lockObject = new object();
        private static AudioManager _instance;
        public static AudioManager Instance {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }
                return null;
            }
            private set => _instance = value; }

        private AudioManager() { }

        private void CheckForDuplicateSingletons()
        {
            lock (_lockObject)
            {
                if ((Instance != null) && (Instance != this))
                {
                    Debug.LogWarning("Multiple instances of AudioManager.cs have been found! Only one instance is allowed per project to prevent errors.");
                    DestroyImmediate(this);
                }
                else
                {
                    Instance = this;
                }
            }
        }
        #endregion

        void OnEnable()
        {
            CheckForDuplicateSingletons();  //Necessary for Singleton Pattern

        }

        private static readonly string defaultAudioPath = "Assets/WaterKat/AudioManagement/";

        [System.Serializable]
        private class DoubleDictionary<TKey, TValue>
        {
            private Dictionary<TKey, TValue> standardDictionary = new Dictionary<TKey, TValue>();
            private Dictionary<TValue, TKey> reverseDictionary = new Dictionary<TValue, TKey>();

            public int Count => standardDictionary.Count;

            public TValue this[TKey _key]
            {
                get { return standardDictionary[_key]; }
            }
            public TKey this[TValue _value]
            {
                get { return reverseDictionary[_value]; }
            }
            public bool Add(TKey _key, TValue _value)
            {
                if (standardDictionary.ContainsKey(_key) || reverseDictionary.ContainsKey(_value))
                {
                    Debug.LogError("Double Dictionary already contains the key value pair" + _key.ToString() + ", " + _value.ToString());
                    return false;
                }

                standardDictionary.Add(_key, _value);
                reverseDictionary.Add(_value, _key);
                return true;
            }

            public bool Remove(TKey _key)
            {
                if (!standardDictionary.ContainsKey(_key))
                {
                    Debug.LogError("Double Dictionary does not contain key " + _key.ToString());
                    return false;
                }

                reverseDictionary.Remove(standardDictionary[_key]);
                standardDictionary.Remove(_key);
                return true;
            }

            public bool RemoveWithValue(TValue _value)
            {
                if (!reverseDictionary.ContainsKey(_value))
                {
                    Debug.LogError("Double Dictionary does not contain value " + _value.ToString());
                    return false;
                }

                standardDictionary.Remove(reverseDictionary[_value]);
                reverseDictionary.Remove(_value);
                return true;
            }

            public bool ContainsKey(TKey _key)
            {
                return standardDictionary.ContainsKey(_key);
            }

            public bool ContainsValue(TValue _value)
            {
                return reverseDictionary.ContainsKey(_value);
            }

            public bool TryGetValue(TKey _key, out TValue _value)
            {
                return standardDictionary.TryGetValue(_key, out _value);
            }
            public bool TryGetKey(TValue _value, out TKey _key)
            {
                return reverseDictionary.TryGetValue(_value, out _key);
            }
        }

        [SerializeField] private DoubleDictionary<string, AudioInterface> audioInterfaces = new DoubleDictionary<string, AudioInterface>();

        private GameObject audioSourceContainer;

        private void OnDisable()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
        public static int ValidateAudioInterface(AudioInterface _audioInterface)
        {
            if (Instance.audioInterfaces.TryGetKey(_audioInterface, out string possibleKey))
            {
                if (possibleKey != _audioInterface.name)
                {
                    Instance.audioInterfaces.RemoveWithValue(_audioInterface);
                    Debug.LogError("Audio Interface name does not match value! Trying to reassign key.");
                    AudioManager.RegisterAudioInterface(_audioInterface);
                    return 1;
                }
            }
            return 0;
        }


        public static int RegisterAudioInterface(AudioInterface _audioInterface)
        {
            Debug.Log(_audioInterface);
            Debug.Log(Instance);
            if (Instance.audioInterfaces.ContainsKey(_audioInterface.name))
            {
                Debug.LogError("Duplicate Audio Interface name found! Name : " + _audioInterface.name);
                return 1;
            }

            Instance.audioInterfaces.Add(_audioInterface.name, _audioInterface);

            Debug.Log("audioInterfaceCount: " + Instance.audioInterfaces.Count);
            return 0;
        }
        public static int UnRegisterAudioInterface(AudioInterface _audioInterface)
        {

            if (!Instance.audioInterfaces.ContainsKey(_audioInterface.name))
            {
                Debug.LogError("Audio Interface with name " + _audioInterface.name + " not found!");
                return 1;
            }

            Instance.audioInterfaces.Remove(_audioInterface.name);
            Debug.Log("audioInterfaceCount: " + Instance.audioInterfaces.Count);
            return 0;
        }

        private AudioInterface GetAudioClip(string _audioName)
        {
            AudioInterface audioInterface = Instance.audioInterfaces[_audioName];
            if (audioInterface == null)
            {
                Debug.LogError("Audio clip with name " + _audioName + " not found!");
            }
            return Instance.audioInterfaces[_audioName];
        }

        public static bool PlaySound(string _audioName)
        {
            AudioInterface audioInterface = Instance.GetAudioClip(_audioName);
            if (audioInterface != null) 
            {
                audioInterface.Play();
                return true;
            }
            return false;
        }

        public static bool SoundPlaying(string _audioName)
        {
            AudioInterface audioInterface = Instance.GetAudioClip(_audioName);
            if (audioInterface != null)
            {
                return audioInterface.isPlaying();
            }
            return false;
        }

        public static bool PlaySoundWithDelay(string _audioName, float _delay)
        {
            AudioInterface audioInterface = Instance.GetAudioClip(_audioName);
            if (audioInterface != null)
            {
                audioInterface.Play(_delay);
                return true;
            }
            return false;
        }
        public static bool PauseSound(string _audioName)
        {
            AudioInterface audioInterface = Instance.GetAudioClip(_audioName);
            if (audioInterface != null)
            {
                audioInterface.Pause();
                return true;
            }
            return false;
        }
        public static bool unPauseSound(string _audioName)
        {
            AudioInterface audioInterface = Instance.GetAudioClip(_audioName);
            if (audioInterface != null)
            {
                audioInterface.unPause();
                return true;
            }
            return false;
        }
        public static bool StopSound(string _audioName)
        {
            AudioInterface audioInterface = Instance.GetAudioClip(_audioName);
            if (audioInterface != null)
            {
                audioInterface.Stop();
                return true;
            }
            return false;
        }

        public static bool PlayAudioClipAtPoint(string _audioName, Vector3 _vector3Point)
        {
            AudioInterface audioInterface = Instance.GetAudioClip(_audioName);
            if (audioInterface != null)
            {
                audioInterface.PlayAtPoint(_vector3Point);
                return true;
            }
            return false;
        }
        /*
#if UNITY_EDITOR
        [ContextMenu("UpdateAssets() Warning! This WILL override CURRENT DATA!")]
        void UpdateAssets()
        {
            AudioClips.Clear();

            string[] guids = AssetDatabase.FindAssets("t:" + typeof(AudioInterface).FullName, new[] { defaultAudioPath });

            List<string> assetPaths = new List<string>();

            foreach (string guid in guids)
            {
                assetPaths.Add(AssetDatabase.GUIDToAssetPath(guid));
            }

            foreach (string assetPath in assetPaths)
            {
                Debug.Log(assetPath);
                AudioInterface audioInterface = AssetDatabase.LoadAssetAtPath(assetPath, typeof(AudioInterface)) as AudioInterface;
                AudioClips.Add(audioInterface);
            }
        }

#endif
        */
    }
}
