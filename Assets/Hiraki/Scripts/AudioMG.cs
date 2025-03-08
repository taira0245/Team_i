using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using Unity.VisualScripting;
using AudioSetting;

/// <summary>
/// BGMとSEの管理を行う　シングルトン
/// BGMだけスクリプタブルオブジェクト用にちょっと魔改造
/// </summary>
public class AudioMG
{
    private enum E_AudioType { BGM, SE, LoopSE }

    //シングルトンinstance
    static AudioMG instance_ = null;
    public static AudioMG Instance
    { get { instance_ ??= new AudioMG(); return instance_; } }

    //SEチャンネル数
    const int BGM_CHANNEL = 1;
    const int SE_CHANNEL = 1;

    //volumeの保存・デフォルト値
    public const string BGM_VOLUME_KEY = "BGM_Volume";
    public const string SE_VOLUME_KEY = "SE_Volume";
    public const float BGM_VOLUME_DEFULT = 0.3f;
    public const float SE_VOLUME_DEFULT = 0.3f;

    //Audioファイルのパス
    const string BGM_PATH = "Audio/BGM/";
    const string SE_PATH = "Audio/SE/";
    const string LOOP_SE_PATH = "Audio/LoopSE/";

    //BGMのフェード時間
    public const float BGM_FADE_SPEED_RATE_HIGH = 0.9f;
    public const float BGM_FADE_SPEED_RATE_LOW = 0.3f;
    float bgmFadeSpeedRate_ = BGM_FADE_SPEED_RATE_HIGH;

    //次に流すAudioファイル名
    string nextBGMName_;

    //フェードアウト状態フラグ
    bool isFadeOut_ = false;
    public static bool IsFadeOut { get { return Instance.isFadeOut_; } }

    //UniTaskキャンセルトークン
    CancellationTokenSource cts;
    CancellationToken cancellationToken_;

    // サウンド再生のためのゲームオブジェクト
    GameObject object_ = null;

    //Audioリソース
    AudioSource sourceBGM_ = null;        //メインBGM用
    AudioSource[] sourceBGMArray_ = null; //BGMチャンネル用
    AudioSource sourceSEDefault_ = null;  //SEデフォルト用
    AudioSource[] sourceSEArray_ = null;  //SEチャンネル用
    AudioSource sourceLoopSE_ = null;  //LoopSE要

    //BGM,SEにアクセスするためのテーブル
    Dictionary<string, Data> poolBGM = new();
    Dictionary<string, Data> poolSE = new();
    Dictionary<string, Data> poolLoopSE = new();

    //保持するデータ
    public class Data
    {
        public string Key;      //アクセス用のキー
        public AudioClip Clip;  //AudioClip

        //コンストラクタ
        public Data(SetAudioData.AudioData audioData)
        {
            Key = audioData.Key;
            Clip = audioData.AudioFile;
            if (Clip == null) Debug.Log("AudioClip == null");
        }
    }


