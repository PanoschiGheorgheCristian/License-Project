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
    public static float debuffDuration = 5f;
    public static float stunDuration = 1.5f;
    private bool isStunBeingProcessed;
    private bool isPoisonBeingProcessed;
    private bool isBleedBeingProcessed;
    private bool isBuffBeingProcessed;
    private bool isDebuffBeingProcessed;


    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth(100);
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

    // Update is called once per frame
    void Update()
    {
        if (isPoisoned && !isPoisonBeingProcessed)
        {
            isPoisonBeingProcessed = true;
            StartCoroutine(TakePoisonDamageAfterTime(2, 0));
        }
        if (isBleeding && !isBleedBeingProcessed)
        {
            isBleedBeingProcessed = true;
            StartCoroutine(TakeBleedDamageAfterTime(5));
        }
        if (isStunned && !isStunBeingProcessed)
        {
            isStunBeingProcessed = true;
            StartCoroutine(WaitDuringStun(stunDuration));
        }
        if (isBuffed && !isBuffBeingProcessed)
        {
            isBuffBeingProcessed = true;
            StartCoroutine(LoseBuff(5f));
        }
        if (isDebuffed && !isDebuffBeingProcessed)
        {
            isDebuffBeingProcessed = true;
            StartCoroutine(LoseDebuff(debuffDuration));
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

    private IEnumerator NextLevel()
    {
        Debug.Log("You Won!");
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("Map", LoadSceneMode.Single);
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
