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
    /// <summary>
    /// インスタンス取得
    /// </summary>
    public static ScoreMG Instance
    {
        //Instanceが使用された際、instance_==nullの場合はインスタンスを生成
        get { instance_ ??= new ScoreMG(); return instance_; }
    }
    static GameObject object_ = null;
    /// <summary>
    /// オブジェクトの作成
    /// </summary>
    void CreateObject()
    {
        object_ = new GameObject("SocreMG");
        UnityEngine.Object.DontDestroyOnLoad(object_);
    }
    //----------------------------


    //各ファイル情報
    /// <summary>
    /// Scoreファイルタイプのリスト
    /// </summary>
    public enum E_ScoreType
    {
        Stage1,

        Max
    }

    static readonly string[] SCORE_TYPE1_FILENAME = { "Score_01", };
    JsonScoreData[] scoreDatas_ = new JsonScoreData[(int)E_ScoreType.Max];

    /// <summary>
    /// 初期化処理
    /// </summary>
    void SocreMGInit()
    {
        if (object_ == null) {
            CreateObject();


        }

        //Jsonファイル読込
        for (int i = 0; i < (int)E_ScoreType.Max; i++) {
            scoreDatas_[i] = JsonDataMG<JsonScoreData>.Load(SCORE_TYPE1_FILENAME[i]);
        }
    }

    /// <summary>
    /// コンストラクター
    /// </summary>
    ScoreMG()
    {
        SocreMGInit();
    }

    /// <summary>
    /// 高い順に保持している全てのスコアデータを取得
    /// </summary>
    /// <returns>int[] : 上位5つ分のスコアデータ配列</returns>
    public static int[] GetScoreDatas(E_ScoreType type = E_ScoreType.Stage1)
    {
        return Instance.scoreDatas_[(int)type].ScoreDatas;
    }

    /// <summary>
    /// 最新(直近)の結果取得：Score
    /// </summary>
    /// <returns>int：直近のスコア</returns>
    public static int GetLatestScoreData(E_ScoreType type = E_ScoreType.Stage1) { return Instance.scoreDatas_[(int)type].LatestScore; }

    /// <summary>
    /// 最新(直近)の結果取得 : HP
    /// </summary>
    /// <param name="type"></param>
    /// <returns> int : HP残量</returns>
    public static int GetHoldHP(E_ScoreType type = E_ScoreType.Stage1) { return Instance.scoreDatas_[(int)type].HoldHP; }


    /// <summary>
    /// スコアを指定したJsonファイルに保存する
    /// </summary>
    /// <param name="saveScore"></param> 書き込むスコアの値
    /// <param name="type"></param>　書込み先のファイル指定
    public static void SaveScoreData(int saveScore, int holdHP, E_ScoreType type = E_ScoreType.Stage1)
    {
        Instance.scoreDatas_[(int)type].ScoreDataUpdate(saveScore);
        JsonDataMG<JsonScoreData>.Save(SCORE_TYPE1_FILENAME[(int)type], Instance.scoreDatas_[(int)type]);
    }

    public int GetHoldScoreNum() => JsonScoreData.HOLD_SCORE_NUM;

}