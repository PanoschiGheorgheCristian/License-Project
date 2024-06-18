using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordAttack : GenericEnemyAttack
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
        if (heroPosition % 5 == 4 || heroPosition % 5 == 3)
        {
            StartCoroutine(TripleSwipe(1.7f, 1.7f, heroCurrentPosition));
        }
        else
        {
            StartCoroutine(ReverseTripleSwipe(1.7f, 1.7f, heroCurrentPosition));
        }
    }

    private void ProcessNormalEnemy(int heroPosition)
    {
        if (heroPosition % 5 == 4 || heroPosition % 5 == 3)
        {
            SimpleSwipe(heroPosition, 0);
        }
        else
            SimpleSwipe(heroPosition, 1);
    }

    void SwipeUp(int heroPosition)
    {
        Attack(heroPosition, 1.5f, 35);
        if (heroPosition < 10)
            Attack(heroPosition + 5, 1.5f, 35);
        if (heroPosition > 4)
            Attack(heroPosition - 5, 1.5f, 35);
    }

    void SwipeLeft(int heroPosition)
    {
        Attack(heroPosition, 1.5f, 35);
        if (heroPosition % 5 != 0)
            Attack(heroPosition - 1, 1.5f, 35);
        if (heroPosition % 5 != 4)
            Attack(heroPosition + 1, 1.5f, 35);
    }

    void SimpleSwipe(int heroPosition, int nr)
    {
        if (nr < 1)
            SwipeLeft(heroPosition);
        else
            SwipeUp(heroPosition);


        StartCoroutine(LoseExhausted(1.5f + timeExhausted));
    }

    private IEnumerator TripleSwipe(float time1, float time2, int heroCurrentPosition)
    {
        SwipeLeft(heroCurrentPosition);

        yield return new WaitForSeconds(time1);

        heroCurrentPosition = hero.GetComponent<PlayerController>().heroCurrentPosition;
        SwipeUp(heroCurrentPosition);

        yield return new WaitForSeconds(time2);

        heroCurrentPosition = hero.GetComponent<PlayerController>().heroCurrentPosition;
        SwipeLeft(heroCurrentPosition);

        StartCoroutine(LoseExhausted(timeExhausted + 1.5f));
    }

    private IEnumerator ReverseTripleSwipe(float time1, float time2, int heroCurrentPosition)
    {
        SwipeUp(heroCurrentPosition);

        yield return new WaitForSeconds(time1);

        heroCurrentPosition = hero.GetComponent<PlayerController>().heroCurrentPosition;
        SwipeLeft(heroCurrentPosition);

        yield return new WaitForSeconds(time2);

        heroCurrentPosition = hero.GetComponent<PlayerController>().heroCurrentPosition;
        SwipeUp(heroCurrentPosition);

        StartCoroutine(LoseExhausted(timeExhausted + 1.5f));
    }
}
