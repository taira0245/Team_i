using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerDirector : MonoBehaviour
{
    [SerializeField] HitBox hitBox_;
    Player player_ = null;
    public int CurrentCount { get => player_.count; }
    int oldHitCnt = 0;  //�P�t���[���O�́A�G�ɓ��Ă���
    public int CurrentHP { get => player_.hp; }
    int oldHp = 0; //1�t���[���O�́AHP

    private void Awake()
    {
        player_ = GetComponent<Player>();
    }

    public void SceneInit()
    {
        oldHp = player_.hp;
        oldHitCnt = player_.count;
#if UNITY_EDITOR
        Debug.Log("<color=green>player_ : " + player_ + "</color>");
#endif
    }

    /// <summary>
    /// �J�E���g�̕ω��̊m�F����
    /// </summary>
    /// <returns>�l�̕ύX�̗L��</returns>
    public bool CheckChangeCount() {
        if (CurrentCount == oldHitCnt) return false;

        oldHitCnt = CurrentCount;
        return true;
    }

    /// <summary>
    /// HP�ω��̊m�F����
    /// </summary>
    /// <returns>�l�̕ύX�̗L��</returns>
    public bool CheckChangeHP()
    {
        if (CurrentHP == oldHp) return false;

        oldHp = CurrentHP;
        return true;
    }


    public void StopMotion(bool enableFlag)
    {
        hitBox_.enabled = enableFlag;
        player_.enabled = enableFlag;
    }

}
