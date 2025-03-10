using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider seSlider;

    void Start()
    {
        bgmSlider = GameObject.Find("BGMSlider").GetComponent<Slider>();
        seSlider = GameObject.Find("SESlider").GetComponent<Slider>();
        //Z[uµÄ¢½¹ŹšĒŻŻ
        float bgmValue = PlayerPrefs.GetFloat("BGM_Volume", 0);
        float seValue = PlayerPrefs.GetFloat("SE_Volume", 0);
        //¹ŹšAudioĘUIÉ½f
        audioMixer.SetFloat("BGM_Volume", ConvertVolume2dB(bgmValue));
        audioMixer.SetFloat("SE_Volume", ConvertVolume2dB(seValue));
        if (bgmSlider != null) bgmSlider.value = bgmValue;
        if (seSlider != null) seSlider.value = seValue;
    }

    float ConvertVolume2dB(float volume) => Mathf.Clamp(20f * Mathf.Log10(Mathf.Clamp(volume, 0f, 1f)), -80f, 0f);

    //BGMĢ¹Ź²ß
    public void SetBGM(float value)
    {
        PlayerPrefs.SetFloat("BGM_Volume", value);
        audioMixer.SetFloat("BGM_Volume", ConvertVolume2dB(value));
        AudioMG.SetChangeBGMVolume(PlayerPrefs.GetFloat("BGM_Volume"));
    }

    //SEĢ¹Ź²ß
    public void SetSE(float value)
    {
        PlayerPrefs.SetFloat("SE_Volume", value);
        audioMixer.SetFloat("SE_Volume", ConvertVolume2dB(value));
        AudioMG.SetChangeSEVolume(PlayerPrefs.GetFloat("SE_Volume"));
    }
}
