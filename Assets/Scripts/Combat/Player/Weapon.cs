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
    protected Weapon() { }
    public abstract void Attack(GameObject enemy);
    public abstract double GetModifier();
}

public class Bow : Weapon
{
    readonly int attackDamage;
    // damageType attackType = damageType.Piercing;

    public Bow()
    {
        attackDamage = 10;
    }

    public override void Attack(GameObject enemy)
    {
        Debug.Log("Attack with Bow");
        enemyStatus = enemy.GetComponent<EnemyStatus>();
        enemyStatus.UpdateHealth(enemyStatus.health - (int)(GetModifier() * attackDamage));
    }
    public override double GetModifier()
    {
        switch ((int)enemyStatus.armor)
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
    readonly int attackDamage;
    // damageType attackType = damageType.Magic;

    public Staff()
    {
        attackDamage = 10;
    }

    public override void Attack(GameObject enemy)
    {
        Debug.Log("Attack with Staff");
        enemyStatus = enemy.GetComponent<EnemyStatus>();
        enemyStatus.UpdateHealth(enemyStatus.health - (int)(GetModifier() * attackDamage));
    }
    public override double GetModifier()
    {
        return 1;
    }
}

public class Shield : Weapon
{
    readonly int attackDamage;
    // damageType attackType = damageType.True;
    readonly GameObject hero;
    public Shield()
    {
        // this.attackDamage = 0;
        hero = GameObject.FindWithTag("Player");
    }

    public override void Attack(GameObject enemy)
    {
        Debug.Log("Block with shield");
        int shieldCharges = hero.GetComponent<HeroStatus>().shieldCharges;
        shieldCharges = shieldCharges == 2 ? 2 : shieldCharges + 1;
        hero.GetComponent<HeroStatus>().shieldCharges = shieldCharges;
        Debug.Log(hero.GetComponent<HeroStatus>().shieldCharges);
        // ADD these if implementing spiked shield:
        // enemyStatus = enemy.GetComponent<EnemyStatus>();
        // enemyStatus.UpdateHealth(enemyStatus.health - (int) (getModifier() * this.attackDamage));
    }

    public override double GetModifier()
    {
        return 1;
    }
}

public class Sword : Weapon
{
    readonly int attackDamage;
    // damageType attackType = damageType.Slashing;

    public Sword()
    {
        attackDamage = 10;
    }

    public override void Attack(GameObject enemy)
    {
        Debug.Log("Attack with Sword");
        enemyStatus = enemy.GetComponent<EnemyStatus>();
        enemyStatus.UpdateHealth(enemyStatus.health - (int)(GetModifier() * attackDamage));
    }

    public override double GetModifier()
    {
        return (int)enemyStatus.armor switch
        {
            0 => 2,
            1 => 1,
            2 => 0.5,
            _ => 1,
        };
    }
}

public class Mace : Weapon
{
    readonly int attackDamage;
    // damageType attackType = damageType.Bludgeoning;

    public Mace()
    {
        attackDamage = 10;
    }

    public override void Attack(GameObject enemy)
    {
        Debug.Log("Attack with Mace");
        enemyStatus = enemy.GetComponent<EnemyStatus>();
        enemyStatus.UpdateHealth(enemyStatus.health - (int)(GetModifier() * attackDamage));
    }

    public override double GetModifier()
    {
        return (int)enemyStatus.armor switch
        {
            0 => 1,
            1 => 0.5,
            2 => 2,
            _ => 1,
        };
    }
}