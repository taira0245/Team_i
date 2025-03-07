using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�R�A�f�[�^�Ǘ��p�N���X (�V���O���g��)
/// </summary>
public class ScoreMG
{
    //========================
    // �C���X�^���X�֌W
    //========================
    static ScoreMG instance_ = null;
    /// <summary>
    /// �C���X�^���X�擾
    /// </summary>
    public static ScoreMG Instance
    {
        //Instance���g�p���ꂽ�ہAinstance_==null�̏ꍇ�̓C���X�^���X�𐶐�
        get { instance_ ??= new ScoreMG(); return instance_; }
    }
    static GameObject object_ = null;
    /// <summary>
    /// �I�u�W�F�N�g�̍쐬
    /// </summary>
    void CreateObject()
    {
        object_ = new GameObject("SocreMG");
        UnityEngine.Object.DontDestroyOnLoad(object_);
    }
    //----------------------------


    //�e�t�@�C�����
    /// <summary>
    /// Score�t�@�C���^�C�v�̃��X�g
    /// </summary>
    public enum E_ScoreType
    {
        Stage1,

        Max
    }

    static readonly string[] SCORE_TYPE1_FILENAME = { "Score_01", };
    JsonScoreData[] scoreDatas_ = new JsonScoreData[(int)E_ScoreType.Max];

    /// <summary>
    /// ����������
    /// </summary>
    void SocreMGInit()
    {
        if (object_ == null) {
            CreateObject();


        }

        //Json�t�@�C���Ǎ�
        for (int i = 0; i < (int)E_ScoreType.Max; i++) {
            scoreDatas_[i] = JsonDataMG<JsonScoreData>.Load(SCORE_TYPE1_FILENAME[i]);
        }
    }

    /// <summary>
    /// �R���X�g���N�^�[
    /// </summary>
    ScoreMG()
    {
        SocreMGInit();
    }

    /// <summary>
    /// �������ɕێ����Ă���S�ẴX�R�A�f�[�^���擾
    /// </summary>
    /// <returns>int[] : ���5���̃X�R�A�f�[�^�z��</returns>
    public static int[] GetScoreDatas(E_ScoreType type = E_ScoreType.Stage1)
    {
        return Instance.scoreDatas_[(int)type].ScoreDatas;
    }

    /// <summary>
    /// �ŐV(����)�̌��ʎ擾�FScore
    /// </summary>
    /// <returns>int�F���߂̃X�R�A</returns>
    public static int GetLatestScoreData(E_ScoreType type = E_ScoreType.Stage1) { return Instance.scoreDatas_[(int)type].LatestScore; }

    /// <summary>
    /// �ŐV(����)�̌��ʎ擾 : HP
    /// </summary>
    /// <param name="type"></param>
    /// <returns> int : HP�c��</returns>
    public static int GetHoldHP(E_ScoreType type = E_ScoreType.Stage1) { return Instance.scoreDatas_[(int)type].HoldHP; }


    /// <summary>
    /// �X�R�A���w�肵��Json�t�@�C���ɕۑ�����
    /// </summary>
    /// <param name="saveScore"></param> �������ރX�R�A�̒l
    /// <param name="type"></param>�@�����ݐ�̃t�@�C���w��
    public static void SaveScoreData(int saveScore, int holdHP, E_ScoreType type = E_ScoreType.Stage1)
    {
        Instance.scoreDatas_[(int)type].ScoreDataUpdate(saveScore);
        JsonDataMG<JsonScoreData>.Save(SCORE_TYPE1_FILENAME[(int)type], Instance.scoreDatas_[(int)type]);
    }

    public int GetHoldScoreNum() => JsonScoreData.HOLD_SCORE_NUM;

}