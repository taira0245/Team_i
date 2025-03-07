using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [Tooltip("ポーズ処理の際に表示状態を切り替えるゲームオブジェクトを設定")]
    [SerializeField] GameObject activeObj;
    bool isPause = false;

    private void Awake()
    {
         activeObj.SetActive(false);
    }

    public IEnumerator PauseFlow()
    {
        activeObj.SetActive(true);
        isPause = true;

        while (isPause) {
            yield return null;
            if (Input.GetKeyDown(KeyCode.Escape)) { UnPause(); break; }
        }

        activeObj.SetActive(false);
    }

    public void UnPause() => isPause = false;
}