    //============================
    //AudioManagerのコンストラクタ
    private AudioMG()
    {
        //チャンネル確保用
        sourceBGMArray_ = new AudioSource[BGM_CHANNEL];
        sourceSEArray_ = new AudioSource[SE_CHANNEL];

        //今回は暫定的に、ゲームを起動するたびに音量をDefault値に設定する
        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, BGM_VOLUME_DEFULT);
        PlayerPrefs.SetFloat(SE_VOLUME_KEY, SE_VOLUME_DEFULT);
    }

    //AudioSourceを取得する
    AudioSource GetAudioSource(E_AudioType type, int channel = -1)
    {
        if (object_ == null) { AudioMGInit(); }

        switch (type) {
            case E_AudioType.BGM:
                //BGMでチャンネル指定がされた場合はサブの方を使用する
                if (0 <= channel) { return sourceBGMArray_[channel]; }
                else { return sourceBGM_; }
            case E_AudioType.SE:
                //SEのチャンネル指定
                if (0 <= channel && channel < SE_CHANNEL) { return sourceSEArray_[channel]; }
                else { return sourceSEDefault_; }
            case E_AudioType.LoopSE:
                return sourceLoopSE_;
            default: break;
        }
        Debug.Log("GetAudioSource.Err");
        return null;
    }


    // 初期化
    void AudioMGInit()
    {
        CreateObj();
        InitUniTask();

        //AudioSourceを作成・パラメータ設定
        sourceBGM_ = object_.AddComponent<AudioSource>();
        sourceBGM_.playOnAwake = false;
        sourceBGM_.loop = true;
        for (int i = 0; i < BGM_CHANNEL; i++) {
            sourceBGMArray_[i] = object_.AddComponent<AudioSource>();
            sourceBGMArray_[i].playOnAwake = false;
            sourceBGMArray_[i].loop = true;
        }

        sourceSEDefault_ = object_.AddComponent<AudioSource>();
        sourceSEDefault_.playOnAwake = false;
        sourceSEDefault_.loop = false;
        for (int i = 0; i < SE_CHANNEL; i++) {
            sourceSEArray_[i] = object_.AddComponent<AudioSource>();
            sourceSEArray_[i].playOnAwake = false;
            sourceSEArray_[i].loop = false;
        }

        sourceLoopSE_ = object_.AddComponent<AudioSource>();
        sourceLoopSE_.playOnAwake = false;
        sourceLoopSE_.loop = true;


        //SoundDataの読込
        string path = "Audio/";
        string[] fileNames = { "BGM_Setting", "SE_Setting", "LoopSE_Setting" };
        var array = new Dictionary<string, Data>[] { poolBGM, poolSE, poolLoopSE };
        for (int i = 0; i < fileNames.Length; i++) {
            var BGMData = Resources.Load<SetAudioData>(path + fileNames[i]);
            foreach (var data in BGMData.AudioList) {
                var key = data.Key;
                if (array[i].ContainsKey(key)) array[i].Remove(key);
                array[i].Add(key, new Data(data));
            }
            Debug.Log(fileNames[i] + ".Count : " + array[i].Count);
        }


        //ChangeVolume()を利用してvolumeを設定
        ChangeVolume(PlayerPrefs.GetFloat(BGM_VOLUME_KEY), PlayerPrefs.GetFloat(SE_VOLUME_KEY));
    }

    void CreateObj()
    {
        object_ = new GameObject("AudioMG");
        UnityEngine.Object.DontDestroyOnLoad(object_);
    }

    void InitUniTask()
    {
        // CancellationTokenSourceの生成 
        cts = new CancellationTokenSource();

        // CancellationTokenをCancellationTokenSourceから取得  
        cancellationToken_ = cts.Token;
    }

    //poolの初期化
    public static void ClearBGMData() { Instance.poolBGM.Clear(); }
    public static void ClearSEData() { Instance.poolSE.Clear(); }
    public static void ClearLoopSEData() { Instance.poolLoopSE.Clear(); }



    //====================
    //  SE
    /// <summary>
    /// SEの再生
    /// </summary>
    /// <param name="channel"></param>　指定した場合、Play()で再生
    public static bool PlaySE(string key, int channel = -1, float pitch = 1.0f) { return Instance.SetPlaySE(key, channel, pitch); }

    public bool SetPlaySE(string key, int channel = -1, float pitch = 1.0f)
    {
        if (0 <= channel && channel < SE_CHANNEL) {
            //チャンネル指定
            var source_ = GetAudioSource(E_AudioType.SE, channel);
            //対応するキーがない
            if (!poolSE.ContainsKey(key)) { Debug.Log("SE.keyName." + key + " == null"); return false; }
            //リソースの取得
            var data_ = poolSE[key];

            source_.pitch = pitch;
            source_.clip = data_.Clip;
            source_.Play();
        }
        else {
            var source_ = GetAudioSource(E_AudioType.SE);
            //対応するキーがない
            if (!poolSE.ContainsKey(key)) { Debug.Log("SE.keyName." + key + " == null"); return false; }
            //リソースの取得
            var data_ = poolSE[key];

            //デフォルトで再生(PlayOneShot()で再生)
            source_.PlayOneShot(data_.Clip);
        }
        return true;
    }

    //LoopSE
    public static bool PlayLoopSE(string key) { return Instance.SetPlayLoopSE(key); }
    public bool SetPlayLoopSE(string key)
    {
        var source_ = GetAudioSource(E_AudioType.LoopSE);
        if (source_.isPlaying) return false;

        //対応するキーがない
        if (!poolLoopSE.ContainsKey(key)) { Debug.Log("LoopSE.keyName." + key + " == null"); return false; }
        //リソースの取得
        var data_ = poolLoopSE[key];

        source_.clip = data_.Clip;
        source_.Play();
        return true;
    }
    public static bool StopLoopSE() { return Instance.SetStopLoopSE(); }
    bool SetStopLoopSE()
    {
        var source_ = GetAudioSource(E_AudioType.LoopSE);
        if (!source_.isPlaying) return false;
        source_.Stop();
        return true;
    }

    //=====================
    //  BGM
    public static bool IsPlayingBGM(int channel = -1) { return Instance.GetAudioSource(E_AudioType.BGM, channel).isPlaying; }

    //BGMの再生
    public static bool PlayBGM(string key, float fadeSpeedRate = BGM_FADE_SPEED_RATE_HIGH, int channel = -1) { return Instance.SetPlayBGM(key, fadeSpeedRate, channel); }

    bool SetPlayBGM(string key, float fadeSpeedRate = BGM_FADE_SPEED_RATE_HIGH, int channel = -1)
    {
        var source_ = GetAudioSource(E_AudioType.BGM, channel);

        if (!poolBGM.ContainsKey(key)) {
            Debug.Log(key + "BGM.keyName." + key + " == null");
            Debug.Log("poolBGM.Count : " + poolBGM.Count);
            return false;
        }

        //リソースの取得
        var data_ = poolBGM[key];

        //再生
        //現在BGMが流れていない場合→そのまま流す
        if (!source_.isPlaying) {
            nextBGMName_ = "";
            source_.clip = data_.Clip;
            source_.Play();
        }
        //流れている場合→フェードさせて流す　同じファイルの場合はスルー
        else if (source_.clip.name != data_.Clip.name) {
            nextBGMName_ = key;
            FadeOutBGM(fadeSpeedRate);
            //Debug.Log(" current.clip.name:" + source_.clip.name + ", next.clip.name:" + data_.Clip.name);
        }
        return true;
    }

    //BGMをすぐに止める
    public static bool StopBGM(int channel = -1) { return Instance.SetStopBGM(channel); }
    bool SetStopBGM(int channel = -1)
    {
        GetAudioSource(E_AudioType.BGM, channel).Stop();
        return true;
    }

    /// <summary>
    /// BGMのフェードアウト(Stop)
    /// </summary>
    /// <param name="fadeTime"></param> フェード時間
    /// <param name="channel"></param>  フェードさせるBGMチャンネル
    public static void StopFadeOutBGM(float fadeTime = 0.8f, int channel = -1) { Instance.SetStopFadeOut(fadeTime, channel); }
    async void SetStopFadeOut(float fadeTime, int channel)
    {
        //bgmFadeSpeedRate_ = fadeTime;
        await StopFadeOut(cancellationToken_, fadeTime, channel);
    }
    async UniTask StopFadeOut(CancellationToken token, float fadeTime, int channel)
    {
        isFadeOut_ = true;
        var source_ = GetAudioSource(E_AudioType.BGM, channel);
        if (source_.volume <= 0 || fadeTime <= 0) { cts.Cancel(); }
        var fadeRate = source_.volume / fadeTime;

        while (isFadeOut_) {

            if (source_ == null) { Debug.Log("source_ == null"); cts.Cancel(); }
            await UniTask.Yield(PlayerLoopTiming.Update, token);
            source_.volume -= Time.deltaTime * fadeRate;
            if (source_.volume <= 0) {
                source_.Stop();
                source_.volume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, BGM_VOLUME_DEFULT);
                isFadeOut_ = false;
            }
        }
    }

    /// <summary>
    /// 現在流れているAudioをフェードアウトさせ,次を再生する
    /// </summary>
    /// <param name="fadeSpeedRate"></param> 指定した割合でフェードアウトスピードが変わる
    public async void FadeOutBGM(float fadeSpeedRate = BGM_FADE_SPEED_RATE_LOW, int channel = -1)
    {
        bgmFadeSpeedRate_ = fadeSpeedRate;
        await FadeOutFlow(cancellationToken_, E_AudioType.BGM, channel);
    }

    async UniTask FadeOutFlow(CancellationToken token, E_AudioType type, int channel = -1)
    {
        isFadeOut_ = true;
        var source_ = GetAudioSource(E_AudioType.BGM, channel);

        while (isFadeOut_) {

            if (source_ == null) { Debug.Log("source_ == null"); cts.Cancel(); }
            await UniTask.Yield(PlayerLoopTiming.Update, token);
            source_.volume -= Time.deltaTime * bgmFadeSpeedRate_;
            if (source_.volume <= 0) {
                source_.Stop();
                source_.volume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, BGM_VOLUME_DEFULT);
                isFadeOut_ = false;
            }
        }

        if (string.IsNullOrEmpty(nextBGMName_)) { Debug.Log(nextBGMName_ + "はnullの可能性があります"); }
        PlayBGM(nextBGMName_);
    }

    //========================
    // 音量変化
    /// <summary>
    /// BGMとSEのvolumeを別々に変更＆保存 (SEは全てのチャンネルを一括変更)
    /// </summary>
    /// <param name="BGMvolume"></param> 変更後のBGMボリュームの値
    /// <param name="SEvolume"></param>　変更後のSEボリュームの値
    public static void ChangeVolume(float BGMvolume, float SEvolume) { Instance.SetBGMVolume(BGMvolume); Instance.SetSEVolume(SEvolume); }

    /// <summary>
    /// BGMのvolume変更
    /// </summary>
    /// <param name="BGMvolume"></param>
    public static void ChangeBGMVolume(float BGMvolume) { Instance.SetBGMVolume(BGMvolume); }
    void SetBGMVolume(float volume)
    {
        Debug.Log("呼ばれた！");
        sourceBGM_.volume = volume;
        for (int i = 0; i < BGM_CHANNEL; i++) { sourceBGMArray_[i].volume = volume; }
        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, volume);
    }

    /// <summary>
    /// SEのvolume変更
    /// </summary>
    /// <param name="SEvolume"></param>
    public static void ChangeSEVolume(float SEvolume) { Instance.SetSEVolume(SEvolume); }
    void SetSEVolume(float volume)
    {
        sourceSEDefault_.volume = volume;
        for (int i = 0; i < SE_CHANNEL; i++) { sourceSEArray_[i].volume = volume; }
        sourceLoopSE_.volume = volume;
        PlayerPrefs.SetFloat(SE_VOLUME_KEY, volume);
    }
}
