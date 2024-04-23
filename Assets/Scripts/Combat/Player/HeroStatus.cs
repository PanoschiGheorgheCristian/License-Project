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
    }

    void Update()
    {
        if (health <= 0)
        {
            alive = 0;
            health = 100;
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
}
