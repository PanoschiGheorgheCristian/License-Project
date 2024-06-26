using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpearAttack : GenericEnemyAttack
{
    int heroCurrentPosition;
    // Update is called once per frame
    void FixedUpdate()
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
        StartCoroutine(TriplePoke(heroPosition));
    }

    private void ProcessNormalEnemy(int heroPosition)
    {
        StartCoroutine(Poke(heroPosition));
    }

    IEnumerator Poke(int heroPosition)
    {
        isAttacking = 1;
        for (int iterator = heroCurrentPosition; iterator % 5 < 4; iterator++)
        {
            Attack(iterator, (5 - heroPosition % 5) * 0.3f, 15);
        }
        Attack(heroPosition / 5* 5 + 4, (5 - heroPosition % 5) * 0.3f, 15);

        yield return new WaitForSeconds(1.5f);

        StartCoroutine(LoseExhausted(timeExhausted));
    }

    IEnumerator TriplePoke(int heroPosition)
    {
        isAttacking = 1;
        for (int iterator = heroCurrentPosition; iterator % 5 < 4; iterator++)
        {
            Attack(iterator, (5 - heroPosition % 5) * 0.3f, 15);
        }
        Attack(heroPosition / 5 * 5 + 4, (5 - heroPosition % 5) * 0.3f, 15);

        yield return new WaitForSeconds(1.5f);

        for (int iterator = heroCurrentPosition; iterator % 5 < 4; iterator++)
        {
            Attack(iterator, (5 - heroPosition % 5) * 0.3f, 15);
        }
        Attack(heroPosition / 5 * 5 + 4, (5 - heroPosition % 5) * 0.3f, 15);

        yield return new WaitForSeconds(1.5f);

        for (int iterator = heroCurrentPosition; iterator % 5 < 4; iterator++)
        {
            Attack(iterator, (5 - heroPosition % 5) * 0.3f, 15);
        }
        Attack(heroPosition / 5 * 5 + 4, (5 - heroPosition % 5) * 0.3f, 15);

        yield return new WaitForSeconds(1.5f);

        StartCoroutine(LoseExhausted(timeExhausted));
    }
}
