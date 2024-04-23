using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    public double attackCooldown;
    public int attackDamage;
    protected enum DamageType
    {
        Piercing,
        Slashing,
        Bludgeoning,
        Magic,
        True
    }
    public EnemyStatus enemyStatus;
    protected Weapon() { }
    public abstract void Attack(GameObject enemy, GameObject hero);
    public abstract double GetModifier();
}

public class Bow : Weapon, ILongRangeWeapon
{
    // damageType attackType = damageType.Piercing;

    public Bow()
    {
        attackDamage = 10;
        attackCooldown = 1;
    }

    public override void Attack(GameObject enemy, GameObject hero)
    {
        Debug.Log("Attack with Bow");
        enemyStatus = enemy.GetComponent<EnemyStatus>();
        enemyStatus.UpdateHealth(enemyStatus.health - (int)(GetModifier() * attackDamage));
    }
    public override double GetModifier()
    {
        return (int)enemyStatus.armor switch
        {
            0 => 0.5,
            1 => 2,
            2 => 1,
            _ => 1,
        };
    }
}

public class Staff : Weapon, IMagicWeapon
{
    // damageType attackType = damageType.Magic;

    public Staff()
    {
        attackDamage = 10;
        attackCooldown = 1;
    }

    public override void Attack(GameObject enemy, GameObject hero)
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

public class Shield : Weapon, IShieldWeapon
{
    // damageType attackType = damageType.True;
    readonly GameObject hero;
    public Shield()
    {
        // this.attackDamage = 0;
        hero = GameObject.FindWithTag("Player");
        attackCooldown = 1;
    }

    public override void Attack(GameObject enemy, GameObject hero)
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

public class Sword : Weapon, ICloseCombatWeapon
{
    // damageType attackType = damageType.Slashing;

    public Sword()
    {
        attackDamage = 10;
        attackCooldown = 1;
    }

    public override void Attack(GameObject enemy, GameObject hero)
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

public class Mace : Weapon, IMeleeWeapon
{
    // damageType attackType = damageType.Bludgeoning;

    public Mace()
    {
        attackDamage = 10;
        attackCooldown = 1;
    }

    public override void Attack(GameObject enemy, GameObject hero)
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

public class CrossBow : Weapon, ILongRangeWeapon
{
    // damageType attackType = damageType.Piercing;

    public CrossBow()
    {
        attackDamage = 20;
        attackCooldown = 2;
    }

    public override void Attack(GameObject enemy, GameObject hero)
    {
        Debug.Log("Attack with CrossBow");
        enemyStatus = enemy.GetComponent<EnemyStatus>();
        enemyStatus.UpdateHealth(enemyStatus.health - (int)(GetModifier() * attackDamage));
    }
    public override double GetModifier()
    {
        return (int)enemyStatus.armor switch
        {
            0 => 0.5,
            1 => 3,
            2 => 1,
            _ => 1,
        };
    }
}

public class SpellBook : Weapon, IMagicWeapon
{
    // damageType attackType = damageType.Magic;

    public SpellBook()
    {
        attackDamage = 0;
        attackCooldown = 1;
    }

    public override void Attack(GameObject enemy, GameObject hero)
    {
        Debug.Log("Attack with SpellBook");
        enemyStatus = enemy.GetComponent<EnemyStatus>();
        enemyStatus.UpdateHealth(enemyStatus.health - (int)(GetModifier() * attackDamage));
    }
    public override double GetModifier()
    {
        return 1;
    }
}

public class Buckler : Weapon, IShieldWeapon
{
    // damageType attackType = damageType.True;
    readonly GameObject hero;
    public Buckler()
    {
        // this.attackDamage = 0;
        hero = GameObject.FindWithTag("Player");
        attackCooldown = 1;
    }

    public override void Attack(GameObject enemy, GameObject hero)
    {
        Debug.Log("Block with Buckler");
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

public class Rapier : Weapon, ICloseCombatWeapon
{
    // damageType attackType = damageType.Piercing;

    public Rapier()
    {
        attackDamage = 10;
        attackCooldown = 1;
    }

    public override void Attack(GameObject enemy, GameObject hero)
    {
        Debug.Log("Attack with Rapier");
        enemyStatus = enemy.GetComponent<EnemyStatus>();
        enemyStatus.UpdateHealth(enemyStatus.health - (int)(GetModifier() * attackDamage));
    }

    public override double GetModifier()
    {
        return (int)enemyStatus.armor switch
        {
            0 => 0.5,
            1 => 2,
            2 => 1,
            _ => 1,
        };
    }
}

public class GreatSword : Weapon, ICloseCombatWeapon
{
    // damageType attackType = damageType.Piercing;

    public GreatSword()
    {
        attackDamage = 10;
        attackCooldown = 1;
    }

    public override void Attack(GameObject enemy, GameObject hero)
    {
        Debug.Log("Attack with GreatSword");
        enemyStatus = enemy.GetComponent<EnemyStatus>();
        enemyStatus.UpdateHealth(enemyStatus.health - (int)(GetModifier() * attackDamage));
    }

    public override double GetModifier()
    {
        return (int)enemyStatus.armor switch
        {
            0 => 0.5,
            1 => 2,
            2 => 1,
            _ => 1,
        };
    }
}

public class Daggers : Weapon, IMeleeWeapon
{
    // damageType attackType = damageType.Bludgeoning;

    public Daggers()
    {
        attackDamage = 5;
        attackCooldown = 1;
    }

    public override void Attack(GameObject enemy, GameObject hero)
    {
        Debug.Log("Attack with Daggers");
        enemyStatus = enemy.GetComponent<EnemyStatus>();
        enemyStatus.UpdateHealth(enemyStatus.health - (int)(GetModifier() * attackDamage));
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

public class Net : Weapon, IMeleeWeapon
{
    // damageType attackType = damageType.Bludgeoning;

    public Net()
    {
        attackDamage = 0;
        attackCooldown = 2;
    }

    public override void Attack(GameObject enemy, GameObject hero)
    {
        Debug.Log("Attack with Daggers");
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