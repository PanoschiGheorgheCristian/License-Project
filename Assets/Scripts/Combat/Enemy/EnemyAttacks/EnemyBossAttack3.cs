using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossAttack3 : GenericEnemyAttack
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
                    GroundSlam(heroCurrentPosition);
    }

    void AttackClose()
    {
        Attack(4, 1.5f, 35);
        Attack(9, 1.5f, 35);
        Attack(14, 1.5f, 35);
    }

    void GroundSlam(int heroCurrentPosition)
    {
        List<int> attackPositions = new List<int>();
        attackPositions.Add(heroCurrentPosition);

        if (heroCurrentPosition > 4)
            attackPositions.Add(heroCurrentPosition - 5);
        if (heroCurrentPosition % 5 != 0)
            attackPositions.Add(heroCurrentPosition - 1);
        if (heroCurrentPosition < 10)
            attackPositions.Add(heroCurrentPosition + 5);
        if (heroCurrentPosition % 5 != 4)
            attackPositions.Add(heroCurrentPosition + 1);

        Attack(attackPositions, 1f, 20);
    }
}
