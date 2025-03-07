using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartAnimationUI : AnimationUI
{
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //デバッグ用
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayUIAnimation();
        }
    }

    //この関数が実行するとアニメーションを再生
    public override void PlayUIAnimation()
    {
        base.PlayUIAnimation();
    }

    //ゲームスタートのアニメーションが終了した時、この関数が実行される。
    //ここに次の処理を入れるとアニメーションが終わった時に実行されます。
    //例：(ゲーム全体の再生)
    public void UIAnimationEnd()
    {
        Debug.Log("アニメーション終了");
    }
}
