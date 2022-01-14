using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Chronos;

public class PlayerStatManager : MonoBehaviour
{
    [Header("Player Basic Stat")]
    [SerializeField] public float maxPlayerHP = 100;
    [SerializeField] public float maxPlayerEnergy = 50;
    [SerializeField] float invicTimer = 1;
    [SerializeField] float knockbackPower = 10;

    
    [SerializeField] float cameraShakeIntensity = 5;
    [SerializeField] float cameraShakeFrequency = 1;
    [SerializeField] float cameraShakeTimer = 0.1f;

    [Header("Energy Parameter")]
    [SerializeField] float energyDrainRate = 0.5f;
    [SerializeField] float energyRegenRate = 0.5f;

    [Header("UI Reference")]
    [SerializeField] private Slider hpProgressUI = null;
    [SerializeField] private Slider energyProgressUI = null;

    public TimeManager timeManager;
    public float currentHp;
    public float currentEnergy;

    public bool isDead;
    public bool isInvulnerable { get; private set; }
    private float internalTimer;
    private PlayerVelocity player;
    private CharacterAttack attack;
    private PlayerAnimator animator;
    private Timeline time;
    // Start is called before the first frame update
    void Awake()
    {
        setPlayerStat();
        player = GetComponent<PlayerVelocity>();
        attack = GetComponent<CharacterAttack>();
        animator = GetComponent<PlayerAnimator>();
        time = GetComponent<Timeline>();
    }

    private void setPlayerStat()
    {
        //perlu di ubah kedepannya karena bakal ada implementasi save
        currentHp = GameManager.Progress.currentHp;
        if(currentHp <= 0)
        {
            currentHp = maxPlayerHP;
        }
        Debug.Log(currentHp);
        currentEnergy = maxPlayerEnergy;


        hpProgressUI.maxValue = maxPlayerHP;
        hpProgressUI.value = currentHp;
        energyProgressUI.maxValue = maxPlayerEnergy;
        energyProgressUI.value = currentEnergy;

    }

    public void takeDamage(float dmg, Vector3 enemyPosition)
    {
        if (!isInvulnerable)
        {
            isInvulnerable = true;
            internalTimer = 0;
            BloodEffect.Instance.setBlood();
            CameraShake.Instance.ShakeCamera(cameraShakeIntensity, cameraShakeTimer, cameraShakeFrequency);
            currentHp -= dmg;
            GameManager.Progress.currentHp = currentHp;
            hpProgressUI.value = currentHp;

            Vector2 direction = transform.position - enemyPosition;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float xside = Mathf.Cos(angle * Mathf.PI / 180) * knockbackPower;
            float yside = Mathf.Sin(angle * Mathf.PI / 180) * knockbackPower;

            player.knockback(xside, yside);
        }
    }
    
    public void setInvulnerable()
    {
        isInvulnerable = true;
        internalTimer = 0;
    }



    // Update is called once per frame
    void Update()
    {
        energyProgressUI.value = currentEnergy;
        internalTimer += time.deltaTime;
        if(internalTimer > invicTimer)
        {
            isInvulnerable = false;
        }
        if(currentHp <= 0)
        {
            dead();
            
        }


        if (timeManager.isInSlowmo)
        {
            currentEnergy -= energyDrainRate * Time.unscaledDeltaTime;
            
            if (currentEnergy <= 0)
            {
                timeManager.stopSlowMotion();
            }
        }
        else
        {
            currentEnergy += (energyRegenRate * Time.unscaledDeltaTime);
            currentEnergy = Mathf.Clamp(currentEnergy, 0, maxPlayerEnergy);
        }

    }

    public void dead()
    {
        animator.SetDead(true);
        animator.enabled = false;
        attack.enabled = false;
       
        if (!isDead)
        {
            StartCoroutine(resetScene());
        }
        GameManager.Progress.currentHp = maxPlayerHP;
        GameManager.Save();
        isDead = true;

    }

    IEnumerator resetScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
