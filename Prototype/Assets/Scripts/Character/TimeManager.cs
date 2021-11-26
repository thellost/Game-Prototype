using UnityEngine;
using Chronos;

public class TimeManager : MonoBehaviour
{
    public float slowDownFactor = 0.5f;
    public float slowDownDuration = 2f;
    public PlayerStatManager playerStat;

    private float fixedDeltaTime;
    private bool isInSlowmo;
    private Clock enemyClock;

    void Awake()
    {
        // Make a copy of the fixedDeltaTime, it defaults to 0.02f, but it can be changed in the editor
        this.fixedDeltaTime = Time.fixedDeltaTime;
    }

    private void Start()
    {
        enemyClock = Timekeeper.instance.Clock("Enemies");
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

        if (playerStat.currentEnergy > 0)
        {
            if (!isInSlowmo)
            {
                isInSlowmo = true;
                enemyClock.localTimeScale = slowDownFactor;
                Invoke("stopSlowMotion", slowDownDuration);
            }
        }
        
    }

    public void stopSlowMotion()
    {
        enemyClock.localTimeScale = 1;
        isInSlowmo = false;
    }
}
