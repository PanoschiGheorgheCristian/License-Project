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
        enemyStatus.UpdateHealth(enemyStatus.health - ((int)(GetModifier() * (attackDamage + attackDamage * ((HeroStatus.isBuffed ? 30 : 0) + (HeroStatus.isDebuffed ? -30 : 0)) / 100))));
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

public class IronBow : Weapon, ILongRangeWeapon
{
    // damageType attackType = damageType.Piercing;

    public IronBow()
    {
        attackDamage = 20;
        attackCooldown = 2.5;
    }

    public override void Attack(GameObject enemy, GameObject hero)
    {
        Debug.Log("Attack with IronBow");
        enemyStatus = enemy.GetComponent<EnemyStatus>();
        enemyStatus.UpdateHealth(enemyStatus.health - ((int)(GetModifier() * (attackDamage + attackDamage * ((HeroStatus.isBuffed ? 30 : 0) + (HeroStatus.isDebuffed ? -30 : 0)) / 100))));
        EnemyStatus.isBleeding = true;
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

public class ThrowingKnives : Weapon, ILongRangeWeapon
{
    // damageType attackType = damageType.Piercing;

    public ThrowingKnives()
    {
        attackDamage = 5;
        attackCooldown = 0.2;
    }

    public override void Attack(GameObject enemy, GameObject hero)
    {
        Debug.Log("Attack with Throwing Knives");
        enemyStatus = enemy.GetComponent<EnemyStatus>();
        enemyStatus.UpdateHealth(enemyStatus.health - ((int)(GetModifier() * (attackDamage + attackDamage * ((HeroStatus.isBuffed ? 30 : 0) + (HeroStatus.isDebuffed ? -30 : 0)) / 100))));
        EnemyStatus.isPoisoned = true;
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
        enemyStatus.UpdateHealth(enemyStatus.health - ((int)(GetModifier() * (attackDamage + attackDamage * ((HeroStatus.isBuffed ? 30 : 0) + (HeroStatus.isDebuffed ? -30 : 0)) / 100))));
    }
    public override double GetModifier()
    {
        return 1;
    }
}

public class Sceptre : Weapon, IMagicWeapon
{
    // damageType attackType = damageType.Magic;

    public Sceptre()
    {
        attackDamage = 0;
        attackCooldown = 1;
    }

    public override void Attack(GameObject enemy, GameObject hero)
    {
        Debug.Log("Debuffed your enemy with Sceptre");
       EnemyStatus.isDebuffed = true;
    }
    public override double GetModifier()
    {
        return 1;
    }
}

public class Circlet : Weapon, IMagicWeapon
{
    // damageType attackType = damageType.Magic;

    public Circlet()
    {
        attackDamage = 0;
        attackCooldown = 1;
    }

    public override void Attack(GameObject enemy, GameObject hero)
    {
        Debug.Log("Buffed yourself with Circlet");
        HeroStatus.isBuffed = true;
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
    }

    public override double GetModifier()
    {
        return 1;
    }
}

public class SpikedShield : Weapon, IShieldWeapon
{
    // damageType attackType = damageType.True;
    readonly GameObject hero;
    public SpikedShield()
    {
        attackDamage = 10;
        hero = GameObject.FindWithTag("Player");
        attackCooldown = 2;
    }

    public override void Attack(GameObject enemy, GameObject hero)
    {
        Debug.Log("Block with Spiked Shield");
        int shieldCharges = hero.GetComponent<HeroStatus>().shieldCharges;
        shieldCharges = shieldCharges == 2 ? 2 : shieldCharges + 1;
        hero.GetComponent<HeroStatus>().shieldCharges = shieldCharges;
        Debug.Log(hero.GetComponent<HeroStatus>().shieldCharges);

        enemyStatus = enemy.GetComponent<EnemyStatus>();
        enemyStatus.UpdateHealth(enemyStatus.health - ((int)(GetModifier() * (attackDamage + attackDamage * ((HeroStatus.isBuffed ? 30 : 0) + (HeroStatus.isDebuffed ? -30 : 0)) / 100))));
    }

    public override double GetModifier()
    {
        return 1;
    }
}

public class MonkeyEarrings : Weapon, IShieldWeapon
{
    // damageType attackType = damageType.True;
    readonly GameObject hero;
    public MonkeyEarrings()
    {
        hero = GameObject.FindWithTag("Player");
        attackCooldown = 4;
    }

    public override void Attack(GameObject enemy, GameObject hero)
    {
        Debug.Log("Block with Monkey Earrings");
        int shieldCharges = hero.GetComponent<HeroStatus>().shieldCharges;
        shieldCharges = shieldCharges == 2 ? 2 : shieldCharges + 1;
        hero.GetComponent<HeroStatus>().shieldCharges = shieldCharges;
        Debug.Log(hero.GetComponent<HeroStatus>().shieldCharges);
        EnemyStatus.isStunned = true;
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
        enemyStatus.UpdateHealth(enemyStatus.health - ((int)(GetModifier() * (attackDamage + attackDamage * ((HeroStatus.isBuffed ? 30 : 0) + (HeroStatus.isDebuffed ? -30 : 0)) / 100))));
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

public class Waraxe : Weapon, ICloseCombatWeapon
{
    // damageType attackType = damageType.Piercing;

    public Waraxe()
    {
        attackDamage = 10;
        attackCooldown = 1;
    }

    public override void Attack(GameObject enemy, GameObject hero)
    {
        Debug.Log("Attack with WarAxe");
        enemyStatus = enemy.GetComponent<EnemyStatus>();
        enemyStatus.UpdateHealth(enemyStatus.health - ((int)(GetModifier() * (attackDamage + attackDamage * ((HeroStatus.isBuffed ? 30 : 0) + (HeroStatus.isDebuffed ? -30 : 0)) / 100))));
        EnemyStatus.isBleeding = true;
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

public class Spear : Weapon, ICloseCombatWeapon
{
    // damageType attackType = damageType.Piercing;

    public Spear()
    {
        attackDamage = 10;
        attackCooldown = 1;
    }

    public override void Attack(GameObject enemy, GameObject hero)
    {
        Debug.Log("Attack with Spear");
        enemyStatus = enemy.GetComponent<EnemyStatus>();
        enemyStatus.UpdateHealth(enemyStatus.health - ((int)(GetModifier() * (attackDamage + attackDamage * ((HeroStatus.isBuffed ? 30 : 0) + (HeroStatus.isDebuffed ? -30 : 0)) / 100))));
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
        enemyStatus.UpdateHealth(enemyStatus.health - ((int)(GetModifier() * (attackDamage + attackDamage * ((HeroStatus.isBuffed ? 30 : 0) + (HeroStatus.isDebuffed ? -30 : 0)) / 100))));
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
        enemyStatus.UpdateHealth(enemyStatus.health - ((int)(GetModifier() * (attackDamage + attackDamage * ((HeroStatus.isBuffed ? 30 : 0) + (HeroStatus.isDebuffed ? -30 : 0)) / 100))));
        enemyStatus.UpdateHealth(enemyStatus.health - ((int)(GetModifier() * (attackDamage + attackDamage * ((HeroStatus.isBuffed ? 30 : 0) + (HeroStatus.isDebuffed ? -30 : 0)) / 100))));
        EnemyStatus.isPoisoned = true;
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

public class Flail : Weapon, IMeleeWeapon
{
    // damageType attackType = damageType.Bludgeoning;

    public Flail()
    {
        attackDamage = 10;
        attackCooldown = 3;
    }

    public override void Attack(GameObject enemy, GameObject hero)
    {
        Debug.Log("Attack with Flail");
        enemyStatus = enemy.GetComponent<EnemyStatus>();
        enemyStatus.UpdateHealth(enemyStatus.health - ((int)(GetModifier() * (attackDamage + attackDamage * ((HeroStatus.isBuffed ? 30 : 0) + (HeroStatus.isDebuffed ? -30 : 0)) / 100))));
        EnemyStatus.isStunned = true;
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