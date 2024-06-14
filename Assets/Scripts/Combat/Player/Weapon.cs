using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public abstract class Weapon
{
    public int weaponExp;
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
    public abstract void UpdateToLevel2();
    public abstract void UpdateToLevel3();
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
        enemyStatus.UpdateHealth(enemyStatus.health - ((int)(GetModifier() * (attackDamage + 
            attackDamage * ((HeroStatus.isBuffed ? HeroStatus.buffPower : 0) + (HeroStatus.isDebuffed ? HeroStatus.debuffPower : 0)) / 100))));
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
    public override void UpdateToLevel2()
    {
        attackDamage = 15;
        attackCooldown = 1;
    }
    public override void UpdateToLevel3()
    {
        attackDamage = 15;
        attackCooldown = 0.5;
    }
}

public class IronBow : Weapon, ILongRangeWeapon, IBleedWeapon
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
        enemyStatus.UpdateHealth(enemyStatus.health - ((int)(GetModifier() * (attackDamage + 
            attackDamage * ((HeroStatus.isBuffed ? HeroStatus.buffPower : 0) + (HeroStatus.isDebuffed ? HeroStatus.debuffPower : 0)) / 100))));
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
    public override void UpdateToLevel2()
    {
        attackDamage = 20;
        attackCooldown = 2;
    }
    public override void UpdateToLevel3()
    {
        attackDamage = 30;
        attackCooldown = 2;
    }
}

public class ThrowingKnives : Weapon, ILongRangeWeapon, IBleedWeapon, IPoisonousWeapon
{
    // damageType attackType = damageType.Piercing;
    private bool isLevel3;
    public ThrowingKnives()
    {
        attackDamage = 5;
        attackCooldown = 1;
        isLevel3 = false;
    }

