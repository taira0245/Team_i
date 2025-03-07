using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirector : MonoBehaviour
{
    [SerializeField] Spawner spawner_;

    public void StopMotion(bool enableFlag)
    {
        spawner_.enabled = enableFlag;
    }
}
