using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowDownFactor = 0.5f;
    public float slowDownDuration = 2f;

    private float fixedDeltaTime;

    void Awake()
    {
        // Make a copy of the fixedDeltaTime, it defaults to 0.02f, but it can be changed in the editor
        this.fixedDeltaTime = Time.fixedDeltaTime;
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
        //Time.timeScale = slowDownFactor;
        //Time.fixedDeltaTime = Time.fixedDeltaTime * Time.timeScale;

        if (Time.timeScale == 1.0f)
            Time.timeScale = 0.5f;
        else
            Time.timeScale = 1.0f;
        // Adjust fixed delta time according to timescale
        // The fixed delta time will now be 0.02 real-time seconds per frame
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
    }
}
