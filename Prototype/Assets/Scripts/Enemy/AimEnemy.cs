using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;
public class AimEnemy : MonoBehaviour
{
    // The target marker.
    [SerializeField] Transform area;
    [SerializeField] GameObject gunMuzzle;
    [SerializeField] EnemyAI ai;
    [SerializeField] Timeline time;
    [SerializeField] float turnRate;
    [SerializeField] float initialTimer = 1.5f;
    [SerializeField] GameObject muzzleParticle;


    [SerializeField] float cameraShakeIntensity = 5;
    [SerializeField] float cameraShakeFrequency = 1;
    [SerializeField] float cameraShakeTimer = 0.1f;

    private Transform bulletSpawnDump;
    [SerializeField] GameObject bullet;
    // Angular speed in radians per sec.
    public float offset = 80f;
    public float radius = 1;
    private Animator anim;

    private Vector2 _startPos;

    private ParticleSystem generatedParticle;
    private Transform target;
    private float timer;
    private Coroutine LookCoroutine;
    private BulletManager bulletManager;
    private void Awake()
    {
        bulletManager = GameObject.FindGameObjectWithTag("Spawner").GetComponent<BulletManager>();
            anim = gameObject.GetComponent<Animator>();
            GameObject bulletSpawnDumpObject = GameObject.Find("BulletSpawn");
            if (bulletSpawnDumpObject == null)
            {
                bulletSpawnDump = transform;
            }
            else
            {
                bulletSpawnDump = bulletSpawnDumpObject.GetComponent<Transform>();
            }
            generatedParticle = null;
        
    }

    private void Start()
    {

        timer = initialTimer;
        target = ai.target;
    }
    private void disableAnimator()
    {
        anim.enabled = false;
        timer = initialTimer;
    }

    private void OnEnable()
    {
        anim.enabled = true;
        timer = initialTimer;
    }
    void Update()
    {

        // Mengubah posisi gun ke world position
            _startPos = area.position;
        if(LookCoroutine == null)
        {
            rotate();
        }
        move();
        timer -= time.deltaTime;
        
    }

    private void rotate()
    {
        if (ai.isFacingRight)
        {

            transform.right = (target.position - transform.position);
        }
        else
        {
            transform.right = -(target.position - transform.position);
        }
        // Smoothly rotate towards the target point.
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);
        transform.Rotate(new Vector3(0, 0, offset));
    }

    private void move()
    {
        Vector2 p = target.position;
        //Hitung supaya 'karet' ketapel berada dalam radius yang ditentukan
        Vector2 dir = p - _startPos;
        if (dir.sqrMagnitude > radius)
            dir = dir.normalized * radius;
        transform.position = _startPos + dir;
    }

    public void fire()
    {
        if (!PauseMenu.isPaused)
        {
            if (timer < 0)
            {

                if (target != null)
                {


                    GameObject particle = bulletManager.GenerateVXFromPool(muzzleParticle, bulletSpawnDump, gunMuzzle.transform.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z));
                    particle.GetComponent<Transform>().Rotate(new Vector3(0, 0, offset));

                    bulletManager.GenerateBulletFromPool(bullet, bulletSpawnDump, particle.transform.position, ref target);

                    CameraShake.Instance.ShakeCamera(cameraShakeIntensity, cameraShakeTimer, cameraShakeFrequency);
                    //EmitFX(bulletTemp.GetComponent<Transform>().rotation, bulletTemp.GetComponent<Transform>().position);
                }
            }
        }
    }

    public void EmitFX(Quaternion rotation , Vector3 bulletPosition)
    {

        generatedParticle.transform.position = bulletPosition;
        generatedParticle.transform.rotation = Quaternion.Euler(0, 90, rotation.eulerAngles.z);

        Debug.Log(generatedParticle.isPlaying);
        if (generatedParticle.isPlaying)
        {
            generatedParticle.Stop();
        } else
        {
            generatedParticle.Play();
        }

        // You can set a fixed duration here if your particle system is looping
        // (I assumed it was not so I used the duration of the particle system to detect the end of it)
    }
}
