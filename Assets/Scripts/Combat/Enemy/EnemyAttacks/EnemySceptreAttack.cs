using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySceptreAttack : GenericEnemyAttack
{
    int heroCurrentPosition;

    private void Awake() {
        if(EnemyToFight.isElite)
            EnemyStatus.debuffPower = -50;
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
        StartCoroutine(PlusAttack(heroPosition));
    }

    private void ProcessNormalEnemy(int heroPosition)
    {
        StartCoroutine(SwipeUp(heroPosition));
    }

    IEnumerator SwipeUp(int heroPosition)
    {
        int heroHealth = hero.GetComponent<HeroStatus>().health;

        Attack(heroPosition, 1f, 15);
        if (heroPosition < 10)
            Attack(heroPosition + 5, 1f, 15);
        if (heroPosition > 4)
            Attack(heroPosition - 5, 1f, 15);

        yield return new WaitForSeconds(1f);

        if (heroHealth != hero.GetComponent<HeroStatus>().health)
            DebuffHero();

        StartCoroutine(LoseExhausted(timeExhausted));
    }

    IEnumerator PlusAttack(int heroPosition)
    {
        int heroHealth = hero.GetComponent<HeroStatus>().health;

        Attack(heroPosition, 1f, 15);
        if (heroPosition < 10)
            Attack(heroPosition + 5, 1f, 15);
        if (heroPosition > 4)
            Attack(heroPosition - 5, 1f, 15);
        if (heroPosition % 5 != 0)
            Attack(heroPosition - 1, 1f, 15);
        if (heroPosition % 5 != 4)
            Attack(heroPosition + 1, 1f, 15);

        yield return new WaitForSeconds(1f);

        if (heroHealth != hero.GetComponent<HeroStatus>().health)
            DebuffHero();

        StartCoroutine(LoseExhausted(timeExhausted));
    }

    void DebuffHero()
    {
        HeroStatus.isDebuffed = true;
        isExhausted = 1;

        StartCoroutine(LoseExhausted(1f));
    }
}
