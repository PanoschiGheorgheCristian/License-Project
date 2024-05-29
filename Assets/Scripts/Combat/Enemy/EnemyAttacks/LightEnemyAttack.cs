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
                if (heroCurrentPosition % 5 == 4)
                    AttackClose();
                else
                    PreciseAttack(heroCurrentPosition);
    }

    void AttackClose()
    {
        Attack(4, 0.75f, 15);
        Attack(9, 0.75f, 15);
        Attack(14, 0.75f, 15);
    }

    void PreciseAttack(int heroCurrentPosition)
    {
        Attack(heroCurrentPosition, 0.3f, 10);
    }
}
