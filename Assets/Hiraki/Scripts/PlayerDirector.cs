using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerDirector : MonoBehaviour
{
    [SerializeField] HitBox hitBox_;
    Player player_ = null;
    public int CurrentCount { get => player_.count; }
    int oldHitCnt = 0;  //１フレーム前の、敵に当てた回数
    public int CurrentHP { get => player_.hp; }
    int oldHp = 0; //1フレーム前の、HP

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
    /// カウントの変化の確認処理
    /// </summary>
    /// <returns>値の変更の有無</returns>
    public bool CheckChangeCount() {
        if (CurrentCount == oldHitCnt) return false;

        oldHitCnt = CurrentCount;
        return true;
    }

    /// <summary>
    /// HP変化の確認処理
    /// </summary>
    /// <returns>値の変更の有無</returns>
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
