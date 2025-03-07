using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class StageDirector : MonoBehaviour
{
    [Header("ステージ情報")]
    [SerializeField] ScoreMG.E_ScoreType saveType = ScoreMG.E_ScoreType.Stage1;

    [Header("UIの設定")]
    [Tooltip("Timerインスタンス参照")]
    [SerializeField] Timer timer_;

    [SerializeField] KillCounter killCounter_;
    [SerializeField] HitPoint hitPoint_;

    [SerializeField] PlayerDirector plDirector_;

    [Header("遷移先シーンの設定")]
    [SerializeField] string nextSceneName = default!;
    [SerializeField] ScreenFade fade_;
    [SerializeField,Range(0,4.0f)] float fadeTime_;


    [Header("ゲーム時間の設定")]
    [SerializeField] float game_time = 30;
    float elapsed_time = 0; //経過時間


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
        GUI.Label(new Rect(x, y, 150, 50), "シーン名 : " + crSceneName, debugFontStyle_);
        GUI.Label(new Rect(x, y += y, 150, 50), "経過時間 : " + elapsed_time, debugFontStyle_);
    }
#endif


    private void Start()
    {
        AudioMG.PlayBGM("TestBGM");

        //カーソルの非表示
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

        //タイマーUIのセット
        timer_.PlayAnim();

        while (isGame) {
            yield return null;


            //メイン処理
            isGame = GameStageExe();
        }

        //シーン遷移
        fade_.SetSceneChange(nextSceneName, fadeTime_);
        SceneManager.LoadScene(nextSceneName);
    }
    void ChangeHP()
    {
        Debug.Log("ダメージ処理(UI)!");
        hitPoint_.Damage();
    }

    void ChangeCount()
    {
        killCounter_.KillCountPlus();
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

        //記録を更新
        ScoreMG.SaveScoreData(plDirector_.CurrentCount, plDirector_.CurrentHP, saveType);

        Cursor.visible = true;
    }

    /// <summary>
    /// ゲーム動作の停止制御
    /// </summary>
    void GameActSwitch(bool enableFlag)
    {
        plDirector_.StopMotion(enableFlag);
    }
}
