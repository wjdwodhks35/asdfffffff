using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagement : MonoBehaviour
{
    public float Speed = 0f;
    public int EnemyLife;
    public int nextMove;

    private int destinationX;
    private float EnemyDamage;

    SpriteRenderer sprite;
    Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(changeMovement());
    }
    private void FixedUpdate()
    {
        Moving();
    }
    IEnumerator changeMovement()
    {
        int delayTime = Random.Range(1, 3);
        nextMove = Random.Range(0, 3); // 0 == 정지, 1 == 왼쪽, 2 == 오른쪽

        yield return new WaitForSeconds(delayTime);

        StartCoroutine(changeMovement());
    }
    private void Moving()
    {
        Vector3 moveVelocity = Vector3.zero;
        float enemySpeed = Speed * Time.deltaTime;

        if (nextMove == 1)
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }
        else if (nextMove == 2)
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(-0.5f, 0.5f, 1);
        }
        else
        {
            enemySpeed = 0;
        }

        if(this.transform.position.x > 6.46 || nextMove == 1)
            enemySpeed = 0;
        else if(this.transform.position.x < -6.46f || nextMove == 2)
            enemySpeed = 0;

        transform.position += moveVelocity * enemySpeed;

    }
    public void SetDamaged(int p_val)
    {
        EnemyLife -= p_val;
        if (EnemyLife <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }
}