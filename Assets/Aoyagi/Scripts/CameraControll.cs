using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraControll : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private CinemachineBasicMultiChannelPerlin noise;
    [SerializeField] private Coroutine shakeCoroutine;

    private void Awake()
    {
        if (virtualCamera != null)
        {
            noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }

    public void Shake(float intensity, float duration)
    {
        if (noise == null) return;

        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
        }
        shakeCoroutine = StartCoroutine(ShakeCoroutine(intensity, duration));
    }

    private IEnumerator ShakeCoroutine(float intensity, float duration)
    {
        noise.m_AmplitudeGain = intensity;
        yield return new WaitForSeconds(duration);
        noise.m_AmplitudeGain = 0f;
    }
}
