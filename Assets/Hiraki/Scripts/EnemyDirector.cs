using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDirector : MonoBehaviour
{
    [SerializeField] Spawner[] spawner_;

    List<GameObject> bomList = new();
    List<GameObject> EnemyList = new();

    /// <summary>
    /// Stage���Enemy��Bom��Active�ؑւ�����
    /// </summary>
    /// <param name="enableFlag"></param>
    public void MotionAct(bool enableFlag)
    {
        for (int i = 0; i < spawner_.Length; i++) {
            spawner_[i].enabled = enableFlag;
        }

        if (enableFlag) {
            //������

            foreach(var obj in EnemyList) {
                obj.SetActive(true);
            }
            foreach (var obj in bomList) {
                obj.SetActive(true);
            }
            

        }
        else {
            //�~�߂�
            bomList.Clear();
            EnemyList.Clear();

            GameObject[] Enemy = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < Enemy.Length; i++) {
                EnemyList.Add(Enemy[i]);
                Enemy[i].SetActive(false);
            }
            Debug.Log("Find Enemy Count : " +  Enemy.Length);

            GameObject[] Bom = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < Bom.Length; i++) {
                EnemyList.Add(Bom[i]);
                Bom[i].SetActive(false);
            }
            Debug.Log("Find Bom Count : " + Enemy.Length);
        }

    }
}
