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
        heroPositions = new List<GameObject>();
        heroPositions.Add(GameObject.Find("Position0"));
        heroPositions.Add(GameObject.Find("Position1"));
        heroPositions.Add(GameObject.Find("Position2"));
        heroPositions.Add(GameObject.Find("Position3"));
        heroPositions.Add(GameObject.Find("Position4"));
        heroPositions.Add(GameObject.Find("Position5"));
        heroPositions.Add(GameObject.Find("Position6"));
        heroPositions.Add(GameObject.Find("Position7"));
        heroPositions.Add(GameObject.Find("Position8"));
        heroPositions.Add(GameObject.Find("Position9"));
        heroPositions.Add(GameObject.Find("Position10"));
        heroPositions.Add(GameObject.Find("Position11"));
        heroPositions.Add(GameObject.Find("Position12"));
        heroPositions.Add(GameObject.Find("Position13"));
        heroPositions.Add(GameObject.Find("Position14"));      
    }

    protected void attack(int position,float attackDelay, int damage)
    {
        isAttacking = 1;
        heroPositions[position].GetComponent<SpriteRenderer>().color = colors[0];
        StartCoroutine(CheckHitAfterTime(attackDelay, 0.15f, timeExhausted, position, damage));
    }

    protected void attack(List<int> positions,float attackDelay, int damage)
    {
        foreach(int i in positions)
        {
            attack(i, attackDelay, damage);
        }
    }

    protected void dealDamage(int damage)
    {
        hero.GetComponent<HeroStatus>().UpdateHealth(hero.GetComponent<HeroStatus>().health - damage);
    }

    protected IEnumerator CheckHitAfterTime(float time1, float time2, float time3, int heroCurrentPosition, int damage)
    {
        yield return new WaitForSeconds(time1);

        int position = heroCurrentPosition;
        if (position == hero.GetComponent<PlayerController>().heroCurrentPosition && !hero.GetComponent<HeroStatus>().isShielded)
            dealDamage(damage);
        heroPositions[position].GetComponent<SpriteRenderer>().color = colors[2];

        yield return new WaitForSeconds(time2);
        
        isExhausted = 1;
        isAttacking = 0;
        heroPositions[heroCurrentPosition].GetComponent<SpriteRenderer>().color = colors[1];

        yield return new WaitForSeconds(time3);

        isExhausted = 0;
    }
}
