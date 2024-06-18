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
        if (isAttacking == 0 && isExhausted == 0 && !EnemyStatus.isStunned)
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
        int heroHealth = hero.GetComponent<HeroStatus>().health;

        for (int iterator = heroCurrentPosition; iterator % 5 < 4; iterator++)
        {
            Attack(iterator, 1f, 15);
        }
        Attack(heroPosition / 5 * 5 + 4, 1f, 15);

        yield return new WaitForSeconds(1f);

        if (heroHealth != hero.GetComponent<HeroStatus>().health)
            Poison();
    }

    IEnumerator ThrowThreeDagger(int heroPosition)
    {
        int heroHealth = hero.GetComponent<HeroStatus>().health;

        for (int iterator = heroCurrentPosition; iterator % 5 < 4; iterator++)
        {
            Attack(iterator, 1f, 15);
        }
        Attack(heroPosition / 5 * 5 + 4, 1f, 15);

        yield return new WaitForSeconds(1f);

        if (heroHealth != hero.GetComponent<HeroStatus>().health)
        {
            Poison();
            Bleed();
        }

        for (int iterator = heroCurrentPosition; iterator % 5 < 4; iterator++)
        {
            Attack(iterator, 1f, 15);
        }
        Attack(heroPosition / 5 * 5 + 4, 1f, 15);

        yield return new WaitForSeconds(1f);

        if (heroHealth != hero.GetComponent<HeroStatus>().health)
        {
            Poison();
            Bleed();
        }

        for (int iterator = heroCurrentPosition; iterator % 5 < 4; iterator++)
        {
            Attack(iterator, 1f, 15);
        }
        Attack(heroPosition / 5 * 5 + 4, 1f, 15);

        yield return new WaitForSeconds(1f);

        if (heroHealth != hero.GetComponent<HeroStatus>().health)
        {
            Poison();
            Bleed();
        }
    }

    void Poison()
    {
        HeroStatus.isPoisoned = true;
        isExhausted = 1;

        StartCoroutine(LoseExhausted(1f));
    }

    void Bleed()
    {
        HeroStatus.isBleeding = true;
    }
}
