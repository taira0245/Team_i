using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class StageDirector : MonoBehaviour
{
    [Header("�X�e�[�W���")]
    [SerializeField] protected PlayerDirector plDirector_;
    [SerializeField] protected EnemyDirector enemyDirector_;


    [Header("UI�̐ݒ�")]
    [Tooltip("Timer�C���X�^���X�Q��")]
    [SerializeField] Timer timer_;

    [SerializeField] protected KillCounter killCounter_;
    [SerializeField] protected HitPoint hitPoint_;
    [SerializeField] OptionManager option_;


    [Header("�A�j���[�^�[�̐ݒ�")]
    [SerializeField] GameStartAnimationUI startAnim_;
    [SerializeField] GameEndAnimationUI endAnim_;

    [Header("�J�ڐ�V�[���̐ݒ�")]
    [SerializeField] string nextSceneName = default!;
    [SerializeField] ScreenFade fade_;
    [SerializeField, Range(0, 3.0f)] float fadeTime_;


    [Header("�Q�[�����Ԃ̐ݒ�")]
    [Tooltip("�t�F�[�h�I���ォ��v���C�J�n�܂ł̒x�؎���")]
    [SerializeField] float startDelay = 2.5f;
    [Tooltip("�Q�[���I���ォ��t�F�[�h�J�n�܂ł̒x�؎���")]
    [SerializeField] float endDelay = 2.5f;
    //[SerializeField] float CountInterval = 0.8f;
    [Tooltip("�Q�[�����v���C���鎞��")]
    [SerializeField] float game_time = 30;
    float elapsed_time = 0; //�o�ߎ���


    bool isGame = false;
    protected bool gameover_Flag = false;
    bool isPause = false;


    private void Awake()
    {
        AudioMG.PlayBGM("PlayBGM");
//#if UNITY_EDITOR
//        Debug.Log("<color=green>timer_ : " + timer_ + "</color>");
//        Debug.Log("<color=green>killCounter_ : " + killCounter_ + "</color>");
//        Debug.Log("<color=green>hitPoint_ : " + hitPoint_ + "</color>");
//        crSceneName = SceneManager.GetActiveScene().name;
//        debugFontStyle_.fontSize = 15;
//        debugFontStyle_.normal.textColor = Color.red;
//#endif
    }


//#if UNITY_EDITOR
//    GUIStyle debugFontStyle_ = new();
//    string crSceneName;
//    private void OnGUI()
//    {
//        float x = 20.0f;
//        float y = 10.0f;
//        float line_y = 20.0f;
//        GUI.Label(new Rect(x, y, 150, 50), "�V�[���� : " + crSceneName, debugFontStyle_);
//        GUI.Label(new Rect(x, y += line_y, 150, 50), "�o�ߎ��� : " + elapsed_time, debugFontStyle_);
//        GUI.Label(new Rect(x, y += line_y, 150, 50), "IsGame : " + isGame, debugFontStyle_);
//        GUI.Label(new Rect(x, y += line_y, 150, 50), "IsPause : " + isPause, debugFontStyle_);
//        GUI.Label(new Rect(x, y += line_y, 150, 50), "IsFadeing : " + ScreenFade.isFading_, debugFontStyle_);
//    }
//#endif


    private void Start()
    {
        //�J�[�\���̔�\��
        Cursor.visible = false;

        plDirector_.SceneInit();
        GameActSwitch(false);
        Time.timeScale = 1;
        StartCoroutine(StageFlow());
    }

    WaitForSecondsRealtime delayTime;
    IEnumerator StageFlow()
    {
        delayTime = new(startDelay);
        gameover_Flag = false;
        elapsed_time = 0;

        //�V�[���t�F�[�h�I���҂�
        while (ScreenFade.isFading_) {
            yield return null;
        }

        //�V�[�������J�n
        startAnim_.PlayUIAnimation();

        yield return delayTime;


        //�Q�[���J�n
        isGame = true;
        GameActSwitch(true);
        while (isGame) {
            yield return null;

            //���C������
            isGame = GameStageExe();
        }

        //�Q�[���I��
        endAnim_.PlayUIAnimation();
        delayTime = new(endDelay);
        yield return delayTime;

        //�V�[���J��
        fade_.SetSceneChange(nextSceneName, fadeTime_);
    }

    /// <summary>
    /// �Q�[���̃��C������
    /// </summary>
    /// <returns> �Q�[���v���C�\��Ԃ�Ԃ�</returns>
    protected virtual bool GameStageExe()
    {
        if (plDirector_.CheckChangeCount()) { killCounter_.KillCountPlus(); }
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
    protected void GameTerminate()
    {
        if (timer_ != null) timer_.GameEnd();
        GameActSwitch(false);
        Time.timeScale = 1;

        //�L�^���X�V
        ScoreMG.SaveScoreData(plDirector_.CurrentCount, plDirector_.CurrentHP);

        Cursor.visible = true;
    }

    List<Bom> bomList = new();
    List<GameObject> EnemyList = new();
    /// <summary>
    /// �Q�[������̒�~����
    /// </summary>
    public void GameActSwitch(bool enableFlag)
    {
        isPause = !enableFlag;
        if (!enableFlag) {
            Time.timeScale = 0;

            //�^�C�}�[��~
            if(timer_ != null) timer_.StopAnim();
            Cursor.visible = true;

        }
        else {
            //�^�C�}�[�ĉ�
            if (timer_ != null) timer_.PlayAnim();

            Time.timeScale = 1;
            Cursor.visible = false;
        }
        plDirector_.MotionAct(enableFlag);
        enemyDirector_.MotionAct(enableFlag);
    }
}
