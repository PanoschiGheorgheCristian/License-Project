using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManage : MonoBehaviour
{

    public GameObject[] enemyTypes = new GameObject[3];
    public int currentEnemy;

    // Start is called before the first frame update
    void Start()
    {
        enemyTypes[0].SetActive(false);
        enemyTypes[1].SetActive(false);
        enemyTypes[2].SetActive(false);

        currentEnemy = GetEnemyNr();
    }

    private int GetEnemyNr()
    {
        int rand = (Random.Range(0, 3));
        enemyTypes[rand].SetActive(true);

        return rand;
    }

    public GameObject GetCurrentEnemy ()
    {
        return enemyTypes[currentEnemy];
    }
}
