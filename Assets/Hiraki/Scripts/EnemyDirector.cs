using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirector : MonoBehaviour
{
    [SerializeField] Spawner[] spawner_;

    public void StopMotion(bool enableFlag)
    {
        for (int i = 0; i < spawner_.Length; i++) {
            spawner_[i].enabled = enableFlag;
        }
    }
}
