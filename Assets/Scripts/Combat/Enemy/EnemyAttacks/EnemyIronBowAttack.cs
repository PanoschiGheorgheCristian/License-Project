using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIronBowAttack : GenericEnemyAttack
{
    int heroCurrentPosition;
    // Update is called once per frame
    void Update()
    {
        //Check for Elite Enemy / Boss by looking at EnemyToFight.isElite / EnemyToFight.isBoss
        heroCurrentPosition = hero.GetComponent<PlayerController>().heroCurrentPosition;
        if (isAttacking == 0 && isExhausted == 0 && !EnemyStatus.isStunned && isInLoadingPeriod == false)
            if (hero.GetComponent<HeroStatus>().alive == 1)
            {
                if (EnemyToFight.isElite)
                    ProcessEliteEnemy(heroCurrentPosition);
                else
                    ProcessNormalEnemy(heroCurrentPosition);
            }
    }

    private void ProcessEliteEnemy(int heroPosition)
    {
        if (heroPosition % 5 == 4 || heroPosition % 5 == 3)
        {
            WideSpreadAttack(heroPosition);
        }
        else
            ImprovedIronArrow(heroPosition);
    }

    private void ProcessNormalEnemy(int heroPosition)
    {
        if (heroPosition % 5 == 4 || heroPosition % 5 == 3)
        {
            SpreadAttack(heroPosition);
        }
        else
            IronArrow(heroPosition);
    }

    void SpreadAttack(int heroPosition)
    {
        isAttacking = 1;
        Attack(heroPosition, 0.7f, 10);
        if(heroPosition % 5 != 0)
        {
            Attack(heroPosition - 1, 0.7f, 10);
            if(heroPosition > 4)
                Attack(heroPosition - 6, 0.7f, 10);
            if(heroPosition < 10)
                Attack(heroPosition + 4, 0.7f, 10);
        }

        StartCoroutine(LoseExhausted(0.7f + timeExhausted));
    }

    void WideSpreadAttack(int heroPosition)
    {
        isAttacking = 1;
        Attack(heroPosition, 0.7f, 10);
        if (heroPosition % 5 != 0)
        {
            Attack(heroPosition - 1, 0.7f, 10);
            if (heroPosition > 4)
                Attack(heroPosition - 6, 0.7f, 10);
            if (heroPosition < 10)
                Attack(heroPosition + 4, 0.7f, 10);
            if(heroPosition % 5 != 1)
                {
                    Attack(heroPosition - 2, 0.7f, 10);
                    if (heroPosition > 4)
                        Attack(heroPosition - 7, 0.7f, 10);
                    if (heroPosition < 10)
                        Attack(heroPosition + 3, 0.7f, 10);
                }
        }

        StartCoroutine(LoseExhausted(0.7f + timeExhausted));
    }

    void IronArrow(int heroCurrentPosition)
    {
        isAttacking = 1;
        List<int> attackPositions = new();

        for (int i = heroCurrentPosition - heroCurrentPosition % 5; i < heroCurrentPosition - heroCurrentPosition % 5 + 5; i++)
        {
            attackPositions.Add(i);
        }

        Attack(attackPositions, 1.2f, 30);

        StartCoroutine(LoseExhausted(1.2f + timeExhausted));
    }

    void ImprovedIronArrow(int heroCurrentPosition)
    {
        isAttacking = 1;
        List<int> attackPositions = new();

        for (int i = heroCurrentPosition - heroCurrentPosition % 5; i < heroCurrentPosition - heroCurrentPosition % 5 + 5; i++)
        {
            attackPositions.Add(i);
        }
        if(heroCurrentPosition > 4)
            attackPositions.Add(heroCurrentPosition - 5);
        if(heroCurrentPosition < 10)
            attackPositions.Add(heroCurrentPosition + 5);

        Attack(attackPositions, 1.2f, 30);

        StartCoroutine(LoseExhausted(1.2f + timeExhausted));
    }
}
