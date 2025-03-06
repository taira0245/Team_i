namespace JsonFileData
{
    [System.Serializable]
    public class JsonScoreData
    {
        /// <summary>
        /// 保持するスコア情報の数
        /// </summary>
        public const int HOLD_SCORE_NUM = 5;

        /// <summary>
        /// スコアデータ(スコアが高い順に保持)
        /// </summary>
        int[] scoreDatas = new int[HOLD_SCORE_NUM];
        public int[] ScoreDatas {  get {  return scoreDatas; } }

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

            scoreDatas = new int[HOLD_SCORE_NUM];
            for (int i = 0; i < HOLD_SCORE_NUM; i++) {
                scoreDatas[i] = 0;
            }
        }


        /// <summary>
        /// スコアデータの更新 (ファイルへの保存は行わない)
        /// </summary>
        /// <param name="saveScoreValue"></param>　直近スコアとしてファイルに保存する値
        /// <returns> 配列の変更の有無</returns>
        public bool ScoreDataUpdate(int saveScoreValue)
        {
            LatestScore = saveScoreValue;
            int compareIdx = HOLD_SCORE_NUM - 1;    //比較する配列のIndex

            //5番目の配列データ以下であれば処理終了
            if (scoreDatas[compareIdx] >= saveScoreValue) { return false; } 

            //最下位の記録を書換
            scoreDatas[compareIdx] = saveScoreValue;

            //下から2番目の配列からチェックを開始
            for(int i = compareIdx - 1; i >= 0; i--) {
                if(saveScoreValue > scoreDatas[i]) {
                    scoreDatas[i + 1] = scoreDatas[i];
                    if(i == 0) { scoreDatas[i] = saveScoreValue; }
                }
                else {
                    scoreDatas[i + 1] = saveScoreValue;
                    break;
                }
            }

            return true;
        }
    }
}