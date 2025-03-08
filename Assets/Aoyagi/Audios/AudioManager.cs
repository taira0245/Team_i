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

    void Awake()
    {
        //ÉZÅ[ÉuÇµÇƒÇ¢ÇΩâπó Çì«Ç›çûÇ›
        float bgmValue = PlayerPrefs.GetFloat("BGM_Volume", 0);
        float seValue = PlayerPrefs.GetFloat("SE_Volume", 0);
        //âπó ÇAudioÇ∆UIÇ…îΩâf
        audioMixer.SetFloat("BGM_Volume", ConvertVolume2dB(bgmValue));
        audioMixer.SetFloat("SE_Volume", ConvertVolume2dB(seValue));
        if (bgmSlider != null) bgmSlider.value = bgmValue;
        if (seSlider != null) seSlider.value = seValue;
    }

    float ConvertVolume2dB(float volume) => Mathf.Clamp(20f * Mathf.Log10(Mathf.Clamp(volume, 0f, 1f)), -80f, 0f);

    //BGMÇÃâπó í≤êﬂ
    public void SetBGM(float value)
    {
        PlayerPrefs.SetFloat("BGM_Volume", value);
        audioMixer.SetFloat("BGM_Volume", ConvertVolume2dB(value));
        AudioMG.ChangeBGMVolume(PlayerPrefs.GetFloat("BGM_Volume"));
    }

    //SEÇÃâπó í≤êﬂ
    public void SetSE(float value)
    {
        PlayerPrefs.SetFloat("SE_Volume", value);
        audioMixer.SetFloat("SE_Volume", ConvertVolume2dB(value));
        AudioMG.ChangeSEVolume(PlayerPrefs.GetFloat("SE_Volume"));
    }
}
