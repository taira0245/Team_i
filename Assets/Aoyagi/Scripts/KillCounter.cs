using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCounter : MonoBehaviour
{
    [Header("�|�����G�̐�"), SerializeField] 
    private int kill_count;
    [Header("�|�����G�̐���\��������TextUI"), SerializeField]
    private Text kill_count_text;

    //�L���X�R�A�̃��Z�b�g
    private void Start()
    {
        kill_count = 0;
        kill_count_text.text = "" + kill_count;
    }

    //���̊֐��������s����ƃL���X�R�A��1������
    public void KillCountPlus()
    {
        kill_count++;
        kill_count_text.text = "" + kill_count;
    }

    //�L���J�E���g�擾�p
    public int GetKillCount()
    {
        return kill_count;
    }
}
