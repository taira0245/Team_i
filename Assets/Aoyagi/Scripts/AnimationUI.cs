using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationUI : MonoBehaviour
{
    [Header("スタートUIのアニメーション"), SerializeField]
    protected Animator animator;

    //この関数が実行するとアニメーションを再生
    public virtual void PlayUIAnimation()
    {
        animator.SetTrigger("ui_anim");
    }
}
