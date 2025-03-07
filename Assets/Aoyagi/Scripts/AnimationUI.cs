using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationUI : MonoBehaviour
{
    [Header("�X�^�[�gUI�̃A�j���[�V����"), SerializeField]
    protected Animator animator;

    //���̊֐������s����ƃA�j���[�V�������Đ�
    public virtual void PlayUIAnimation()
    {
        animator.SetTrigger("ui_anim");
    }
}
