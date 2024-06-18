using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossAttack2 : GenericEnemyAttack
{
    EnemyStatus enemyStatus;
    bool isHealing = false;
    int heroCurrentPosition;
    private void Awake() {
        enemyStatus = gameObject.GetComponent<EnemyStatus>();
    }
    // Update is called once per frame
    void Update()
    {
        //Check for Elite Enemy / Boss by looking at EnemyToFight.isElite / EnemyToFight.isBoss
        heroCurrentPosition = hero.GetComponent<PlayerController>().heroCurrentPosition;
        if (isAttacking == 0 && isExhausted == 0 && !EnemyStatus.isStunned)
            if (hero.GetComponent<HeroStatus>().alive == 1)
            {
                ProcessAttacks(heroCurrentPosition);
            }
        if(!isHealing)
        {
            enemyStatus.UpdateHealth(enemyStatus.health + 10);
            isHealing = true;
            StartCoroutine(BecomeAbleToHeal());
        }
    }

    private IEnumerator BecomeAbleToHeal()
    {
        yield return new WaitForSeconds(5f);
        
        isHealing = false;
    }

    void ProcessAttacks(int heroPosition)
    {
        StartCoroutine(ChainAttack(heroPosition));
    }

    void SwipeHorizontal(int heroPosition)
    {
        Attack(heroPosition, 0.8f, 15);
        if(heroPosition % 5 != 4)
        {
            Attack(heroPosition + 1, 0.8f, 15);
            if (heroPosition % 5 != 3)
                Attack(heroPosition + 2, 0.8f, 15);
        }
        if (heroPosition % 5 != 0)
        {
            Attack(heroPosition - 1, 0.8f, 15);
            if (heroPosition % 5 != 1)
                Attack(heroPosition - 2, 0.8f, 15);
        }
    }

    void SwipeVertical(int heroPosition)
    {
        Attack(heroPosition, 0.8f, 15);
        if (heroPosition / 5 == 0)
        {
            Attack(heroPosition + 5, 0.8f, 15);
            Attack(heroPosition + 10, 0.8f, 15);
        }
        else if(heroPosition / 5 == 1)
        {
            Attack(heroPosition + 5, 0.8f, 15);
            Attack(heroPosition - 5, 0.8f, 15);
        }
        else
        {
            Attack(heroPosition - 5, 0.8f, 15);
            Attack(heroPosition - 10, 0.8f, 15);
        }
    }

    void DonutShape(int heroPosition)
    {
        if(heroPosition % 5 != 4)
        {
            Attack(heroPosition + 1, 1f, 20);
            if(heroPosition / 5 != 2)
            {
                Attack(heroPosition + 6, 1f, 20);
                Attack(heroPosition + 5, 1f, 20);
                if (heroPosition % 5 != 0)
                {
                    Attack(heroPosition + 4, 1f, 20);
                    Attack(heroPosition - 1, 1f, 20);
                    if(heroPosition / 5 != 0)
                    {
                        Attack(heroPosition - 4, 1f, 20);
                        Attack(heroPosition - 5, 1f, 20);
                        Attack(heroPosition - 6, 1f, 20);
                    }
                }
                else
                {
                    if (heroPosition / 5 != 0)
                    {
                        Attack(heroPosition - 4, 1f, 20);
                        Attack(heroPosition - 5, 1f, 20);
                    }
                }
            }
            else
            {
                Attack(heroPosition - 4, 1f, 20);
                Attack(heroPosition - 5, 1f, 20);
                if (heroPosition / 5 != 0)
                {
                    Attack(heroPosition - 6, 1f, 20);
                    Attack(heroPosition - 1, 1f, 20);
                }
            }
        }
        else
        {
            Attack(heroPosition - 1, 1f, 20);
            if (heroPosition / 5 != 2)
            {
                Attack(heroPosition + 4, 1f, 20);
                Attack(heroPosition + 5, 1f, 20);
                if (heroPosition / 5 != 0)
                {
                    Attack(heroPosition - 5, 1f, 20);
                    Attack(heroPosition - 6, 1f, 20);
                }
            }
            else
            {
                Attack(heroPosition - 5, 1f, 20);
                Attack(heroPosition - 6, 1f, 20);
            }
        }
    }

    IEnumerator ChainAttack(int heroPosition)
    {
        SwipeHorizontal(heroPosition);

        yield return new WaitForSeconds(0.8f);

        heroPosition = hero.GetComponent<PlayerController>().heroCurrentPosition;
        SwipeVertical(heroPosition);

        yield return new WaitForSeconds(0.8f);

        heroPosition = hero.GetComponent<PlayerController>().heroCurrentPosition;
        DonutShape(heroPosition);

        yield return new WaitForSeconds(1f);

        StartCoroutine(LoseExhausted(timeExhausted));
    }
}
