using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChangeOnTap : MonoBehaviour
{
    [SerializeField] string next_scene_name = default!;
    void Update()
    {
        // 画面をタップしたらシーンを変更
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(next_scene_name); // 遷移先のシーン名を指定
        }
    }
}