using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class HeroStatus : MonoBehaviour
{
    public int health;
    public int alive;
    public HealthBar healthbar;
    public int shieldCharges;
    public bool isShielded;
    public static bool isBuffed;
    public static bool isDebuffed;
    public static bool isPoisoned;
    public static bool isBleeding;
    public static bool isStunned;
    public static int buffPower = 30;
    public static int debuffPower = -30;
    public static float buffDuration = 5f;
    private bool isStunBeingProcessed;
    private bool isPoisonBeingProcessed;
    private bool isBleedBeingProcessed;
    private bool isBuffBeingProcessed;
    private bool isDebuffBeingProcessed;
    [SerializeField] List<GameObject> StatusEffectIcons;

    // Start is called before the first frame update
    void Start()
    {
        string json = SaveObject.getJsonSave();
        SaveObject saveObject = JsonUtility.FromJson<SaveObject>(json);
        bool check = false;
        foreach (string iteratorString in saveObject.curses)
        {
            if( string.Equals("Haunted",iteratorString))
            {
                healthbar.SetMaxHealth(70);
                check = true;
            }
        }
        if(!check)
        {
            healthbar.SetMaxHealth(100);
            UpdateHealth(saveObject.heroHealth);
        }
        else
            UpdateHealth(saveObject.heroHealth < 70 ? saveObject.heroHealth : 70);

        alive = 1;
        shieldCharges = 0;
        isShielded = false;
        isBuffed = false;
        isDebuffed = false;
        isPoisoned = false;
        isBleeding = false;
        isStunned = false;
        isStunBeingProcessed = false;
        isPoisonBeingProcessed = false;
        isBleedBeingProcessed = false;
        isBuffBeingProcessed = false;
        isDebuffBeingProcessed = false;

        foreach(GameObject gameObject in StatusEffectIcons)
        {
            gameObject.SetActive(false);
        }

        if(gameObject.GetComponent<PlayerController>().cuesedDelay > 0f)
            StatusEffectIcons[4].SetActive(true);
    }

    void Update()
    {
        if(isPoisoned && !isPoisonBeingProcessed)
        {
            isPoisonBeingProcessed = true;
            StatusEffectIcons[3].SetActive(true);
            StartCoroutine(TakePoisonDamageAfterTime(2, 0));
        }
        if(isBleeding && !isBleedBeingProcessed)
        {
            isBleedBeingProcessed = true;
            StatusEffectIcons[0].SetActive(true);
            StartCoroutine(TakeBleedDamageAfterTime(5));
        }
        if (isStunned && !isStunBeingProcessed)
        {
            isStunBeingProcessed = true;
            StatusEffectIcons[2].SetActive(true);
            StartCoroutine(WaitDuringStun(1.5f));
        }
        if(isBuffed && !isBuffBeingProcessed)
        {
            isBuffBeingProcessed = true;
            StatusEffectIcons[1].SetActive(true);
            StartCoroutine(LoseBuff(buffDuration));
        }
        if(isDebuffed && !isDebuffBeingProcessed)
        {
            isDebuffBeingProcessed = true;
            StatusEffectIcons[5].SetActive(true);
            StartCoroutine(LoseDebuff(5f));
        }
        if (isShielded)
        {
            StatusEffectIcons[6].SetActive(true);
        }
        else if(StatusEffectIcons[6].activeSelf)
        {
            StatusEffectIcons[6].SetActive(false);
        }
        if (health <= 0)
        {
            alive = 0;
        }
        if (alive == 0)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            alive = -1;
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

        if(count < 4)
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
        StatusEffectIcons[0].SetActive(false);
        isBleedBeingProcessed = false;
    }

    private IEnumerator WaitDuringStun(float duration)
    {
        yield return new WaitForSeconds(duration);

        isStunned = false;
        StatusEffectIcons[2].SetActive(false);
        isStunBeingProcessed = false;
    }

    private IEnumerator LoseBuff(float duration)
    {
        yield return new WaitForSeconds(duration);

        isBuffed = false;
        StatusEffectIcons[1].SetActive(false);
        isBuffBeingProcessed = false;
    }

    private IEnumerator LoseDebuff(float duration)
    {
        yield return new WaitForSeconds(duration);

        isDebuffed = false;
        isDebuffBeingProcessed = false;
        StatusEffectIcons[5].SetActive(false);
    }
}