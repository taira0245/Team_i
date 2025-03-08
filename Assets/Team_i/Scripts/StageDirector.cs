using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class StageDirector : MonoBehaviour
{
    [Header("ステージ情報")]
    [SerializeField] PlayerDirector plDirector_;
    [SerializeField] EnemyDirector enemyDirector_;


    [Header("UIの設定")]
    [Tooltip("Timerインスタンス参照")]
    [SerializeField] Timer timer_;

    [SerializeField] KillCounter killCounter_;
    [SerializeField] HitPoint hitPoint_;


    [Header("アニメーターの設定")]
    [SerializeField] GameStartAnimationUI startAnim_;
    [SerializeField] GameEndAnimationUI endAnim_;

    [Header("遷移先シーンの設定")]
    [SerializeField] string nextSceneName = default!;
    [SerializeField] ScreenFade fade_;
    [SerializeField, Range(0, 3.0f)] float fadeTime_;


    [Header("ゲーム時間の設定")]
    [Tooltip("フェード終了後からプレイ開始までの遅滞時間")]
    [SerializeField] float startDelay = 2.5f;
    [Tooltip("ゲーム終了後からフェード開始までの遅滞時間")]
    [SerializeField] float endDelay = 2.5f;
    //[SerializeField] float CountInterval = 0.8f;
    [Tooltip("ゲームをプレイする時間")]
    [SerializeField] float game_time = 30;
    float elapsed_time = 0; //経過時間


    bool isGame = false;
    bool gameover_Flag = false;
    bool isPause = false;


    private void Awake()
    {
        AudioMG.PlayBGM("PlayBGM");
#if UNITY_EDITOR
        Debug.Log("<color=green>timer_ : " + timer_ + "</color>");
        Debug.Log("<color=green>killCounter_ : " + killCounter_ + "</color>");
        Debug.Log("<color=green>hitPoint_ : " + hitPoint_ + "</color>");
        crSceneName = SceneManager.GetActiveScene().name;
        debugFontStyle_.fontSize = 15;
        debugFontStyle_.normal.textColor = Color.red;
#endif
    }


#if UNITY_EDITOR
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
        GUI.Label(new Rect(x, y += line_y, 150, 50), "IsFadeing : " + ScreenFade.isFading_, debugFontStyle_);
    }
#endif


    private void Start()
    {
        //カーソルの非表示
        Cursor.visible = false;

        plDirector_.SceneInit();
        GameActSwitch(false);
        StartCoroutine(StageFlow());
    }

    WaitForSecondsRealtime delayTime;
    IEnumerator StageFlow()
    {
        delayTime = new(startDelay);
        gameover_Flag = false;
        elapsed_time = 0;

        //シーンフェード終了待ち
        while (ScreenFade.isFading_) {
            yield return null;
        }

        //シーン処理開始
        startAnim_.PlayUIAnimation();

        yield return delayTime;
        //float elapsedTime = 0;
        //while (true) {
        //    yield return null;
        //    elapsedTime += Time.unscaledDeltaTime;
        //    if (elapsedTime >= startDelay) { break; }
        //}


        //ゲーム開始
        isGame = true;
        GameActSwitch(true);
        while (isGame) {
            yield return null;

            ////pause処理
            //if (Input.GetKeyDown(KeyCode.P)) {
            //    isPause = !isPause;
            //    GameActSwitch(!isPause);
            //}

            //メイン処理
            isGame = GameStageExe();
        }

        //ゲーム終了
        endAnim_.PlayUIAnimation();
        delayTime = new(endDelay);
        yield return delayTime;

        //シーン遷移
        fade_.SetSceneChange(nextSceneName, fadeTime_);
    }

    /// <summary>
    /// ゲームのメイン処理
    /// </summary>
    /// <returns> ゲームプレイ可能状態を返す</returns>
    bool GameStageExe()
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
        ScoreMG.SaveScoreData(plDirector_.CurrentCount, plDirector_.CurrentHP);

        Cursor.visible = true;
    }

    List<Bom> boms = new();
    /// <summary>
    /// ゲーム動作の停止制御
    /// </summary>
    void GameActSwitch(bool enableFlag)
    {
        if (!enableFlag) {
            //Time.timeScale = 0;
            //タイマー停止
            timer_.StopAnim();

        }
        else {
            //enemy_F.Clear();
            //enemy_R.Clear();
            boms.Clear();

            //タイマー再会
            timer_.PlayAnim();
            //Time.timeScale = 1;
        }
        plDirector_.StopMotion(enableFlag);
        enemyDirector_.StopMotion(enableFlag);
    }
}
