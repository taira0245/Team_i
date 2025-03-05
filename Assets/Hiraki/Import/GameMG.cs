using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.Serialization.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameMG
{
    //インスタンス
    static GameMG instance = null;
    GameObject object_ = null;
    private GameMG() { }
    public static GameMG Instance { get { instance ??= new GameMG(); return instance; } }

    //--------------------------------------
    public static void GameGMInit()
    {
        if (Instance.object_ == null) {
            Instance.CreateObj();
            
            //Audioを初期化
            Instance.AudioDataInit();

            //JsonFileの読込
            Instance.LoadJson();
        }
    }

    //ゲームオブジェクトの作成
    void CreateObj()
    {
        object_ = new GameObject("GameMG");
        GameObject.DontDestroyOnLoad(object_);
    }

    //==================
    // AudioDataList
    //Audioデータの初期化(一括で読み込み)
    void AudioDataInit()
    {
        //BGM
        //AudioMG.LoadBGM("Title", "Title"); //Title

        //操作系SE
        //AudioMG.LoadSE("Cancel", "cancel"); //キャンセル
    }



    //================================================================
    //  JsonFileData
    //================================================================

    //Jsonデータのインスタンス

    //FilePath
    const string PLAYER_DATA_PATH = "PlayerData";
    const string FISH_RECORD_PATH = "FishRecordData";

    /// <summary>
    /// JsonFileの読込
    /// </summary>
    void LoadJson()
    {
        
    }

    //各JsonFileのセーブ

    /// <summary>
    /// Jsonデータの初期化
    /// </summary>
    public static void ResetJsonData()
    {
        
    }
}


