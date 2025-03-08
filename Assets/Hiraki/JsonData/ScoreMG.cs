using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スコアデータ管理用クラス (シングルトン)
/// </summary>
public class ScoreMG
{
    //========================
    // インスタンス関係
    //========================
    static ScoreMG instance_ = null;
    public static ScoreMG Instance
    {
        //Instanceが使用された際、instance_==nullの場合はインスタンスを生成
        get { instance_ ??= new ScoreMG(); return instance_; }
    }

    static GameObject object_ = null;
    void CreateObject()
    {
        object_ = new GameObject("SocreMG");
        UnityEngine.Object.DontDestroyOnLoad(object_);
    }
    //----------------------------


    //各ファイル情報
    JsonScoreData scoreData_ = new();

    //0308：結果が外部ファイルに反映されないバグの急造対策用
    int killCount_ = 0;
    int HPCount_ = 0;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void SocreMGInit()
    {
        if (object_ == null) {
            CreateObject();
        }

        //Jsonファイル読込
        scoreData_ = JsonDataMG<JsonScoreData>.Load();
    }

    /// <summary>
    /// コンストラクター
    /// </summary>
    ScoreMG()
    {
        SocreMGInit();
    }

    /// <summary>
    /// 最新(直近)の結果取得：Score
    /// </summary>
    /// <returns>int：直近のスコア</returns>
    public static int GetLatestScoreData()
    {
        //return Instance.scoreDatas_.LatestScore;
        return Instance.killCount_;
    }

    /// <summary>
    /// 最新(直近)の結果取得 : HP
    /// </summary>
    /// <param name="type"></param>
    /// <returns> int : HP残量</returns>
    public static int GetHoldHP()
    {
        //return Instance.scoreDatas_.HoldHP;
        return Instance.HPCount_;
    }


    /// <summary>
    /// スコアを指定したJsonファイルに保存する
    /// </summary>
    /// <param name="saveScore"></param> 書き込むスコアの値
    /// <param name="type"></param>　書込み先のファイル指定
    public static void SaveScoreData(int saveScore, int holdHP)
    {
        Instance.scoreData_.LatestScore = saveScore;
        Instance.scoreData_.HoldHP = holdHP;
        JsonDataMG<JsonScoreData>.Save(Instance.scoreData_);

        Instance.killCount_ = saveScore;
        Instance.HPCount_ = holdHP;
    }

}