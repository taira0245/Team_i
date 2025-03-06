using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Timer : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool timer_stop;

    private void Start()
    {
        animator.SetFloat("MoveTime", 0);
        timer_stop = true;
    }

    public void Update()
    {
        //デバッグ用
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!timer_stop)
            {
                StopAnim();
                timer_stop = true;
            }
            else
            {
                PlayAnim();
                timer_stop = false;
            }
        }
    }

    //タイマーを一時停止
    public void StopAnim()
    {
        animator.SetFloat("MoveTime", 0);
    }

    //タイマーを再生
    public void PlayAnim()
    {
        animator.SetFloat("MoveTime", 1);
    }

    //ここからゲーム終了の処理を実行
    public void GameEnd()
    {
        Debug.Log("ゲームオーバー");
    }
}
