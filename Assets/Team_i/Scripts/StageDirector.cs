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

    [SerializeField] PlayerDirector plDirector_;

    [Header("�J�ڐ�V�[���̐ݒ�")]
    [SerializeField] string nextSceneName = default!;
    [SerializeField] ScreenFade fade_;
    [SerializeField,Range(0,4.0f)] float fadeTime_;


    [Header("�Q�[�����Ԃ̐ݒ�")]
    [SerializeField] float game_time = 30;
    float elapsed_time = 0; //�o�ߎ���


    bool isGame = false;
    bool gameover_Flag = false;


    //  Debug
#if UNITY_EDITOR
    private void Awake()
    {
        Debug.Log("<color=green>timer_ : " + timer_ + "</color>");
        Debug.Log("<color=green>killCounter_ : " + killCounter_ + "</color>");
        Debug.Log("<color=green>hitPoint_ : " + hitPoint_ + "</color>");
        crSceneName = SceneManager.GetActiveScene().name;
        debugFontStyle_.fontSize = 15;
        debugFontStyle_.normal.textColor = Color.red;
    }

    GUIStyle debugFontStyle_ = new();
    string crSceneName;
    private void OnGUI()
    {
        float x = 20.0f;
        float y = 20.0f;
        GUI.Label(new Rect(x, y, 150, 50), "�V�[���� : " + crSceneName, debugFontStyle_);
        GUI.Label(new Rect(x, y += y, 150, 50), "�o�ߎ��� : " + elapsed_time, debugFontStyle_);
    }
#endif


    private void Start()
    {
        AudioMG.PlayBGM("TestBGM");

        //�J�[�\���̔�\��
        Cursor.visible = false;
        plDirector_.SceneInit();
        StartCoroutine(StageFlow());
    }

    IEnumerator StageFlow()
    {
        isGame = true;
        gameover_Flag = false;
        elapsed_time = 0;
        isGame = true;

        //�^�C�}�[UI�̃Z�b�g
        timer_.PlayAnim();

        while (isGame) {
            yield return null;


            //���C������
            isGame = GameStageExe();
        }

        //�V�[���J��
        fade_.SetSceneChange(nextSceneName, fadeTime_);
        SceneManager.LoadScene(nextSceneName);
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
    /// �Q�[���̃��C������
    /// </summary>
    /// <returns> �Q�[���v���C�\��Ԃ�Ԃ�</returns>
    bool GameStageExe()
    {
        if(plDirector_.CheckChangeCount()) { killCounter_.KillCountPlus(); }
        if (plDirector_.CheckChangeHP()) {
            hitPoint_.Damage();
            if (plDirector_.CurrentHP <= 0) {
                gameover_Flag = true;
                GameTerminate();
                return false;
            }
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
    /// �Q�[���X�e�[�W�I�����̋��ʏ���
    /// </summary>
    void GameTerminate()
    {
        timer_.GameEnd();

        //�L�^���X�V
        ScoreMG.SaveScoreData(plDirector_.CurrentCount, plDirector_.CurrentHP, saveType);

        Cursor.visible = true;
    }

    /// <summary>
    /// �Q�[������̒�~����
    /// </summary>
    void GameActSwitch(bool enableFlag)
    {
        plDirector_.StopMotion(enableFlag);
    }
}
