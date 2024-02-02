using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemyAttack : GenericEnemyAttack
{
    int heroCurrentPosition;
    // Update is called once per frame
    void Update()
    {
        heroCurrentPosition = hero.GetComponent<PlayerController>().heroCurrentPosition;
        if(isAttacking == 0 && isExhausted == 0)
            if(hero.GetComponent<HeroStatus>().alive == 1)
                if(heroCurrentPosition % 5 == 4)
                    attackClose();
                else
                    groundSlam(heroCurrentPosition);
    }

    void attackClose() 
    {
        attack(4, 1.5f, 35);
        attack(9, 1.5f, 35);
        attack(14, 1.5f, 35);
    }

    void groundSlam(int heroCurrentPosition)
    {
        List<int> attackPositions = new List<int>();
        attackPositions.Add(heroCurrentPosition);

        if(heroCurrentPosition > 4)
            attackPositions.Add(heroCurrentPosition - 5);
        if(heroCurrentPosition % 5 != 0)
            attackPositions.Add(heroCurrentPosition - 1);
        if(heroCurrentPosition < 10)
            attackPositions.Add(heroCurrentPosition + 5);
        if(heroCurrentPosition % 5 != 4)
            attackPositions.Add(heroCurrentPosition + 1);

        attack(attackPositions, 1.5f, 20);
    }
}
