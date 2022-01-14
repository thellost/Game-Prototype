using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;
public class BossFire : MonoBehaviour
{
    private Transform bulletSpawnDump;

    private float offset = 180f;
    [SerializeField] GameObject muzzleParticle;
    [SerializeField] GameObject bullet;
    [SerializeField] AudioClip shootSound;
    [SerializeField] Transform target;
    [SerializeField] float cameraShakeIntensity = 5;
    [SerializeField] float cameraShakeFrequency = 1;
    [SerializeField] float cameraShakeTimer = 0.1f;
    [SerializeField] float interval = 0.2f;
    private float timer;
    private Timeline time;
    private BulletManager bulletManager;
    // Start is called before the first frame update
    void Awake()
    {
        bulletManager = GameObject.FindGameObjectWithTag("Spawner").GetComponent<BulletManager>();
        GameObject bulletSpawnDumpObject = GameObject.Find("BulletSpawn");
        time = GetComponent<Timeline>();
        if (bulletSpawnDumpObject == null)
        {
            bulletSpawnDump = transform;
        }
        else
        {
            bulletSpawnDump = bulletSpawnDumpObject.GetComponent<Transform>();
        }
    }
    private void OnEnable()
    {
        SoundManager.Instance.PlaySFX(shootSound,0.5f,true);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= time.deltaTime;
        if (timer < 0)
        {
            timer = interval;
            if (target != null)
            {

               
                GameObject particle = bulletManager.GenerateVXFromPool(muzzleParticle, bulletSpawnDump, transform.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z));
                particle.GetComponent<Transform>().Rotate(new Vector3(0, 0, offset));


                bulletManager.GenerateBulletFromPool(bullet, bulletSpawnDump, transform.position, ref target);

                CameraShake.Instance.ShakeCamera(cameraShakeIntensity, cameraShakeTimer, cameraShakeFrequency);
            }
        }
    }

  
}
