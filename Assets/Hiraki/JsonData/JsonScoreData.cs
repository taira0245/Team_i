[System.Serializable]
public class JsonScoreData
{
    /// <summary>
    /// 最新のスコアデータ
    /// </summary>
    public int LatestScore { get; private set; }

    /// <summary>
    /// 直近の結果で終了時点で残っているHP
    /// </summary>
    public int HoldHP { get; private set; }


    public JsonScoreData()
    {
        LatestScore = 0;
        HoldHP = 3;
    }


}
