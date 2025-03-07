using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAndEndAnimation : MonoBehaviour
{
    [Header("�X�^�[�gUI�̃A�j���[�V����"), SerializeField]
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //���̊֐������s����ƃA�j���[�V�������Đ�
    public void PlayUIAnimation()
    {
        animator.SetTrigger("anim_start");
    }

    //�Q�[���X�^�[�g�̃A�j���[�V�������I���������A���̊֐������s�����B
    //�����Ɏ��̏���������ƃA�j���[�V�������I��������Ɏ��s����܂��B
    //��F(�Q�[���S�̂̈ꎞ��~�E�Đ�)
    public void UIAnimationEnd()
    {
        Debug.Log("�A�j���[�V�����I��");
        this.gameObject.SetActive(false);
    }
}
