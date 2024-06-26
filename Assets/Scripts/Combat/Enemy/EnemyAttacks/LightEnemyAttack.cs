using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEnemyAttack : GenericEnemyAttack
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
        StartCoroutine(ThrowThreeDagger(heroPosition));
    }

    private void ProcessNormalEnemy(int heroPosition)
    {
        StartCoroutine(ThrowDagger(heroPosition));
    }

    IEnumerator ThrowDagger(int heroPosition)
    {
        isAttacking = 1;
        int heroHealth = hero.GetComponent<HeroStatus>().health;

        if (heroPosition % 5 == 0)
        {
            Attack(heroPosition, 1f, 15);
            heroPosition++;
        }
        for (int iterator = heroPosition; iterator % 5 > 0; iterator++)
        {
            Attack(iterator, 1f, 15);
        }

        yield return new WaitForSeconds(1f);

        if (heroHealth != hero.GetComponent<HeroStatus>().health)
            Poison();

        StartCoroutine(LoseExhausted(timeExhausted));
    }

    IEnumerator ThrowThreeDagger(int heroPosition)
    {
        isAttacking = 1;
        int heroHealth = hero.GetComponent<HeroStatus>().health;

        if (heroPosition % 5 == 0)
        {
            Attack(heroPosition, 1f, 15);
            heroPosition++;
        }
        for (int iterator = heroPosition; iterator % 5 > 0; iterator++)
        {
            Attack(iterator, 1f, 15);
        }

        yield return new WaitForSeconds(1.25f);

        if (heroHealth != hero.GetComponent<HeroStatus>().health)
        {
            Poison();
            Bleed();
        }

        heroHealth = hero.GetComponent<HeroStatus>().health;
        heroPosition = hero.GetComponent<PlayerController>().heroCurrentPosition;
        if (heroPosition % 5 == 0)
        {
            Attack(heroPosition, 1f, 15);
            heroPosition++;
        }
        for (int iterator = heroPosition; iterator % 5 > 0; iterator++)
        {
            Attack(iterator, 1f, 15);
        }

        yield return new WaitForSeconds(1.25f);

        if (heroHealth != hero.GetComponent<HeroStatus>().health)
        {
            Poison();
            Bleed();
        }

        heroHealth = hero.GetComponent<HeroStatus>().health;
        heroPosition = hero.GetComponent<PlayerController>().heroCurrentPosition;
        if (heroPosition % 5 == 0)
        {
            Attack(heroPosition, 1f, 15);
            heroPosition++;
        }
        for (int iterator = heroPosition; iterator % 5 > 0; iterator++)
        {
            Attack(iterator, 1f, 15);
        }

        yield return new WaitForSeconds(1.25f);

        if (heroHealth != hero.GetComponent<HeroStatus>().health)
        {
            Poison();
            Bleed();
        }

        StartCoroutine(LoseExhausted(timeExhausted));
    }

    void Poison()
    {
        HeroStatus.isPoisoned = true;
    }

    void Bleed()
    {
        HeroStatus.isBleeding = true;
    }
}
