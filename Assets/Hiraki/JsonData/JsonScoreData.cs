namespace JsonFileData
{
    [System.Serializable]
    public class JsonScoreData
    {
        /// <summary>
        /// �ێ�����X�R�A���̐�
        /// </summary>
        public const int HOLD_SCORE_NUM = 5;

        /// <summary>
        /// �X�R�A�f�[�^(�X�R�A���������ɕێ�)
        /// </summary>
        int[] scoreDatas = new int[HOLD_SCORE_NUM];
        public int[] ScoreDatas {  get {  return scoreDatas; } }

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

            scoreDatas = new int[HOLD_SCORE_NUM];
            for (int i = 0; i < HOLD_SCORE_NUM; i++) {
                scoreDatas[i] = 0;
            }
        }


        /// <summary>
        /// �X�R�A�f�[�^�̍X�V (�t�@�C���ւ̕ۑ��͍s��Ȃ�)
        /// </summary>
        /// <param name="saveScoreValue"></param>�@���߃X�R�A�Ƃ��ăt�@�C���ɕۑ�����l
        /// <returns> �z��̕ύX�̗L��</returns>
        public bool ScoreDataUpdate(int saveScoreValue)
        {
            LatestScore = saveScoreValue;
            int compareIdx = HOLD_SCORE_NUM - 1;    //��r����z���Index

            //5�Ԗڂ̔z��f�[�^�ȉ��ł���Ώ����I��
            if (scoreDatas[compareIdx] >= saveScoreValue) { return false; } 

            //�ŉ��ʂ̋L�^������
            scoreDatas[compareIdx] = saveScoreValue;

            //������2�Ԗڂ̔z�񂩂�`�F�b�N���J�n
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