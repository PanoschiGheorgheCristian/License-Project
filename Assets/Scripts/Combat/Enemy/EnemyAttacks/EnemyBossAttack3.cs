using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossAttack3 : GenericEnemyAttack
{
    float improvedAttackSpeed;
    int improvedAttackDamage;
    int improvedAttackPosition;
    int heroCurrentPosition;
    private void Awake() {
        FindMostPowerfulEquipedWeaopn();
    }
    // Update is called once per frame
    void Update()
    {
        //Check for Elite Enemy / Boss by looking at EnemyToFight.isElite / EnemyToFight.isBoss
        heroCurrentPosition = hero.GetComponent<PlayerController>().heroCurrentPosition;
        if (isAttacking == 0 && isExhausted == 0 && !EnemyStatus.isStunned && isInLoadingPeriod == false)
            if (hero.GetComponent<HeroStatus>().alive == 1)
                ProcessAttacks(heroCurrentPosition);
    }

    private void ProcessAttacks(int heroPosition)
    {
        StartCoroutine(AttackColumn(heroPosition));
    }

    void FindMostPowerfulEquipedWeaopn()
    {
        int[] weaponLevels = PlayerWeapons.GetAllWeaponsLevels();
        int[] equipedWeapons = PlayerWeapons.GetWeaponsIndexes();
        int max = 0;
        for (int i = 0; i < 5; i++)
        {
            if(weaponLevels[equipedWeapons[i]] > max)
                max = weaponLevels[equipedWeapons[i]];
        }
        for (int i = 0; i < 5; i++)
        {
            if (weaponLevels[equipedWeapons[i]] == max)
            {
                improvedAttackPosition = i;
                improvedAttackDamage = 15;
                improvedAttackSpeed = 0.5f;
            }
        }
    }

    IEnumerator AttackColumn(int heroPosition)
    {
        isAttacking = 1;
        int column = heroPosition % 5;
        if(column == improvedAttackPosition)
        {
            Attack(column, 1f - improvedAttackSpeed, 15 + improvedAttackDamage);
            Attack(column + 5, 1f - improvedAttackSpeed, 15 + improvedAttackDamage);
            Attack(column + 10, 1f - improvedAttackSpeed, 15 + improvedAttackDamage);

            yield return new WaitForSeconds(1f - improvedAttackSpeed);

        }
        else
        {
            Attack(column, 1f, 15 + improvedAttackDamage);
            Attack(column + 5, 1f, 15 + improvedAttackDamage);
            Attack(column + 10, 1f, 15 + improvedAttackDamage);

            yield return new WaitForSeconds(1f);
        }

        StartCoroutine(LoseExhausted(timeExhausted));
    }
}
