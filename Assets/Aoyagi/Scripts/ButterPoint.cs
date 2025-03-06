using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class ButterPoint : MonoBehaviour
{
    [Header("�m�[�c"), SerializeField] 
    private Image notes_ring;
    [Header("�m�[�c�̑傫��"), SerializeField]
    private float scale, max_scale;
    [Header("�m�[�c���������Ȃ�X�s�[�h"), SerializeField]
    private float notes_speed;
    [Header("�Œ胊���O�̃A�j���[�V����"),SerializeField]
    private Animator animator;

    private void Start()
    {
        scale = max_scale;
    }

    public void Update()
    {
        NotesReduction();

        if (Input.GetButtonDown("Fire1"))
        {
            if(scale >= 0.9 && scale <= 1.0f)
            {
                animator.SetTrigger("success");
                NotesReset();
            }
        }
    }

    //�m�[�c�����X�ɏ���������
    public void NotesReduction()
    {
        scale -= notes_speed * Time.deltaTime;
        notes_ring.transform.localScale = new Vector3(scale, scale, scale);
        if (scale <= 0)
        {
            NotesReset();
        }
    }

    //�m�[�c�����̈ʒu�ɖ߂�
    public void NotesReset()
    {
        scale = max_scale;
    }
}
