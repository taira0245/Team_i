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
    public static ScoreMG Instance
    {
        //Instance���g�p���ꂽ�ہAinstance_==null�̏ꍇ�̓C���X�^���X�𐶐�
        get { instance_ ??= new ScoreMG(); return instance_; }
    }

    static GameObject object_ = null;
    void CreateObject()
    {
        object_ = new GameObject("SocreMG");
        UnityEngine.Object.DontDestroyOnLoad(object_);
    }
    //----------------------------


    //�e�t�@�C�����
    JsonScoreData scoreData_ = new();

    //0308�F���ʂ��O���t�@�C���ɔ��f����Ȃ��o�O�̋}���΍��p
    int killCount_ = 0;
    int HPCount_ = 0;

    /// <summary>
    /// ����������
    /// </summary>
    void SocreMGInit()
    {
        if (object_ == null) {
            CreateObject();
        }

        //Json�t�@�C���Ǎ�
        scoreData_ = JsonDataMG<JsonScoreData>.Load();
    }

    /// <summary>
    /// �R���X�g���N�^�[
    /// </summary>
    ScoreMG()
    {
        SocreMGInit();
    }

    /// <summary>
    /// �ŐV(����)�̌��ʎ擾�FScore
    /// </summary>
    /// <returns>int�F���߂̃X�R�A</returns>
    public static int GetLatestScoreData()
    {
        //return Instance.scoreDatas_.LatestScore;
        return Instance.killCount_;
    }

    /// <summary>
    /// �ŐV(����)�̌��ʎ擾 : HP
    /// </summary>
    /// <param name="type"></param>
    /// <returns> int : HP�c��</returns>
    public static int GetHoldHP()
    {
        //return Instance.scoreDatas_.HoldHP;
        return Instance.HPCount_;
    }


    /// <summary>
    /// �X�R�A���w�肵��Json�t�@�C���ɕۑ�����
    /// </summary>
    /// <param name="saveScore"></param> �������ރX�R�A�̒l
    /// <param name="type"></param>�@�����ݐ�̃t�@�C���w��
    public static void SaveScoreData(int saveScore, int holdHP)
    {
        Instance.scoreData_.LatestScore = saveScore;
        Instance.scoreData_.HoldHP = holdHP;
        JsonDataMG<JsonScoreData>.Save(Instance.scoreData_);

        Instance.killCount_ = saveScore;
        Instance.HPCount_ = holdHP;
    }

}