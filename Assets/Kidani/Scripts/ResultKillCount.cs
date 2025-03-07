using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GetKillCount : MonoBehaviour
{
    [Header("�|�����G�̐���\��������TextUI"), SerializeField]
    private Text kill_count_text;
    public KillCounter kill_counter; // �C���X�y�N�^�[�ŃA�^�b�`

    void Update()
    {
        if (kill_counter != null)
        {
            kill_count_text.text = "Kill : " + kill_counter.GetKillCount();
        }
    }
}