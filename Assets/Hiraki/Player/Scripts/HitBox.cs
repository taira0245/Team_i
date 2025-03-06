using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 拡縮させるHitBoxにアタッチ
/// </summary>
public class HitBox : MonoBehaviour
{
    [Tooltip("ヒットする可能性がある範囲内に弾があるときのエリアの色")]
    [SerializeField] Vector2 expantScale = Vector2.one;
    Vector2 defaultScale = Vector2.one;
    Vector2 subScale = Vector2.one; //拡大値とデフォルト値の差
    Coroutine coroutin = null;

    /// <summary>
    /// デフォルト値に戻るまで掛かる時間
    /// </summary>
    float time = 1.5f;

    private void Awake()
    {
        defaultScale = transform.localScale;
    }


    /// <summary>
    /// アタッチされているオブジェクトの収縮開始処理
    /// </summary>
    public void ContractBoxStart()
    {
        if (coroutin != null) { StopCoroutine(coroutin); }
        coroutin = StartCoroutine(ContractFlow());
    }

    IEnumerator ContractFlow()
    {
        subScale = expantScale - defaultScale;
        transform.localScale = expantScale;

        float elapsedTime = 0;
        float smallRate = 0;
        bool smallerFlag = true;    //縮小処理フラグ
        while (smallerFlag) {
            yield return null;

            elapsedTime += Time.deltaTime;
            if (elapsedTime >= time) {
                elapsedTime = time;
                smallerFlag = false;
            }
            smallRate = elapsedTime / time;
            transform.localScale = new Vector2(expantScale.x - subScale.x * smallRate, expantScale.y - subScale.y * smallRate);

        }
        coroutin = null;
    }


    /// <summary>
    /// 縮小時間の計算
    /// </summary>
    /// <param name="throw_speed"></param>
    /// <param name="throw_pos"></param>
    /// <returns>縮小に掛かる時間</returns>
    float ArrivalTimeCalcu(float throw_speed,Vector3 throw_pos)
    {
        var hitPos = transform.position;
        float dir = (hitPos.x - throw_pos.x) * (hitPos.x - throw_pos.x) + (hitPos.y - throw_pos.y) * (hitPos.y - throw_pos.y);
        return dir / throw_speed;
    }
    private void Update()
    {
        //暫定的に常に拡縮を繰り返すように処理
        if (coroutin == null) { ContractBoxStart(); }
    }
}
