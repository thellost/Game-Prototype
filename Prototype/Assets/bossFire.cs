using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;
public class bossFire : MonoBehaviour
{
    private Transform bulletSpawnDump;

    public float offset = 80f;
    [SerializeField] GameObject muzzleParticle;
    [SerializeField] GameObject bullet;

    [SerializeField] Transform target;
    [SerializeField] float cameraShakeIntensity = 5;
    [SerializeField] float cameraShakeFrequency = 1;
    [SerializeField] float cameraShakeTimer = 0.1f;
    [SerializeField] float interval = 0.2f;
    private float timer;
    private Timeline time;

    // Start is called before the first frame update
    void Awake()
    {
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

    // Update is called once per frame
    void Update()
    {
        timer -= time.deltaTime;
        if (timer < 0)
        {
            timer = interval;
            if (target != null)
            {

                GameObject particle = Instantiate(muzzleParticle, bulletSpawnDump);

                particle.GetComponent<Transform>().position = transform.position;
                particle.GetComponent<Transform>().transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);
                particle.GetComponent<Transform>().Rotate(new Vector3(0, 0, offset));

                GameObject bulletTemp = Instantiate(bullet, bulletSpawnDump);
                bulletTemp.GetComponent<Bullet>().setTarget(ref target);
                bullet.GetComponent<Transform>().position = particle.transform.position;

                CameraShake.Instance.ShakeCamera(cameraShakeIntensity, cameraShakeTimer, cameraShakeFrequency);
            }
        }
    }

  
}
