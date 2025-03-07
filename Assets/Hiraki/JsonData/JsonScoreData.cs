[System.Serializable]
public class JsonScoreData
{
    /// <summary>
    /// 最新のスコアデータ
    /// </summary>
    public int LatestScore;

    /// <summary>
    /// 直近の結果で終了時点で残っているHP
    /// </summary>
    public int HoldHP;


    public JsonScoreData()
    {
        LatestScore = 0;
        HoldHP = 3;
    }


}
