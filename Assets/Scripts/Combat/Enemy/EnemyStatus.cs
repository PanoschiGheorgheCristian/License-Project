using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyStatus : MonoBehaviour
{
    public enum armorType
    {
        Light,
        Medium,
        Heavy
    }
    public int health;
    public armorType armor;
    public HealthBar healthbar;
    public static bool isBuffed;
    public static bool isDebuffed;
    public static bool isPoisoned;
    public static bool isBleeding;
    public static bool isStunned;
    public static bool isShielded;
    public static int buffPower = 30;
    public static int debuffPower = -30;
    public static float buffDuration = 5f;
    public static float debuffDuration = 5f;
    public static float stunDuration = 1.5f;
    private bool isStunBeingProcessed;
    private bool isPoisonBeingProcessed;
    private bool isBleedBeingProcessed;
    private bool isBuffBeingProcessed;
    private bool isDebuffBeingProcessed;
    private bool isShieldBeingProcessed;
    [SerializeField] List<GameObject> StatusEffectIcons;


    // Start is called before the first frame update
    void Start()
    {
        int difficultyHealthBonus = 0;
        if(EnemyToFight.isElite)
            difficultyHealthBonus = 50;
        else if (EnemyToFight.isBoss)
            difficultyHealthBonus = 100;
        healthbar.SetMaxHealth(100 + (EnemyToFight.layer - 1) * 50 + difficultyHealthBonus);
        UpdateHealth(100 + (EnemyToFight.layer - 1) * 50 + difficultyHealthBonus);
        isBuffed = false;
        isDebuffed = false;
        isPoisoned = false;
        isBleeding = false;
        isStunned = false;
        isShielded = false;
        isStunBeingProcessed = false;
        isPoisonBeingProcessed = false;
        isBleedBeingProcessed = false;
        isBuffBeingProcessed = false;
        isDebuffBeingProcessed = false;
        isShieldBeingProcessed = false;

        foreach (GameObject gameObject in StatusEffectIcons)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPoisoned && !isPoisonBeingProcessed)
        {
            isPoisonBeingProcessed = true;
            StatusEffectIcons[3].SetActive(true);
            StartCoroutine(TakePoisonDamageAfterTime(2, 0));
        }
        if (isBleeding && !isBleedBeingProcessed)
        {
            isBleedBeingProcessed = true;
            StatusEffectIcons[0].SetActive(true);
            StartCoroutine(TakeBleedDamageAfterTime(5));
        }
        if (isStunned && !isStunBeingProcessed)
        {
            isStunBeingProcessed = true;
            StatusEffectIcons[2].SetActive(true);
            StartCoroutine(WaitDuringStun(stunDuration));
        }
        if (isBuffed && !isBuffBeingProcessed)
        {
            isBuffBeingProcessed = true;
            StatusEffectIcons[1].SetActive(true);
            StartCoroutine(LoseBuff(5f));
        }
        if (isDebuffed && !isDebuffBeingProcessed)
        {
            isDebuffBeingProcessed = true;
            StatusEffectIcons[4].SetActive(true);
            StartCoroutine(LoseDebuff(debuffDuration));
        }
        if (isShielded && !isShieldBeingProcessed)
        {
            isShieldBeingProcessed = true;
            StatusEffectIcons[5].SetActive(true);
            StartCoroutine(LoseShield(2f));
        }
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void UpdateHealth(int newHealth)
    {
        health = newHealth;
        healthbar.SetHealth(newHealth);
    }

    private IEnumerator TakePoisonDamageAfterTime(int damage, int count)
    {
        yield return new WaitForSeconds(1.5f);

        if (count < 4)
        {
            UpdateHealth(health - damage);
            StartCoroutine(TakePoisonDamageAfterTime(damage, count + 1));
        }
        else
        {
            isPoisoned = false;
            isPoisonBeingProcessed = false;
            StatusEffectIcons[3].SetActive(false);
        }
    }

    private IEnumerator TakeBleedDamageAfterTime(int damage)
    {
        yield return new WaitForSeconds(3f);

        UpdateHealth(health - damage);
        isBleeding = false;
        isBleedBeingProcessed = false;
        StatusEffectIcons[0].SetActive(false);
    }

    private IEnumerator WaitDuringStun(float duration)
    {
        yield return new WaitForSeconds(duration);

        isStunned = false;
        isStunBeingProcessed = false;
        StatusEffectIcons[2].SetActive(false);

    }

    private IEnumerator LoseBuff(float duration)
    {
        yield return new WaitForSeconds(duration);

        isBuffed = false;
        isBuffBeingProcessed = false;
        StatusEffectIcons[1].SetActive(false);
    }

    private IEnumerator LoseDebuff(float duration)
    {
        yield return new WaitForSeconds(duration);

        isDebuffed = false;
        isDebuffBeingProcessed = false;
        StatusEffectIcons[4].SetActive(false);
    }

    private IEnumerator LoseShield(float duration)
    {
        yield return new WaitForSeconds(duration);

        isShielded = false;
        isShieldBeingProcessed = false;
        StatusEffectIcons[5].SetActive(false);
    }
}
