using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class OptionManager : MonoBehaviour
{
    [Header("ここにCanvasの中のOptionUIを入れる")]
    [SerializeField] private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //tureならポーズs画面・falseならゲーム画面
    public void PushOption(bool is_option)
    {
        animator.SetBool("option", is_option);
    }

    //trueなら設定画面・falseならポーズ画面
    public void PushSetting(bool is_setting)
    {
        animator.SetBool("setting", is_setting);
    }

    //ボタンのインスペクター画面で、リスタートするシーン番号を入れる
    public void PushReStart(int scene_num)
    {
        StartCoroutine(LoadSceneCoroutine(scene_num));
    }

    public void PushTitleChack()
    {
        animator.SetBool("title_chack", true);
    }

    public void TitleChackNo()
    {
        animator.SetBool("title_chack", false);
    }

    //シーン読み込み開始
    public void TitleChackYes(int scene_num)
    {
        animator.SetBool("title_chack", false);
        animator.SetBool("option", false);
        StartCoroutine(LoadSceneCoroutine(scene_num));
    }

    

    //シーン読み込み
    private IEnumerator LoadSceneCoroutine(int scene_num)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene(scene_num);
    }
}
