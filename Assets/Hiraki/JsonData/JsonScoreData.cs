[System.Serializable]
public class JsonScoreData
{
    /// <summary>
    /// �ŐV�̃X�R�A�f�[�^
    /// </summary>
    public int LatestScore;

    /// <summary>
    /// ���߂̌��ʂŏI�����_�Ŏc���Ă���HP
    /// </summary>
    public int HoldHP;


    public JsonScoreData()
    {
        LatestScore = 0;
        HoldHP = 3;
    }


}
