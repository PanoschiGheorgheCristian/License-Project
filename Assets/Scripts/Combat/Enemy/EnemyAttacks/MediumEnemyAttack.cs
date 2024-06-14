using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumEnemyAttack : GenericEnemyAttack
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

    }

    private void ProcessNormalEnemy(int heroPosition)
    {

    }

    void AttackClose()
    {
        Attack(4, 1f, 25);
        Attack(9, 1f, 25);
        Attack(14, 1f, 25);
    }

    void Arrow(int heroCurrentPosition)
    {
        List<int> attackPositions = new List<int>();
        attackPositions.Add(heroCurrentPosition);

        for (int i = heroCurrentPosition - heroCurrentPosition % 5; i < heroCurrentPosition - heroCurrentPosition % 5 + 5; i++)
        {
            attackPositions.Add(i);
        }

        Attack(attackPositions, 0.7f, 20);
    }
}
