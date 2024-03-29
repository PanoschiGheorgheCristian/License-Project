using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // 0 - Bow || 1 - Staff || 2 - Shield || 3 - Sword || 4 - Mace
    public int indexCurrentWeapon = 2;
    Weapon currentWeapon;
    int isAttacking;
    public GameObject enemyManager;
    public GameObject enemy;
    public float attackCooldown;
    private static string SAVE_FOLDER;

    // Start is called before the first frame update
    void Start()
    {
        SAVE_FOLDER = Application.dataPath + "/Saves";
        string json = File.ReadAllText(SAVE_FOLDER);

        SaveObject save = JsonUtility.FromJson<SaveObject>(json);
        // if (save is not null)
        // {
            // playerWeapons = save.currentWeapons;
            //for eacg weapon get the idnex from the save and go to the database
        // }
        indexCurrentWeapon = 2;
        isAttacking = 0;
        currentWeapon = PlayerWeapons.GetWeapons()[indexCurrentWeapon];
    }

    // Update is called once per frame
    void Update()
    {
        enemy = enemyManager.GetComponent<EnemyManage>().GetCurrentEnemy();
        currentWeapon = PlayerWeapons.GetWeapons()[indexCurrentWeapon];
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
