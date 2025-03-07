using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndAnimationUI : AnimationUI
{
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //�f�o�b�O�p
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayUIAnimation();
        }
    }

    //���̊֐������s����ƃA�j���[�V�������Đ�
    public override void PlayUIAnimation()
    {
        base.PlayUIAnimation();
    }

    //�Q�[���X�^�[�g�̃A�j���[�V�������I���������A���̊֐������s�����B
    //�����Ɏ��̏���������ƃA�j���[�V�������I��������Ɏ��s����܂��B
    //��F(�Q�[���S�̂̈ꎞ��~)
    public void UIAnimationEnd()
    {
        Debug.Log("�A�j���[�V�����I��");
    }
}
