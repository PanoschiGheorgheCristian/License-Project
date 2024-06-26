using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossAttack : GenericEnemyAttack
{
    int indicator = 0;
    int heroCurrentPosition;
    // Update is called once per frame
    void Update()
    {
        //Check for Elite Enemy / Boss by looking at EnemyToFight.isElite / EnemyToFight.isBoss
        heroCurrentPosition = hero.GetComponent<PlayerController>().heroCurrentPosition;
        if (isAttacking == 0 && isExhausted == 0 && !EnemyStatus.isStunned && isInLoadingPeriod == false)
            if (hero.GetComponent<HeroStatus>().alive == 1)
            {
                ProcessAttacks(heroCurrentPosition);
            }
    }

    private void ProcessAttacks(int heroPosition)
    {
        if(indicator < 2)
        {
            StartCoroutine(PreciseAttack(heroPosition));
            indicator ++;
        }
        else
        {
            StartCoroutine(BoardWipe());
            indicator = 0;
        }
    }

    IEnumerator PreciseAttack(int heroPosition)
    {
        isAttacking = 1;
        Attack(heroPosition, 0.5f, 20);

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(LoseExhausted(timeExhausted));
    }

    IEnumerator BoardWipe()
    {
        isAttacking = 1;
        int rand = Random.Range(0, 15);
        while(rand == 15)
            rand = Random.Range(0, 15);
        for (int i = 0; i < 15; i++)
        {
            if(i != rand)
                Attack(i, 2.5f, 30);
        }

        yield return new WaitForSeconds(2.5f);

        StartCoroutine(LoseExhausted(timeExhausted));
    }
}
