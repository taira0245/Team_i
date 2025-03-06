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
        //�f�o�b�O�p
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

    //�^�C�}�[���ꎞ��~
    public void StopAnim()
    {
        animator.SetFloat("MoveTime", 0);
    }

    //�^�C�}�[���Đ�
    public void PlayAnim()
    {
        animator.SetFloat("MoveTime", 1);
    }

    //��������Q�[���I���̏��������s
    public void GameEnd()
    {
        Debug.Log("�Q�[���I�[�o�[");
    }
}
