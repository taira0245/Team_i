using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAndEndAnimation : MonoBehaviour
{
    [Header("スタートUIのアニメーション"), SerializeField]
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //この関数が実行するとアニメーションを再生
    public void PlayUIAnimation()
    {
        animator.SetTrigger("anim_start");
    }

    //ゲームスタートのアニメーションが終了した時、この関数が実行される。
    //ここに次の処理を入れるとアニメーションが終わった時に実行されます。
    //例：(ゲーム全体の一時停止・再生)
    public void UIAnimationEnd()
    {
        Debug.Log("アニメーション終了");
        this.gameObject.SetActive(false);
    }
}
