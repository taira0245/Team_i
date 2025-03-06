using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeController : MonoBehaviour
{
    [SerializeField] string next_scene_name = default!;
    public Image fadeImage;  // �t�F�[�h�p�摜
    public float fadeDuration = 1f;  // �t�F�[�h�̎���
    private bool isTransitioning = false;
    private static GameObject mainCanvasInstance;  // Main��Canvas�̃C���X�^���X
    public GameObject[] mainUI;

    void Awake()
    {

        // Main�V�[����Canvas�̂�DontDestroyOnLoad�ɂ���
        if (SceneManager.GetActiveScene().name == "Main" && mainCanvasInstance == null)
        {
            DontDestroyOnLoad(gameObject);
            mainCanvasInstance = gameObject;  // ���̃I�u�W�F�N�g��ۑ�
        }
    }

    void Start()
    {
        // �ŏ��͓����ɂ���iAlpha��0�j
        fadeImage.color = new Color(0, 0, 0, 0);

    }

    public void StartSceneTransition(string next_scene_name)
    {
        if (!isTransitioning)
        {
            StartCoroutine(SwitchScene(next_scene_name));
        }
    }

    // �t�F�[�h�A�E�g���ăV�[����؂�ւ���
    private IEnumerator SwitchScene(string next_scene_name)
    {
        isTransitioning = true;

        // �t�F�[�h�C��
        yield return StartCoroutine(Fade(1));

        // �V�[����؂�ւ���UI�v�f�폜
        SceneManager.LoadScene(next_scene_name);
        foreach (GameObject uiElement in mainUI)
        {
            uiElement.SetActive(false);
        }

        // �V�[�����؂�ւ��܂ő҂i���̃t���[���j
        yield return null;

        // �t�F�[�h�A�E�g
        yield return StartCoroutine(Fade(0));

        isTransitioning = false;
    }

    // �t�F�[�h����
    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, targetAlpha);
    }

    public void BackMain()
    {
        SceneManager.LoadScene("Main");
        Destroy(mainCanvasInstance);  // �ʃV�[���Ɉړ�������Main-Canvas���폜
        mainCanvasInstance = null;
        foreach (GameObject uiElement in mainUI)
        {
            uiElement.SetActive(true);
        }
    }
}