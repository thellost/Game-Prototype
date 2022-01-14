using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript: MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject flare;
    [SerializeField] Transform muzzle;
    [SerializeField] Transform target;
    [SerializeField] AudioClip shootSound;
    [SerializeField] float cameraShakeIntensity = 5;
    [SerializeField] float cameraShakeFrequency = 1;
    [SerializeField] float cameraShakeTimer = 0.1f;
    private float offset = 180f;
    private BulletManager bulletManager;
    private Transform bulletSpawnDump;

    private void Awake()
    {
        bulletManager = GameObject.FindGameObjectWithTag("Spawner").GetComponent<BulletManager>();
        bulletSpawnDump = bulletManager.gameObject.GetComponent<Transform>();
    }
    private void fireBullet()
    {
        SoundManager.Instance.PlaySFX(shootSound, 0.5f, true);

        GameObject particle = bulletManager.GenerateVXFromPool(flare, bulletSpawnDump, muzzle.transform.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z));
        particle.GetComponent<Transform>().Rotate(new Vector3(0, 0, offset));

        bulletManager.GenerateBulletFromPool(bullet, bulletSpawnDump, muzzle.transform.position, ref target);

        CameraShake.Instance.ShakeCamera(cameraShakeIntensity, cameraShakeTimer, cameraShakeFrequency);
    }
}
