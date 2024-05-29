using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrowDaggerAttack : GenericEnemyAttack
{
    int heroCurrentPosition;
    // Update is called once per frame
    void Update()
    {
        //Check for Elite Enemy / Boss by looking at EnemyToFight.isElite / EnemyToFight.isBoss
        heroCurrentPosition = hero.GetComponent<PlayerController>().heroCurrentPosition;
        if (isAttacking == 0 && isExhausted == 0 && !EnemyStatus.isStunned)
            if (hero.GetComponent<HeroStatus>().alive == 1)
                if (heroCurrentPosition % 5 == 4)
                    AttackClose();
                else
                    IronArrow(heroCurrentPosition);
    }

    void AttackClose()
    {
        Attack(4, 0.5f, 10);
        Attack(9, 0.5f, 10);
        Attack(14, 0.5f, 10);
    }

    void IronArrow(int heroCurrentPosition)
    {
        List<int> attackPositions = new List<int>();
        attackPositions.Add(heroCurrentPosition);

        for (int i = heroCurrentPosition - heroCurrentPosition % 5; i < heroCurrentPosition - heroCurrentPosition % 5 + 5; i++)
        {
            attackPositions.Add(i);
        }

        Attack(attackPositions, 0.7f, 10);
    }
}
