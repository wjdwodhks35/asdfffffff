using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;          // ������ٵ�
    [SerializeField] SpriteRenderer sprite;      // ��������Ʈ

    public float MoveSpeed;     // �ӵ� ����
    public float Life;          // ü��
    public float JumpPower;     // ������
    float hor;                  // ����
    bool isJump = false;        // �ߺ� ���� �ȵǰ� �ϱ�

    bool isLive = true;         // ������� �Ǵ�
    Animator anime;             // ���ϸ��̼�

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