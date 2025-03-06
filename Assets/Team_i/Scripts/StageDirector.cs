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

    [SerializeField] Player player_;
    int oldHitCnt = 0;  //１フレーム前の、敵に当てた回数
    int oldHp = 0; //1フレーム前の、HP


    bool isGame = false;
    bool gameover_Flag = false;


    /// <summary>
    /// ゲーム開始時の初期化処理
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
        //カーソルの非表示
        Cursor.visible = false;

        StartInit();
    }

    private void Update()
    {
        if (isGame) {
            isGame = GameStageExe();
        }
    }

    [Header("遷移先シーンの設定")]
    [SerializeField] string nextSceneName = default!;


    [Header("ゲーム時間の設定")]
    [SerializeField] float game_time = 30;
    float elapsed_time = 0; //経過時間

    /// <summary>
    /// PLStateの一時保存
    /// </summary>
    void PlPramUpdate()
    {
        oldHp = player_.hp;
        oldHitCnt = player_.count;
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
    /// ゲームの流れ処理
    /// </summary>
    /// <returns> ゲームプレイ可能状態を返す</returns>
    bool GameStageExe()
    {

        if (player_.hp != oldHp) { ChangeHP(); }
        if (player_.count != oldHitCnt) { ChangeCount(); }
        PlPramUpdate();

        //ゲームオーバー処理
        if (player_.hp <= 0) {
            gameover_Flag = true;
            GameTerminate();
            return false;
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
    /// ゲームステージ終了時の処理
    /// </summary>
    void GameTerminate()
    {
        timer_.GameEnd();

        //記録を更新
        ScoreMG.SaveScoreData(player_.count, player_.hp, saveType);

        Cursor.visible = true;
        SceneManager.LoadScene(nextSceneName);
    }
}
