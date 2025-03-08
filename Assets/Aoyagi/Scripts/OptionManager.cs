using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class OptionManager : MonoBehaviour
{
    [Header("������Canvas�̒���OptionUI������")]
    [SerializeField] private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //ture�Ȃ�|�[�Ys��ʁEfalse�Ȃ�Q�[�����
    public void PushOption(bool is_option)
    {
        animator.SetBool("option", is_option);
        AudioMG.PlaySE("Click");
    }

    //true�Ȃ�ݒ��ʁEfalse�Ȃ�|�[�Y���
    public void PushSetting(bool is_setting)
    {
        animator.SetBool("setting", is_setting);
        AudioMG.PlaySE("Click");
    }

    //�{�^���̃C���X�y�N�^�[��ʂŁA���X�^�[�g����V�[���ԍ�������
    public void PushReStart(int scene_num)
    {
        StartCoroutine(LoadSceneCoroutine(scene_num));
        AudioMG.PlaySE("Click");
    }

    public void PushTitleChack()
    {
        animator.SetBool("title_chack", true);
        AudioMG.PlaySE("Click");
    }

    public void TitleChackNo()
    {
        animator.SetBool("title_chack", false);
        AudioMG.PlaySE("Click");
    }

    //�V�[���ǂݍ��݊J�n
    public void TitleChackYes(int scene_num)
    {
        animator.SetBool("title_chack", false);
        animator.SetBool("option", false);
        StartCoroutine(LoadSceneCoroutine(scene_num));
    }

    //�V�[���ǂݍ���
    private IEnumerator LoadSceneCoroutine(int scene_num)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene(scene_num);
    }
}
