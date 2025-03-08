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
            [Header("�����g�p����ۂ̃L�[���[�h")]
            [Tooltip("�����I�ɂǂ�ȏ�ʂŎg�p���邩�����閼�O(�A���t�@�x�b�g)�����")]
            [SerializeField] string key;
            public string Key { get { return key; } }

            [Tooltip("�����t�@�C��")]
            [SerializeField] AudioClip audioFile;
            public AudioClip AudioFile { get { return audioFile; } }
        }

        public List<AudioData> AudioList;
    }
}