using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagement : MonoBehaviour
{
    public float Speed = 0f;
    public int EnemyLife;

    private int destinationX;
    private float EnemyDamage;

    SpriteRenderer sprite;
    Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(Moving(0));
    }
    private void FixedUpdate()
    {
          //flip
    }
    IEnumerator Moving(int delayTime)
    {
        destinationX = Random.Range(-8, 8);
        Vector3 destination = new Vector3(destinationX, transform.position.y, 0);
        float enemySpeed = Speed * Time.deltaTime;

        Debug.Log(destinationX);
        for (; this.transform.position.x < 8 || this.transform.position.x > -8 ; )
        {
            rigid.velocity = Vector3.MoveTowards(transform.position, destination, enemySpeed);
            if (transform.position == destination)
                break;
        }
        
        yield return new WaitForSeconds(delayTime);

        delayTime = Random.Range(1, 3);
        StartCoroutine(Moving(delayTime));
    }

    private void flip()
    {
        if (transform.position.x - destinationX < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (transform.position.x - destinationX > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
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