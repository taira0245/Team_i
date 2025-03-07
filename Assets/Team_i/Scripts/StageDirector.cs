using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class StageDirector : MonoBehaviour
{
    [Header("ステージ情報")]
    [SerializeField] ScoreMG.E_ScoreType saveType = ScoreMG.E_ScoreType.Stage1;
    [SerializeField] PlayerDirector plDirector_;
    [SerializeField] EnemyDirector enemyDirector_;

    [Header("UIの設定")]
    [Tooltip("Timerインスタンス参照")]
    [SerializeField] Timer timer_;

    [SerializeField] KillCounter killCounter_;
    [SerializeField] HitPoint hitPoint_;


    [Header("遷移先シーンの設定")]
    [SerializeField] string nextSceneName = default!;
    [SerializeField] ScreenFade fade_;
    [SerializeField,Range(0,4.0f)] float fadeTime_;


    [Header("ゲーム時間の設定")]
    [Tooltip("プレイ開始の遅滞時間")]
    [SerializeField] float startDelay = 2.5f;
    [SerializeField] float CountInterval = 0.8f;
    [SerializeField] float game_time = 30;
    float elapsed_time = 0; //経過時間


    bool isGame = false;
    bool gameover_Flag = false;
    bool isPause = false;


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
        float y = 10.0f;
        float line_y = 20.0f;
        GUI.Label(new Rect(x, y, 150, 50), "シーン名 : " + crSceneName, debugFontStyle_);
        GUI.Label(new Rect(x, y += line_y, 150, 50), "経過時間 : " + elapsed_time, debugFontStyle_);
        GUI.Label(new Rect(x, y += line_y, 150, 50), "IsGame : " + isGame, debugFontStyle_);
        GUI.Label(new Rect(x, y += line_y, 150, 50), "IsPause : " + isPause, debugFontStyle_);
    }
#endif


    private void Start()
    {
        AudioMG.PlayBGM("TestBGM");

        //カーソルの非表示
        Cursor.visible = false;

        plDirector_.SceneInit();
        GameActSwitch(false);
        StartCoroutine(StageFlow());
    }

    WaitForSecondsRealtime delayTime;
    IEnumerator StageFlow()
    {
        //シーンフェード終了待ち
        while (ScreenFade.isFading_) {
            yield return null;
        }

        delayTime = new(startDelay);
        gameover_Flag = false;
        elapsed_time = 0;
        yield return delayTime;


        isGame = true;
        GameActSwitch(true);

        while (isGame) {
            yield return null;

            if (Input.GetKeyDown(KeyCode.P)) {
                isPause = !isPause;
                GameActSwitch(!isPause);
            }

            //メイン処理
            isGame = GameStageExe();
        }

        //シーン遷移
        fade_.SetSceneChange(nextSceneName, fadeTime_);
    }

    /// <summary>
    /// ゲームのメイン処理
    /// </summary>
    /// <returns> ゲームプレイ可能状態を返す</returns>
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

        //時間経過処理
        elapsed_time += Time.deltaTime;
        if (elapsed_time > game_time) {
            GameTerminate();
            return false;
        }

        return true;
    }

    /// <summary>
    /// ゲームステージ終了時の共通処理
    /// </summary>
    void GameTerminate()
    {
        timer_.GameEnd();
        GameActSwitch(false);

        //記録を更新
        ScoreMG.SaveScoreData(plDirector_.CurrentCount, plDirector_.CurrentHP, saveType);

        Cursor.visible = true;
    }

    List<Enemy_left> enemy_F = new();
    List<Enemy_right> enemy_R = new();
    List<Bom> boms = new();
    /// <summary>
    /// ゲーム動作の停止制御
    /// </summary>
    void GameActSwitch(bool enableFlag)
    {
        if (!enableFlag) {
            Time.timeScale = 0;
            //タイマー停止
            timer_.StopAnim();

        }
        else{
            enemy_F.Clear();
            enemy_R.Clear();
            boms.Clear();

            //タイマー再会
            timer_.PlayAnim();
            Time.timeScale = 1;
        }
        plDirector_.StopMotion(enableFlag);
    }
}
