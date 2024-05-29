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
    private bool isStunBeingProcessed;
    private bool isPoisonBeingProcessed;
    private bool isBleedBeingProcessed;
    private bool isBuffBeingProcessed;
    private bool isDebuffBeingProcessed;

    // Start is called before the first frame update
    void Start()
    {
        healthbar.SetMaxHealth(100);
        string json = SaveObject.getJsonSave();
        SaveObject saveObject = JsonUtility.FromJson<SaveObject>(json);
        UpdateHealth(saveObject.heroHealth);

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
    }

    void Update()
    {
        if(isPoisoned && !isPoisonBeingProcessed)
        {
            isPoisonBeingProcessed = true;
            StartCoroutine(TakePoisonDamageAfterTime(2, 0));
        }
        if(isBleeding && !isBleedBeingProcessed)
        {
            isBleedBeingProcessed = true;
            StartCoroutine(TakeBleedDamageAfterTime(5));
        }
        if (isStunned && !isStunBeingProcessed)
        {
            isStunBeingProcessed = true;
            StartCoroutine(WaitDuringStun(1.5f));
        }
        if(isBuffed && !isBuffBeingProcessed)
        {
            isBuffBeingProcessed = true;
            StartCoroutine(LoseBuff(5f));
        }
        if(isDebuffed && !isDebuffBeingProcessed)
        {
            isDebuffBeingProcessed = true;
            StartCoroutine(LoseDebuff(5f));
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
        }
    }

    private IEnumerator TakeBleedDamageAfterTime(int damage)
    {
        yield return new WaitForSeconds(3f);

        UpdateHealth(health - damage);
        isBleeding = false;
        isBleedBeingProcessed = false;
    }

    private IEnumerator WaitDuringStun(float duration)
    {
        yield return new WaitForSeconds(duration);

        isStunned = false;
        isStunBeingProcessed = false;
    }

    private IEnumerator LoseBuff(float duration)
    {
        yield return new WaitForSeconds(duration);

        isBuffed = false;
        isBuffBeingProcessed = false;
    }

    private IEnumerator LoseDebuff(float duration)
    {
        yield return new WaitForSeconds(duration);

        isDebuffed = false;
        isDebuffBeingProcessed = false;
    }
}