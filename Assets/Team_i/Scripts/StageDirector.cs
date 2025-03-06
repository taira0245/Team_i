using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class StageDirector : MonoBehaviour
{
    [Header("タイマーUIの設定")]
    [Tooltip("Timerインスタンス参照")]
    [SerializeField] Timer timer_;

    [SerializeField] KillCounter killCounter_;
    [SerializeField] HitPoint hitPoint_;

    [SerializeField] Player player_;
    int oldHitCnt = 0;  //１フレーム前の、敵に当てた回数
    int oldHp = 0; //1フレーム前の、HP


    int hp = 0;
    bool isGame = false;
    bool gameover_Flag = false;

    /// <summary>
    /// ゲーム開始時の初期化処理
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

    [Header("ゲーム時間の設定")]
    [SerializeField] float game_time = 30;
    float elapsed_time = 0; //経過時間

    /// <summary>
    /// PLStateの一時保存
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
    /// ゲームの流れ処理
    /// </summary>
    /// <returns> ゲームプレイ可能状態を返す</returns>
    bool GameStageExe()
    {

        if (player_.hp != oldHp) { ChangeHP(); }
        if (player_.count != oldHitCnt) { ChangeCount(); }
        SavePLParam();

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
        SceneManager.LoadScene(nextSceneName);
    }
}
