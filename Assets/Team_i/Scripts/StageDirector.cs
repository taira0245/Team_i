using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class StageDirector : MonoBehaviour
{
    [Header("�^�C�}�[UI�̐ݒ�")]
    [Tooltip("Timer�C���X�^���X�Q��")]
    [SerializeField] Timer timer_;

    [SerializeField] KillCounter killCounter_;
    [SerializeField] HitPoint hitPoint_;

    [SerializeField] Player player_;
    int oldHitCnt = 0;  //�P�t���[���O�́A�G�ɓ��Ă���
    int oldHp = 0; //1�t���[���O�́AHP


    int hp = 0;
    bool isGame = false;
    bool gameover_Flag = false;

    /// <summary>
    /// �Q�[���J�n���̏���������
    /// </summary>
    void StartInit()
    {
        isGame = true;
        gameover_Flag = false;
        hp = 3;
        elapsed_time = 0;
        oldHitCnt = 0;
        timer_.PlayAnim();

        oldHp = player_.hp;
        oldHitCnt = player_.count;
    }


    private void Start()
    {
        StartInit();
    }

    private void Update()
    {
        if (isGame) {
            isGame = GameStageExe();
        }
    }

    [SerializeField] string nextSceneName = default!;

    [SerializeField] PlayerDirector playerDirector_;
    PlayerDirector.E_PLState crPLState;

    [Header("�Q�[�����Ԃ̐ݒ�")]
    [SerializeField] float game_time = 30;
    float elapsed_time = 0; //�o�ߎ���

    /// <summary>
    /// PLState�̈ꎞ�ۑ�
    /// </summary>
    void SavePLParam()
    {
        oldHitCnt = player_.hp;
        oldHitCnt = player_.count;
    }

    void ChangeHP()
    {
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
        SavePLParam();

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
        SceneManager.LoadScene(nextSceneName);
    }
}
