using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericEnemyAttack : MonoBehaviour
{
    public GameObject hero;
    List<GameObject> heroPositions;
    public Color[] colors = new Color[3];
    // 0 - threat ; 1 - normal ; 2 - damage
    protected int isAttacking;
    protected int isExhausted;
    public float timeExhausted;

    // Start is called before the first frame update
    void Start()
    {
        isAttacking = 0;
        isExhausted = 0;
        heroPositions = new List<GameObject>
        {
            GameObject.Find("Position0"),
            GameObject.Find("Position1"),
            GameObject.Find("Position2"),
            GameObject.Find("Position3"),
            GameObject.Find("Position4"),
            GameObject.Find("Position5"),
            GameObject.Find("Position6"),
            GameObject.Find("Position7"),
            GameObject.Find("Position8"),
            GameObject.Find("Position9"),
            GameObject.Find("Position10"),
            GameObject.Find("Position11"),
            GameObject.Find("Position12"),
            GameObject.Find("Position13"),
            GameObject.Find("Position14")
        };
    }

    protected void Attack(int position, float attackDelay, int damage)
    {
        isAttacking = 1;
        heroPositions[position].GetComponent<SpriteRenderer>().color = colors[0];
        StartCoroutine(CheckHitAfterTime(attackDelay, 0.15f, timeExhausted, position, damage));
    }

    protected void Attack(List<int> positions, float attackDelay, int damage)
    {
        foreach (int i in positions)
        {
            Attack(i, attackDelay, damage);
        }
    }

    protected void DealDamage(int damage)
    {
        hero.GetComponent<HeroStatus>().UpdateHealth(hero.GetComponent<HeroStatus>().health - damage);
    }

    protected IEnumerator CheckHitAfterTime(float time1, float time2, float time3, int heroCurrentPosition, int damage)
    {
        yield return new WaitForSeconds(time1);

        if(!EnemyStatus.isStunned)
        {
        int position = heroCurrentPosition;
        if (position == hero.GetComponent<PlayerController>().heroCurrentPosition && !hero.GetComponent<HeroStatus>().isShielded)
            DealDamage(damage + (int)(damage * ((EnemyStatus.isBuffed ? 30 : 0) + (EnemyStatus.isDebuffed ? -30 : 0)) / 100));
        heroPositions[position].GetComponent<SpriteRenderer>().color = colors[2];

        yield return new WaitForSeconds(time2);

        isExhausted = 1;
        isAttacking = 0;
        heroPositions[heroCurrentPosition].GetComponent<SpriteRenderer>().color = colors[1];

        yield return new WaitForSeconds(time3);

        isExhausted = 0;
        }
        else
        {
            isExhausted = 0;
            isAttacking = 0;
            heroPositions[heroCurrentPosition].GetComponent<SpriteRenderer>().color = colors[1];
        }
    }
}
