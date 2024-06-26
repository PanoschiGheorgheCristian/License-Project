using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMonkeyEarringsAttack : GenericEnemyAttack
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
        if (!EnemyStatus.isShielded)
            ProtectSelf();
        else
        {
            StartCoroutine(TwoStrikePreciseAttack(heroPosition));
        }
    }

    private void ProcessNormalEnemy(int heroPosition)
    {
        if(!EnemyStatus.isShielded)
            ProtectSelf();
        else
        {
            StartCoroutine(PreciseAttack(heroPosition));
        }
    }


    void ProtectSelf()
    {
        EnemyStatus.isShielded = true;
        isExhausted = 1;
        Debug.Log("Protected");

        StartCoroutine(LoseExhausted(1.5f));
    }

    IEnumerator PreciseAttack(int heroPosition)
    {
        isAttacking = 1;
        int heroHealth = hero.GetComponent<HeroStatus>().health;        
        Attack(heroPosition, 1f, 10);

        yield return new WaitForSeconds(1.1f);

        if(heroHealth != hero.GetComponent<HeroStatus>().health)
            HeroStatus.isStunned = true;

        StartCoroutine(LoseExhausted(timeExhausted));
    }

    IEnumerator TwoStrikePreciseAttack(int heroPosition)
    {
        isAttacking = 1;
        int heroHealth = hero.GetComponent<HeroStatus>().health;
        Attack(heroPosition, 0.3f, 10);

        yield return new WaitForSeconds(0.3f);

        if (heroHealth != hero.GetComponent<HeroStatus>().health)
            HeroStatus.isStunned = true;

        yield return new WaitForSeconds(0.2f);

        Attack(heroPosition, 1.5f, 15);

        StartCoroutine(LoseExhausted(1.5f + timeExhausted));
    }
}
