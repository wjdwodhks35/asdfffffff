using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagement : MonoBehaviour
{
    public float Speed = 0f;
    public int EnemyLife = 10;
    public int nextMove;

    SpriteRenderer sprite;
    Animator anime;
    private void Start()
    {
        anime = GetComponent<Animator>();
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
            transform.localScale = new Vector3(1f, 1f, 1);
        }
        else if (nextMove == 2)
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(-1f, 1f, 1);
        }
        else
        {
            enemySpeed = 0;
        }

        //if(this.transform.position.x > 6.46 || nextMove == 1)
        //    enemySpeed = 0;
        //else if(this.transform.position.x < -6.46f || nextMove == 2)
        //    enemySpeed = 0;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -6.46f, 6.46f), transform.position.y, transform.position.z);

        transform.position += moveVelocity * enemySpeed;

    }
    int Hit = 0;
    public float Atkcool;
    public float Delay;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))                                     // 공격 하기
        {
            StartCoroutine(enemyAtkcool(Atkcool));
        }
        if (other.gameObject.CompareTag("PlayerAttackRange") && Hit == 0)   // 공격 받기
        {
            EnemyLife--;
            SetDamaged();
        }
        else
            return;
    }
    private void SetDamaged()       // 공격 받기 함수
    {
        if (EnemyLife <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    IEnumerator enemyAtkcool(float motionDelay)     // 코루틴으로 공격하기
    {
        nextMove = 0;
        yield return motionDelay;
        anime.SetBool("EnemyAtk", true);
    }
}