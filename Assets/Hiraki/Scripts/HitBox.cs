using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// �g�k������HitBox�ɃA�^�b�`
/// </summary>
public class HitBox : MonoBehaviour
{
    [Tooltip("�q�b�g����\��������͈͓��ɒe������Ƃ��̃G���A�̐F")]
    [SerializeField] Vector2 expantScale = Vector2.one;
    Vector2 defaultScale = Vector2.one;
    Vector2 subScale = Vector2.one; //�g��l�ƃf�t�H���g�l�̍�
    Coroutine coroutin = null;

    /// <summary>
    /// �f�t�H���g�l�ɖ߂�܂Ŋ|���鎞��
    /// </summary>
    float time = 1.5f;

    private void Awake()
    {
        defaultScale = transform.localScale;
    }


    /// <summary>
    /// �A�^�b�`����Ă���I�u�W�F�N�g�̎��k�J�n����
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
        bool smallerFlag = true;    //�k�������t���O
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
    /// �k�����Ԃ̌v�Z
    /// </summary>
    /// <param name="throw_speed"></param>
    /// <param name="throw_pos"></param>
    /// <returns>�k���Ɋ|���鎞��</returns>
    float ArrivalTimeCalcu(float throw_speed,Vector3 throw_pos)
    {
        var hitPos = transform.position;
        float dir = (hitPos.x - throw_pos.x) * (hitPos.x - throw_pos.x) + (hitPos.y - throw_pos.y) * (hitPos.y - throw_pos.y);
        return dir / throw_speed;
    }
    private void Update()
    {
        //�b��I�ɏ�Ɋg�k���J��Ԃ��悤�ɏ���
        if (coroutin == null) { ContractBoxStart(); }
    }
}
