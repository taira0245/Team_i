using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance;
    public Image fadeImage; // �t�F�[�h�p�� Image
    public float fadeDuration = 1f; // �t�F�[�h����

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �V�[�����܂����ŃI�u�W�F�N�g��ێ�
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(FadeIn()); // �V�[���J�n���Ƀt�F�[�h�C��
    }

    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    private IEnumerator FadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        float timer = fadeDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, timer / fadeDuration);
            yield return null;
        }
        fadeImage.gameObject.SetActive(false);
    }

    private IEnumerator FadeOutAndLoad(string sceneName)
    {
        fadeImage.gameObject.SetActive(true);
        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, timer / fadeDuration);
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
        yield return new WaitForSeconds(0.1f); // �V�[�����[�h�̑ҋ@
        StartCoroutine(FadeIn()); // �t�F�[�h�C��
    }
}

