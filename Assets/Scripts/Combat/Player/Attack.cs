using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // 0 - Bow || 1 - Staff || 2 - Shield || 3 - Sword || 4 - Mace
    public int indexCurrentWeapon = 2;
    public Weapon currentWeapon;
    public int isAttacking;
    public GameObject enemyManager;
    public GameObject enemy;
    public float attackCooldown;

    // Start is called before the first frame update
    void Start()
    {
        string json = SaveObject.getJsonSave();

        SaveObject save = JsonUtility.FromJson<SaveObject>(json);

        indexCurrentWeapon = 2;
        isAttacking = 0;
        currentWeapon = PlayerWeapons.GetWeapons()[indexCurrentWeapon];
    }

    // Update is called once per frame
    void Update()
    {
        enemy = enemyManager.GetComponent<EnemyManage>().GetCurrentEnemy();
        currentWeapon = PlayerWeapons.GetWeapons()[indexCurrentWeapon];
        if (Input.GetKeyDown(KeyCode.E) && isAttacking == 0 && !HeroStatus.isStunned)
        {
            AttackEnemy(currentWeapon, enemy);
        }
        if(Input.GetKeyDown(KeyCode.P))
            enemy.GetComponent<EnemyStatus>().health = 0;
    }

    void AttackEnemy(Weapon currentWeapon, GameObject enemy)
    {
        isAttacking = 1;
        if(!EnemyStatus.isShielded)
            currentWeapon.Attack(enemy, gameObject);
        StartCoroutine(CooldownAttack(attackCooldown));
    }

    IEnumerator CooldownAttack(float time)
    {
        yield return new WaitForSeconds(time);

        isAttacking = 0;
    }
}
