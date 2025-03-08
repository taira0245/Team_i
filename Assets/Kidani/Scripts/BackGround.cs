using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChangeOnTap : MonoBehaviour
{
    [SerializeField] string next_scene_name = default!;
    [SerializeField] ScreenFade screenfade;
    [SerializeField, Range(0, 2)] float fadetime;
    void Update()
    {
        // 画面をタップしたらシーンを変更
        if (Input.GetMouseButtonDown(0))
        {
            screenfade.SetSceneChange(next_scene_name, fadetime);
        }
    }
}