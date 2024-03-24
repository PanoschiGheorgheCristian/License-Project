using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int heroCurrentPosition;
    private readonly Vector3[] heroBoardPositions = new Vector3[15];
    public Attack attackScript;
    int canMove;
    int isDashing;
    public float movementDelay;
    public float shieldDuration;

    // Start is called before the first frame update
    void Start()
    {

        heroBoardPositions[0] = new Vector3(-8f, -1.5f, -1f);
        heroBoardPositions[1] = new Vector3(-5.5f, -1.5f, -1f);
        heroBoardPositions[2] = new Vector3(-3f, -1.5f, -1f);
        heroBoardPositions[3] = new Vector3(-0.5f, -1.5f, -1f);
        heroBoardPositions[4] = new Vector3(2f, -1.5f, -1f);

        heroBoardPositions[5] = new Vector3(-8f, 0.5f, -1f);
        heroBoardPositions[6] = new Vector3(-5.5f, 0.5f, -1f);
        heroBoardPositions[7] = new Vector3(-3f, 0.5f, -1f);
        heroBoardPositions[8] = new Vector3(-0.5f, 0.5f, -1f);
        heroBoardPositions[9] = new Vector3(2f, 0.5f, -1f);

        heroBoardPositions[10] = new Vector3(-8f, 2.5f, -1f);
        heroBoardPositions[11] = new Vector3(-5.5f, 2.5f, -1f);
        heroBoardPositions[12] = new Vector3(-3f, 2.5f, -1f);
        heroBoardPositions[13] = new Vector3(-0.5f, 2.5f, -1f);
        heroBoardPositions[14] = new Vector3(2f, 2.5f, -1f);
        heroCurrentPosition = 7;
        canMove = 1;
    }

    // Update is called once per frame
    void Update()
    {
        CheckSpace();
        Move();
        UseShieldCharge();
    }

    private void CheckSpace()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            isDashing = 1;
        if (Input.GetKeyUp(KeyCode.Space))
            isDashing = 0;
    }
    private void Move()
    {
        if (canMove == 1)
        {
            if (Input.GetKeyDown(KeyCode.A) && heroCurrentPosition % 5 != 0)
            {
                heroCurrentPosition = heroCurrentPosition - 1 - (heroCurrentPosition % 5 == 1 ? 0 : isDashing);
                transform.position = heroBoardPositions[heroCurrentPosition];
                attackScript.indexCurrentWeapon = heroCurrentPosition % 5;
                canMove = 0;
                StartCoroutine(RegainMovement(movementDelay));
            }

            if (Input.GetKeyDown(KeyCode.D) && heroCurrentPosition % 5 != 4)
            {
                heroCurrentPosition = heroCurrentPosition + 1 + (heroCurrentPosition % 5 == 3 ? 0 : isDashing);
                transform.position = heroBoardPositions[heroCurrentPosition];
                attackScript.indexCurrentWeapon = heroCurrentPosition % 5;
                canMove = 0;
                StartCoroutine(RegainMovement(movementDelay));
            }

            if (Input.GetKeyDown(KeyCode.S) && heroCurrentPosition > 4)
            {
                heroCurrentPosition = heroCurrentPosition - 5 - 5 * (heroCurrentPosition < 10 ? 0 : isDashing);
                transform.position = heroBoardPositions[heroCurrentPosition];
                attackScript.indexCurrentWeapon = heroCurrentPosition % 5;
                canMove = 0;
                StartCoroutine(RegainMovement(movementDelay));
            }

            if (Input.GetKeyDown(KeyCode.W) && heroCurrentPosition < 10)
            {
                heroCurrentPosition = heroCurrentPosition + 5 + 5 * (heroCurrentPosition > 4 ? 0 : isDashing);
                transform.position = heroBoardPositions[heroCurrentPosition];
                attackScript.indexCurrentWeapon = heroCurrentPosition % 5;
                canMove = 0;
                StartCoroutine(RegainMovement(movementDelay));
            }
        }
    }

    public void UseShieldCharge()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !gameObject.GetComponent<HeroStatus>().isShielded && gameObject.GetComponent<HeroStatus>().shieldCharges > 0)
        {
            gameObject.GetComponent<HeroStatus>().isShielded = true;
            gameObject.GetComponent<HeroStatus>().shieldCharges = gameObject.GetComponent<HeroStatus>().shieldCharges - 1;
            StartCoroutine(LoseShield(shieldDuration));
        }
    }
    public int GetIndex(Vector3 position)
    {
        for (int i = 0; i < heroBoardPositions.Length; i++)
        {
            if (position == heroBoardPositions[i])
                return i;
        }
        return -1;
    }

    IEnumerator LoseShield(float shieldDuration)
    {
        yield return new WaitForSeconds(shieldDuration);
        Debug.Log("Shield Expired");
        gameObject.GetComponent<HeroStatus>().isShielded = false;
    }
    IEnumerator RegainMovement(float movementDelay)
    {
        yield return new WaitForSeconds(movementDelay);
        canMove = 1;
    }
}
