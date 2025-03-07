using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    [Header("SceneFade設定")]
    [SerializeField] Color fadeColor_ = Color.black;
    [SerializeField] Image fadePanel_;
    [SerializeField] Image maskPanel_ = null;
    public static bool isFading_ = false;
    float fadeAlpha_ = 0;

    /// <summary>
    /// フェードアウトのみを行うフラグ
    /// </summary>
    bool fadeOutOnly_ = false;


    private void OnGUI()
    {
        if (isFading_) {
            //Colorの更新
            fadeColor_.a = fadeAlpha_;
            fadePanel_.color = fadeColor_;
            if (maskPanel_ != null) {
                maskPanel_.color = fadeColor_;
            }
            
        }
    }


    /// <summary>
    /// フェードしながら、現シーン破棄と次シーンの読込を行う。
    /// フェード途中でSEを鳴らさない場合は、SEKeyを渡さない。
    /// </summary>
    public void SetSceneChange(string sceneName, float feadTime = 1.0f,string SEKey = null)
    {
        StartCoroutine(FadeFlow(sceneName, feadTime, SEKey));
    }


    /// <summary>
    /// 画面のフェードアウトのみを行う
    /// </summary>
    /// <param name="fadeSpeed"></param>
    /// <param name="SEKey"></param>
    public IEnumerator FadeOutFlow(float fadeSpeed, string SEKey = null)
    {
        isFading_ = true;
        while (true) {
            yield return null;

            float alpha = Time.deltaTime / fadeSpeed;
            fadeAlpha_ = Mathf.Min(fadeAlpha_ + alpha, 1.0f);
            if (fadeAlpha_ >= 1.0f) { break; }

            //フェード中にSEが入る場合の処理
            if (SEKey != null && fadeAlpha_ > 0.5f) { AudioMG.PlaySE(SEKey); SEKey = null; }
        }

        isFading_ = false;
    }
    

    /// <summary>
    /// シーン遷移処理
    /// </summary>
    /// <param name="nextScene"></param>
    /// <param name="fadeSpeed"></param>
    /// <param name="SEKey"></param>
    /// <returns></returns>
    IEnumerator FadeFlow(string nextScene,float fadeSpeed, string SEKey)
    {
        //画面のフェードアウト処理
        yield return FadeOutFlow(fadeSpeed,SEKey);

        isFading_ = true;
        if (!fadeOutOnly_) {
            //シーン遷移処理
            DontDestroyOnLoad(this.gameObject);
            SceneManager.LoadScene(nextScene);


            //画面のフェードイン処理
            while (true) {
                yield return null;

                float alpha = Time.deltaTime / fadeSpeed;
                fadeAlpha_ = Mathf.Max(fadeAlpha_ - alpha, 0.0f);

                if (fadeAlpha_ <= 0.0f) { break; }

            }

            Destroy(this.gameObject);
        }

        fadeOutOnly_ = false;
        isFading_ = false;
    }

}
