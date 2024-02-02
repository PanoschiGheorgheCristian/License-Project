using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumEnemyAttack : GenericEnemyAttack
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
                    arrow(heroCurrentPosition);
    }

    void attackClose() 
    {
        attack(4, 1f, 25);
        attack(9, 1f, 25);
        attack(14, 1f, 25);
    }

    void arrow(int heroCurrentPosition)
    {
        List<int> attackPositions = new List<int>();
        attackPositions.Add(heroCurrentPosition);

        for(int i = heroCurrentPosition - heroCurrentPosition % 5; i < heroCurrentPosition - heroCurrentPosition % 5 + 5; i++)
        {
            attackPositions.Add(i);
        }

        attack(attackPositions, 1.5f, 20);
    }
}
