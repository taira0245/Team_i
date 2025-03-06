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
    /// ゲーム開始時の初期化処理
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

    [Header("ゲーム時間の設定")]
    [SerializeField] float game_time = 30;
    float elapsed_time = 0; //経過時間

    /// <summary>
    /// ゲームの流れ処理
    /// </summary>
    /// <returns> ゲームプレイ可能状態を返す</returns>
    bool GameStageExe()
    {
        //ゲームオーバー処理
        if (hp <= 0) {
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
        SceneManager.LoadScene(next_scene_name);
    }
}
