using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endless : MonoBehaviour
{
    [SerializeField] string nextSceneName = default!;
    [SerializeField] ScreenFade fade_;
    [SerializeField,Range(0,4)] float fadeTime = 1.0f;

    public void SceneChange() => fade_.SetSceneChange(nextSceneName, fadeTime);
}
