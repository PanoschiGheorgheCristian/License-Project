using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemyAttack : GenericEnemyAttack
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
        StartCoroutine(FastStunStrike(heroPosition));
    }

    private void ProcessNormalEnemy(int heroPosition)
    {
        StartCoroutine(StunStrike(heroPosition));
    }

    IEnumerator StunStrike(int heroPosition)
    {
        isAttacking = 1;
        Attack(heroPosition, 1f, 15);

        yield return new WaitForSeconds(1.1f);
        
        StartCoroutine(LoseExhausted(timeExhausted));
    }

    IEnumerator FastStunStrike(int heroPosition)
    {
        isAttacking = 1;
        int playerHealth = hero.GetComponent<HeroStatus>().health;
        Attack(heroPosition, 0.5f, 15);

        yield return new WaitForSeconds(0.6f);

        if (playerHealth != hero.GetComponent<HeroStatus>().health)
        {
            HeroStatus.isStunned = true;
        }

        StartCoroutine(LoseExhausted(timeExhausted));
    }
}
