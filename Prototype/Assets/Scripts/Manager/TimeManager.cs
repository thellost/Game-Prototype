using UnityEngine;
using Chronos;

public class TimeManager : Singleton<TimeManager>
{
    [SerializeField] private AudioClip bulletTimeSfx;
    [SerializeField] Ghost ghostEffect;
    public bool slowMotionActivated;
    public float enemiesSlowDownFactor = 0.2f;
    public float playerSlowDownFactor = 0.4f;
    public float bulletSlowDownFactor = 0.1f;
    public float slowDownDuration = 3f;
    public PlayerStatManager playerStat;

    public bool isInSlowmo;
    private Clock enemyClock, playerClock, bulletClock;
    private BulletTimeLight bulletTimeEffect;
    private LerpingMaterial materialEffect;

    private void Start()
    {
        enemyClock = Timekeeper.instance.Clock("Enemies");
        playerClock = Timekeeper.instance.Clock("Player");
        bulletClock = Timekeeper.instance.Clock("Bullet");
        bulletTimeEffect = GetComponent<BulletTimeLight>();
        materialEffect = GetComponent<LerpingMaterial>();
    }

    void Update()
    {
        // Time.timeScale      += (1f / slowDownDuration) * Time.unscaledDeltaTime;
        // Time.fixedDeltaTime += (0.01f / slowDownDuration) * Time.unscaledDeltaTime;
        // Time.timeScale      = Mathf.Clamp(Time.timeScale, 0f, 1f);
        // Time.fixedDeltaTime = Mathf.Clamp(Time.fixedDeltaTime, 0f, 0.01f);
    }

    public void DoSlowMotion()
    {
        if (slowMotionActivated)
        {
            if (playerStat.currentEnergy > 0)
            {
                if (!isInSlowmo)
                {
                    if (ghostEffect != null)
                    {

                        ghostEffect.makeGhost = true;
                    }
                    isInSlowmo = true;
                    bulletTimeEffect.startEffect(false);
                    materialEffect.setMaterialEffect(false);
                    enemyClock.localTimeScale = enemiesSlowDownFactor;
                    playerClock.localTimeScale = playerSlowDownFactor;
                    bulletClock.localTimeScale = bulletSlowDownFactor;
                    //Invoke("stopSlowMotion", slowDownDuration);

                    SoundManager.Instance.PlaySFX(bulletTimeSfx);
                }

                else
                {
                    stopSlowMotion();
                }
            }
        }
    }

    public void stopSlowMotion()
    {
        if (ghostEffect != null)
        {

            ghostEffect.makeGhost = false;
        }
        bulletTimeEffect.startEffect(true);
        materialEffect.setMaterialEffect(true);
        enemyClock.localTimeScale = 1;
        playerClock.localTimeScale = 1;
        bulletClock.localTimeScale = 1;
        isInSlowmo = false;
    }
}
