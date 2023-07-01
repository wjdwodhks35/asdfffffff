using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;          // 리지드바디
    [SerializeField] SpriteRenderer sprite;      // 스프라이트

    public float MoveSpeed;     // 속도 조절
    public float Life;          // 체력
    public float JumpPower;     // 점프력
    float hor;                  // 방향
    bool isJump = false;        // 중복 점프 안되게 하기

    bool isLive = true;         // 살아있음 판단
    Animator anime;             // 에니메이션

    private void Awake()
    {
        anime = GetComponent<Animator>();
    }
    private void Update()
    {
        Flip();
        Jump();
        UpdataState();
    }
    private void FixedUpdate()
    {
        hor = Input.GetAxisRaw("Horizontal");

        rigid.velocity = new Vector2(hor * MoveSpeed, rigid.velocity.y);

    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            isJump = true;
            rigid.velocity = Vector2.up * JumpPower;
            anime.SetBool("isJumping", true);
            anime.SetTrigger("doJumping");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJump = false;
            anime.SetBool("isJumping", false);
        }
    }
    private void UpdataState()
    {
        if (Life <= 0)
            isLive = false;
        if (isLive == false)
            anime.SetBool("isDeath", true);
        if (hor != 0)
        {
            anime.SetBool("isMoving", true);
        }
        else
        {
            anime.SetBool("isMoving", false);
        }
    }
    private void Flip()
    {
        if (hor < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (hor > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}