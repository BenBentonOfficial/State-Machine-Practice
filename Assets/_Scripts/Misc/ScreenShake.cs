using System;
using UnityEngine;
using Cinemachine;

public class ScreenShake : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin noise;
    private Timer shakeTimer;
    
    private void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        noise = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Start()
    {
        cinemachineVirtualCamera.Follow.gameObject.GetComponent<Player>().onDamage += ShakeCamera;
    }

    private void ShakeCamera()
    {
        noise.m_AmplitudeGain = 1f;
        Invoke("ResetShake", 0.1f);
    }

    private void ResetShake()
    {
        noise.m_AmplitudeGain = 0f;
    }
}
