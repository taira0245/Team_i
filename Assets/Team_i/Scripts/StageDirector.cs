using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageDirector : MonoBehaviour
{
    [SerializeField] Timer timer_;
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
        timer_.PlayAnim();
    }

    private void Awake()
    {
        StartInit();
    }

    private void Update()
    {
        if (isGame) {
            isGame = GameStageExe();
        }
    }

    [SerializeField] string next_scene_name = default!;

    [Header("�Q�[�����Ԃ̐ݒ�")]
    [SerializeField] float game_time = 30;
    float elapsed_time = 0; //�o�ߎ���

    /// <summary>
    /// �Q�[���̗��ꏈ��
    /// </summary>
    /// <returns> �Q�[���v���C�\��Ԃ�Ԃ�</returns>
    bool GameStageExe()
    {
        //�Q�[���I�[�o�[����
        if (hp <= 0) {
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
        SceneManager.LoadScene(next_scene_name);
    }
}
