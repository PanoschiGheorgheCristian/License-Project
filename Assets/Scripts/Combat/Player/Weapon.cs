using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon 
{
    protected enum damageType
    {
        Piercing,
        Slashing,
        Bludgeoning,
        Magic,
        True
    }
    public EnemyStatus enemyStatus;
    protected Weapon() {}
    public abstract void attack(GameObject enemy);
    public abstract double getModifier();
}

public class Bow : Weapon
{
    int attackDamage;
    // damageType attackType = damageType.Piercing;
    
    public Bow()
    {
        this.attackDamage = 10;
    }

    public override void attack(GameObject enemy)
    {
        Debug.Log("Attack with Bow");
        enemyStatus = enemy.GetComponent<EnemyStatus>();
        enemyStatus.UpdateHealth(enemyStatus.health - (int) (getModifier() * this.attackDamage));
    }
    public override double getModifier()
    {
        switch((int) enemyStatus.armor)
        {
            case 0:
                return 0.5;
            case 1:
                return 2;
            case 2:
                return 1;
            default:
                return 1;
        }
    }
}

public class Staff : Weapon
{
    int attackDamage;
    // damageType attackType = damageType.Magic;
    
    public Staff()
    {
        this.attackDamage = 10;
    }

    public override void attack(GameObject enemy)
    {
        Debug.Log("Attack with Staff");
        enemyStatus = enemy.GetComponent<EnemyStatus>();
        enemyStatus.UpdateHealth(enemyStatus.health - (int) (getModifier() * this.attackDamage));
    }
    public override double getModifier()
    {
        return 1;
    }
}

public class Shield : Weapon
{   
    int attackDamage;
    // damageType attackType = damageType.True;
    GameObject hero;
    public Shield()
    {
        // this.attackDamage = 0;
        hero = GameObject.FindWithTag("Player");
    }

    public override void attack(GameObject enemy)
    {
        Debug.Log("Block with shield");
        int shieldCharges = hero.GetComponent<HeroStatus>().shieldCharges;
        shieldCharges = (shieldCharges == 2 ? 2 : shieldCharges + 1);
        hero.GetComponent<HeroStatus>().shieldCharges = shieldCharges;
        Debug.Log(hero.GetComponent<HeroStatus>().shieldCharges);
        // ADD these if implementing spiked shield:
        // enemyStatus = enemy.GetComponent<EnemyStatus>();
        // enemyStatus.UpdateHealth(enemyStatus.health - (int) (getModifier() * this.attackDamage));
    }

    public override double getModifier()
    {
        return 1;
    }
}

public class Sword : Weapon
{
    int attackDamage;
    // damageType attackType = damageType.Slashing;
    
    public Sword()
    {
        this.attackDamage = 10;
    }

    public override void attack(GameObject enemy)
    {
        Debug.Log("Attack with Sword");
        enemyStatus = enemy.GetComponent<EnemyStatus>();
        enemyStatus.UpdateHealth(enemyStatus.health - (int) (getModifier() * this.attackDamage));
    }

    public override double getModifier()
    {
        switch((int) enemyStatus.armor)
        {
            case 0:
                return 2;
            case 1:
                return 1;
            case 2:
                return 0.5;
            default:
                return 1;
        }
    }
}

public class Mace : Weapon
{
    int attackDamage;
    // damageType attackType = damageType.Bludgeoning;
    
    public Mace()
    {
        this.attackDamage = 10;
    }

    public override void attack(GameObject enemy)
    {
        Debug.Log("Attack with Mace");
        enemyStatus = enemy.GetComponent<EnemyStatus>();
        enemyStatus.UpdateHealth(enemyStatus.health - (int) (getModifier() * this.attackDamage));
    }

    public override double getModifier()
    {
        switch((int) enemyStatus.armor)
        {
            case 0:
                return 1;
            case 1:
                return 0.5;
            case 2:
                return 2;
            default:
                return 1;
        }
    }
}