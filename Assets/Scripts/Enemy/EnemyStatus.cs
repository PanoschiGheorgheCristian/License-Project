using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth(100);
    }

    // Update is called once per frame
    void Update()
    {
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
}
