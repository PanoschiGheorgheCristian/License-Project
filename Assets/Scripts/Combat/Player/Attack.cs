using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // 0 - Bow || 1 - Staff || 2 - Shield || 3 - Sword || 4 - Mace
    public int indexCurrentWeapon = 2;
    Weapon currentWeapon;
    readonly List<Weapon> weapons = new();
    int isAttacking;
    public GameObject enemyManager;
    public GameObject enemy;
    public float attackCooldown;

    // Start is called before the first frame update
    void Start()
    {
        weapons.Add(new Bow());
        weapons.Add(new Staff());
        weapons.Add(new Shield());
        weapons.Add(new Sword());
        weapons.Add(new Mace());
        indexCurrentWeapon = 2;
        isAttacking = 0;
        currentWeapon = weapons[indexCurrentWeapon];
    }

    // Update is called once per frame
    void Update()
    {
        enemy = enemyManager.GetComponent<EnemyManage>().GetCurrentEnemy();
        currentWeapon = weapons[indexCurrentWeapon];
        if (Input.GetKeyDown(KeyCode.E) && isAttacking == 0)
        {
            AttackEnemy(currentWeapon, enemy);
        }
    }

    void AttackEnemy(Weapon currentWeapon, GameObject enemy)
    {
        isAttacking = 1;
        currentWeapon.Attack(enemy);
        StartCoroutine(CooldownAttack(attackCooldown));
    }

    IEnumerator CooldownAttack(float time)
    {
        yield return new WaitForSeconds(time);

        isAttacking = 0;
    }
}
