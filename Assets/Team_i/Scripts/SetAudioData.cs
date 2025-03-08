using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioSetting
{
    [CreateAssetMenu(menuName = "SetAuidoData")]
    public class SetAudioData : ScriptableObject
    {
        [System.Serializable]
        public class AudioData
        {
            [Header("音を使用する際のキーワード")]
            [Tooltip("直感的にどんな場面で使用するか分かる名前(アルファベット)を入力")]
            [SerializeField] string key;
            public string Key { get { return key; } }

            [Tooltip("音源ファイル")]
            [SerializeField] AudioClip audioFile;
            public AudioClip AudioFile { get { return audioFile; } }
        }

        public List<AudioData> AudioList;
    }
}