    public override void Attack(GameObject enemy, GameObject hero)
    {
        Debug.Log("Attack with Throwing Knives");
        enemyStatus = enemy.GetComponent<EnemyStatus>();
        enemyStatus.UpdateHealth(enemyStatus.health - ((int)(GetModifier() * (attackDamage + 
            attackDamage * ((HeroStatus.isBuffed ? HeroStatus.buffPower : 0) + (HeroStatus.isDebuffed ? HeroStatus.debuffPower : 0)) / 100))));
        EnemyStatus.isPoisoned = true;
        if(isLevel3)
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
    public override void UpdateToLevel2()
    {
        attackDamage = 10;
        attackCooldown = 1;
    }
    public override void UpdateToLevel3()
    {
        attackDamage = 10;
        attackCooldown = 0.5;
        isLevel3 = true;
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
        enemyStatus.UpdateHealth(enemyStatus.health - ((int)(GetModifier() * (attackDamage + 
            attackDamage * ((HeroStatus.isBuffed ? HeroStatus.buffPower : 0) + (HeroStatus.isDebuffed ? HeroStatus.debuffPower : 0)) / 100))));
    }
    public override double GetModifier()
    {
        return 1;
    }
    public override void UpdateToLevel2()
    {
        attackDamage = 15;
        attackCooldown = 1;
    }
    public override void UpdateToLevel3()
    {
        attackDamage = 20;
        attackCooldown = 1;
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
    public override void UpdateToLevel2()
    {
        EnemyStatus.debuffDuration = 7f;
    }
    public override void UpdateToLevel3()
    {
        HeroStatus.debuffPower = -50;
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
    public override void UpdateToLevel2()
    {
        HeroStatus.buffDuration = 7f;
    }
    public override void UpdateToLevel3()
    {
        HeroStatus.buffPower = 50;
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
        shieldCharges = shieldCharges == HeroShield.maxShield1Charges ? HeroShield.maxShield1Charges : shieldCharges + 1;
        hero.GetComponent<HeroStatus>().shieldCharges = shieldCharges;
        Debug.Log(hero.GetComponent<HeroStatus>().shieldCharges);
    }

    public override double GetModifier()
    {
        return 1;
    }
    public override void UpdateToLevel2()
    {
        HeroShield.maxShield1Charges = 3;
    }
    public override void UpdateToLevel3()
    {
        HeroShield.shield1Duration = 2.5f;
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
        shieldCharges = shieldCharges == HeroShield.maxShield2Charges ? HeroShield.maxShield2Charges : shieldCharges + 1;
        hero.GetComponent<HeroStatus>().shieldCharges = shieldCharges;
        Debug.Log(hero.GetComponent<HeroStatus>().shieldCharges);

        enemyStatus = enemy.GetComponent<EnemyStatus>();
        enemyStatus.UpdateHealth(enemyStatus.health - ((int)(GetModifier() * (attackDamage + 
            attackDamage * ((HeroStatus.isBuffed ? HeroStatus.buffPower : 0) + (HeroStatus.isDebuffed ? HeroStatus.debuffPower : 0)) / 100))));
    }

    public override double GetModifier()
    {
        return 1;
    }
    public override void UpdateToLevel2()
    {
        attackDamage = 15;
        HeroShield.maxShield2Charges = 3;
    }
    public override void UpdateToLevel3()
    {
        HeroShield.shield2Duration = 1f;
    }
}

public class MonkeyEarrings : Weapon, IShieldWeapon, IStunWeapon
{
    // damageType attackType = damageType.True;
    private float stunDuration = 1.5f;
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
        EnemyStatus.stunDuration = stunDuration;
        EnemyStatus.isStunned = true;
    }

    public override double GetModifier()
    {
        return 1;
    }
    public override void UpdateToLevel2()
    {
        stunDuration = 2f;
    }
    public override void UpdateToLevel3()
    {
        stunDuration = 2.5f;
    }
}

public class Sword : Weapon, ICloseCombatWeapon, IBleedWeapon
{
    // damageType attackType = damageType.Slashing;
    private bool isLevel3;
    public Sword()
    {
        attackDamage = 10;
        attackCooldown = 1;
        isLevel3 = false;
    }

    public override void Attack(GameObject enemy, GameObject hero)
    {
        Debug.Log("Attack with Sword");
        enemyStatus = enemy.GetComponent<EnemyStatus>();
        enemyStatus.UpdateHealth(enemyStatus.health - ((int)(GetModifier() * (attackDamage + 
            attackDamage * ((HeroStatus.isBuffed ? HeroStatus.buffPower : 0) + (HeroStatus.isDebuffed ? HeroStatus.debuffPower : 0)) / 100))));
        if (isLevel3)
            EnemyStatus.isBleeding = true;
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
    public override void UpdateToLevel2()
    {
        attackDamage = 15;
        attackCooldown = 1;
    }
    public override void UpdateToLevel3()
    {
        isLevel3 = true;
    }
}

public class Waraxe : Weapon, ICloseCombatWeapon, IBleedWeapon
{
    // damageType attackType = damageType.Piercing;

    public Waraxe()
    {
        attackDamage = 20;
        attackCooldown = 2;
    }

    public override void Attack(GameObject enemy, GameObject hero)
    {
        Debug.Log("Attack with WarAxe");
        enemyStatus = enemy.GetComponent<EnemyStatus>();
        enemyStatus.UpdateHealth(enemyStatus.health - ((int)(GetModifier() * (attackDamage + 
            attackDamage * ((HeroStatus.isBuffed ? HeroStatus.buffPower : 0) + (HeroStatus.isDebuffed ? HeroStatus.debuffPower : 0)) / 100))));
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
    public override void UpdateToLevel2()
    {
        attackDamage = 25;
        attackCooldown = 2;
    }
    public override void UpdateToLevel3()
    {
        attackDamage = 25;
        attackCooldown = 1;
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
        enemyStatus.UpdateHealth(enemyStatus.health - ((int)(GetModifier() * (attackDamage + 
            attackDamage * ((HeroStatus.isBuffed ? HeroStatus.buffPower : 0) + (HeroStatus.isDebuffed ? HeroStatus.debuffPower : 0)) / 100))));
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
    public override void UpdateToLevel2()
    {
        attackDamage = 15;
        attackCooldown = 1;
    }
    public override void UpdateToLevel3()
    {
        attackDamage = 25;
        attackCooldown = 1;
    }
}

public class Mace : Weapon, IMeleeWeapon, IStunWeapon
{
    // damageType attackType = damageType.Bludgeoning;
    private bool isLevel3;
    public Mace()
    {
        attackDamage = 15;
        attackCooldown = 1.5;
        isLevel3 = false;
    }

    public override void Attack(GameObject enemy, GameObject hero)
    {
        Debug.Log("Attack with Mace");
        enemyStatus = enemy.GetComponent<EnemyStatus>();
        enemyStatus.UpdateHealth(enemyStatus.health - ((int)(GetModifier() * (attackDamage + 
            attackDamage * ((HeroStatus.isBuffed ? HeroStatus.buffPower : 0) + (HeroStatus.isDebuffed ? HeroStatus.debuffPower : 0)) / 100))));
        if (isLevel3)
        {
            EnemyStatus.stunDuration = 1f;
            EnemyStatus.isStunned = true;
        }
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
    public override void UpdateToLevel2()
    {
        attackDamage = 20;
        attackCooldown = 1.5;
    }
    public override void UpdateToLevel3()
    {
        attackDamage = 25;
        attackCooldown = 3;
        isLevel3 = true;
    }
}

public class Daggers : Weapon, IMeleeWeapon, IPoisonousWeapon
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
        enemyStatus.UpdateHealth(enemyStatus.health - ((int)(GetModifier() * (attackDamage + 
            attackDamage * ((HeroStatus.isBuffed ? HeroStatus.buffPower : 0) + (HeroStatus.isDebuffed ? HeroStatus.debuffPower : 0)) / 100))));
        enemyStatus.UpdateHealth(enemyStatus.health - ((int)(GetModifier() * (attackDamage + 
            attackDamage * ((HeroStatus.isBuffed ? HeroStatus.buffPower : 0) + (HeroStatus.isDebuffed ? HeroStatus.debuffPower : 0)) / 100))));
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
    public override void UpdateToLevel2()
    {
        attackDamage = 7;
        attackCooldown = 1;
    }
    public override void UpdateToLevel3()
    {
        attackDamage = 15;
        attackCooldown = 1;
    }
}

public class Flail : Weapon, IMeleeWeapon, IStunWeapon
{
    // damageType attackType = damageType.Bludgeoning;
    private float stunDuration = 1.5f;
    public Flail()
    {
        attackDamage = 10;
        attackCooldown = 3;
    }

    public override void Attack(GameObject enemy, GameObject hero)
    {
        Debug.Log("Attack with Flail");
        enemyStatus = enemy.GetComponent<EnemyStatus>();
        enemyStatus.UpdateHealth(enemyStatus.health - ((int)(GetModifier() * (attackDamage + 
            attackDamage * ((HeroStatus.isBuffed ? HeroStatus.buffPower : 0) + (HeroStatus.isDebuffed ? HeroStatus.debuffPower : 0)) / 100))));
        EnemyStatus.stunDuration = stunDuration;
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
    public override void UpdateToLevel2()
    {
        attackDamage = 20;
        attackCooldown = 3;
    }
    public override void UpdateToLevel3()
    {
        attackDamage = 25;
        attackCooldown = 3;
        stunDuration = 2f;
    }
}