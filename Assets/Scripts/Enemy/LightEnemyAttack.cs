using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEnemyAttack : GenericEnemyAttack
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
                    preciseAttack(heroCurrentPosition);
    }

    void attackClose() 
    {
        attack(4, 0.75f, 15);
        attack(9, 0.75f, 15);
        attack(14, 0.75f, 15);
    }

    void preciseAttack(int heroCurrentPosition)
    {
        attack(heroCurrentPosition, 0.3f, 10);
    }
}
