using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageDisp : MonoBehaviour
{
    [SerializeField] Image imageObj;
    [SerializeField] SpritesData spData;

    /// <summary>
    /// 表示スプライトの変更
    /// </summary>
    /// <param name="arrayIdx"></param>
    public void ChangeSprite(int arrayIdx = 0) {
        Debug.Log("Sprite Count : " + spData.Count);
        Debug.Log("Sprite Index : " + arrayIdx);
        imageObj.sprite = spData.GetSprite(arrayIdx); }
    public void ChangeSprite(Color color, int arrayIdx = 0)
    {
        ChangeSprite(arrayIdx);
        imageObj.color = color;
    }



    Coroutine coroutine_ = null;
    float fadeAlpha = 0;
    Color fadeColor = Color.white;
    /// <summary>
    /// アニメーション付きのスプライト変更
    /// </summary>
    /// <param name="fadeTime"></param>
    /// <param name="arrayIdx"></param>
    public void AnimChageSprite(float fadeTime, int arrayIdx)
    {
        if (coroutine_ != null) { StopCoroutine(coroutine_); }

        fadeAlpha = 0;
        fadeColor.a = fadeAlpha;
        imageObj.color = fadeColor;
        ChangeSprite(arrayIdx);
        fadeColor = imageObj.color;
        coroutine_ = StartCoroutine(AnimDispFlow(fadeTime));

    }

    IEnumerator AnimDispFlow(float fadeTime)
    {
        fadeAlpha = 0;
        fadeColor.a = fadeAlpha;
        imageObj.color = fadeColor;

        while (true) {
            yield return null;

            float alpha = Time.unscaledDeltaTime / fadeTime;
            fadeAlpha = Mathf.Min(fadeAlpha, 1.0f);
            if (fadeAlpha >= 1.0f) { break; }
        }
        coroutine_ = null;
    }

    private void OnGUI()
    {
        if(coroutine_ != null) {
            //透明度の更新
            fadeColor.a = fadeAlpha;
            imageObj.color = fadeColor;
        }
    }
}
