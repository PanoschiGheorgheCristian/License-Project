using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircletAttack : GenericEnemyAttack
{
    int heroCurrentPosition;

    private void Awake() {
        if (EnemyToFight.isElite)
            EnemyStatus.buffPower = 50;
    }
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
        if (!EnemyStatus.isBuffed)
            BuffSelf();
        else
            StartCoroutine(EliteConcentratedFire(heroPosition));
    }

    private void ProcessNormalEnemy(int heroPosition)
    {
        if(!EnemyStatus.isBuffed)
            BuffSelf();
        else
            StartCoroutine(ConcentratedFire(heroPosition));
    }

    void BuffSelf()
    {
        EnemyStatus.isBuffed = true;
        isExhausted = 1;

        StartCoroutine(LoseExhausted(1f));
    }

    IEnumerator ConcentratedFire(int heroPosition)
    {
        Attack(heroPosition, 1f, 5);

        yield return new WaitForSeconds(1.2f);

        for(int i = 0; i < 5; i++)
        {
            Attack(heroPosition, 0.2f, 5);
            yield return new WaitForSeconds(0.2f);
        }

        Attack(heroPosition, 0.4f, 5);

        StartCoroutine(LoseExhausted(0.4f + timeExhausted));
    }

    IEnumerator EliteConcentratedFire(int heroPosition)
    {
        List<int> attackPositions = new()
        {
            heroPosition
        };

        if (heroCurrentPosition > 4)
            attackPositions.Add(heroCurrentPosition - 5);
        if (heroCurrentPosition % 5 != 0)
            attackPositions.Add(heroCurrentPosition - 1);
        if (heroCurrentPosition < 10)
            attackPositions.Add(heroCurrentPosition + 5);
        if (heroCurrentPosition % 5 != 4)
            attackPositions.Add(heroCurrentPosition + 1);

        Attack(attackPositions, 1f, 5);

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 5; i++)
        {   
            Attack(attackPositions, 0.2f, 5);
            yield return new WaitForSeconds(0.2f);
        }

        Attack(attackPositions, 0.4f, 5);

        StartCoroutine(LoseExhausted(0.4f + timeExhausted));
    }
}
