using UnityEngine;

public class EnemyManage : MonoBehaviour
{

    public GameObject[] enemyTypes = new GameObject[15];
    public int currentEnemy;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 16; i++)
            enemyTypes[i].SetActive(false);

        currentEnemy = GetEnemyNr();
    }

    private int GetEnemyNr()
    {
        int enemyNumber = EnemyToFight.currentEnemy;
        enemyTypes[enemyNumber].SetActive(true);

        return enemyNumber;
    }

    public GameObject GetCurrentEnemy()
    {
        return enemyTypes[currentEnemy];
    }
}

public static class EnemyToFight
{
    public static int currentEnemy;
    public static bool isElite;
    public static bool isBoss;
}