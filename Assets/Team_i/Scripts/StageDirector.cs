using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class StageDirector : MonoBehaviour
{
    [Header("�X�e�[�W���")]
    [SerializeField] ScoreMG.E_ScoreType saveType = ScoreMG.E_ScoreType.Stage1;

    [Header("UI�̐ݒ�")]
    [Tooltip("Timer�C���X�^���X�Q��")]
    [SerializeField] Timer timer_;

    [SerializeField] KillCounter killCounter_;
    [SerializeField] HitPoint hitPoint_;

    [SerializeField] Player player_;
    int oldHitCnt = 0;  //�P�t���[���O�́A�G�ɓ��Ă���
    int oldHp = 0; //1�t���[���O�́AHP


    bool isGame = false;
    bool gameover_Flag = false;


    /// <summary>
    /// �Q�[���J�n���̏���������
    /// </summary>
    void StartInit()
    {
        AudioMG.PlayBGM("TestBGM");

        isGame = true;
        gameover_Flag = false;
        elapsed_time = 0;
        oldHitCnt = 0;
        timer_.PlayAnim();

        oldHp = player_.hp;
        oldHitCnt = player_.count;

        //================
        // Debug
#if UNITY_EDITOR
        Debug.Log("<color=green>timer_ : " + timer_ + "</color>");
        Debug.Log("<color=green>killCounter_ : " + killCounter_ + "</color>");
        Debug.Log("<color=green>hitPoint_ : " + hitPoint_ + "</color>");
        Debug.Log("<color=green>player_ : " + player_ + "</color>");

#endif
        //=================

    }

    
    private void Start()
    {
        //�J�[�\���̔�\��
        Cursor.visible = false;

        StartInit();
    }

    private void Update()
    {
        if (isGame) {
            isGame = GameStageExe();
        }
    }

    [Header("�J�ڐ�V�[���̐ݒ�")]
    [SerializeField] string nextSceneName = default!;


    [Header("�Q�[�����Ԃ̐ݒ�")]
    [SerializeField] float game_time = 30;
    float elapsed_time = 0; //�o�ߎ���

    /// <summary>
    /// PLState�̈ꎞ�ۑ�
    /// </summary>
    void PlPramUpdate()
    {
        oldHp = player_.hp;
        oldHitCnt = player_.count;
    }

    void ChangeHP()
    {
        Debug.Log("�_���[�W����(UI)!");
        hitPoint_.Damage();
    }

    void ChangeCount()
    {
        killCounter_.KillCountPlus();
    }

    /// <summary>
    /// �Q�[���̗��ꏈ��
    /// </summary>
    /// <returns> �Q�[���v���C�\��Ԃ�Ԃ�</returns>
    bool GameStageExe()
    {

        if (player_.hp != oldHp) { ChangeHP(); }
        if (player_.count != oldHitCnt) { ChangeCount(); }
        PlPramUpdate();

        //�Q�[���I�[�o�[����
        if (player_.hp <= 0) {
            gameover_Flag = true;
            GameTerminate();
            return false;
        }


        //���Ԍo�ߏ���
        elapsed_time += Time.deltaTime;
        if (elapsed_time > game_time) {
            GameTerminate();
            return false;
        }

        return true;
    }

    /// <summary>
    /// �Q�[���X�e�[�W�I�����̏���
    /// </summary>
    void GameTerminate()
    {
        timer_.GameEnd();

        //�L�^���X�V
        ScoreMG.SaveScoreData(player_.count, player_.hp, saveType);

        Cursor.visible = true;
        SceneManager.LoadScene(nextSceneName);
    }
}
