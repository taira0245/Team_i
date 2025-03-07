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
    JsonScoreData scoreDatas_ = new();

    /// <summary>
    /// ����������
    /// </summary>
    void SocreMGInit()
    {
        if (object_ == null) {
            CreateObject();
        }

        //Json�t�@�C���Ǎ�
        scoreDatas_ = JsonDataMG<JsonScoreData>.Load();
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
    public static int GetLatestScoreData() { return Instance.scoreDatas_.LatestScore; }

    /// <summary>
    /// �ŐV(����)�̌��ʎ擾 : HP
    /// </summary>
    /// <param name="type"></param>
    /// <returns> int : HP�c��</returns>
    public static int GetHoldHP() { return Instance.scoreDatas_.HoldHP; }


    /// <summary>
    /// �X�R�A���w�肵��Json�t�@�C���ɕۑ�����
    /// </summary>
    /// <param name="saveScore"></param> �������ރX�R�A�̒l
    /// <param name="type"></param>�@�����ݐ�̃t�@�C���w��
    public static void SaveScoreData(int saveScore, int holdHP)
    {
        JsonDataMG<JsonScoreData>.Save(Instance.scoreDatas_);
    }

}