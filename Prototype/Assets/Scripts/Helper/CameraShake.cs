using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    //singleton
    public static CameraShake Instance { get; private set; }


    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shaketimer;
    private float sta;


   
    private void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    //origin tempat dia muncul dimana transform.enemy
    //text dmg nya berapa ?????
    //jadi kamu butuh string
    public void ShakeCamera(float intensity, float time, float frequency = 1)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frequency;
        shaketimer = time;

        //instatiate
    }

    private void Update()
    {
        if(shaketimer > 0)
        {
            shaketimer -= Time.deltaTime;
            if(shaketimer <= 0f)
            {
                //shake selesai
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }
}
