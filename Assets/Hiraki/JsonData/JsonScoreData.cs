[System.Serializable]
public class JsonScoreData
{
    /// <summary>
    /// �ŐV�̃X�R�A�f�[�^
    /// </summary>
    public int LatestScore { get; private set; }

    /// <summary>
    /// ���߂̌��ʂŏI�����_�Ŏc���Ă���HP
    /// </summary>
    public int HoldHP { get; private set; }


    public JsonScoreData()
    {
        LatestScore = 0;
        HoldHP = 3;
    }


}
