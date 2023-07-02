using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;

    [Header("스탯정보")]
    public float MoveSpeed;     // 속도 조절
    public float PlayerLife;    // 체력
    public float JumpPower;     // 점프력
    [Header("공격정보")]
    public float delay = 0;

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
        Attack1();
    }


    public List<GameObject> m_AttackObjList = new List<GameObject>();


    //GameObject compareobj = null;
    //public bool Predicatea(GameObject obj)
    //{
    //    if (compareobj == obj)
    //        return true;

    //    return false;
    //}

    public void EnterTarget(GameObject p_obj)
    {
        //compareobj = p_obj;
        //GameObject findobk2 = m_AttackObjList.Find(Predicatea);

        GameObject findobk = m_AttackObjList.Find((x) =>
        {
            if (x == p_obj)
            {
                return true;
            }

            return false;
        }); //람다식
        if (findobk != null)
        {
            return;
        }

        m_AttackObjList.Add(p_obj);
        // 공격데미지 줘라
        PlayerLife--;
    }


    public void EndAttack()
    {
        m_AttackObjList.Clear();
    }

    private void FixedUpdate()
    {
        hor = Input.GetAxisRaw("Horizontal");
        if (m_ISAttack)
            MoveSpeed = 0;
        else
            MoveSpeed = 5;
        rigid.velocity = new Vector2(hor * MoveSpeed, rigid.velocity.y);
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJump && m_ISAttack == false)
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
        if (PlayerLife <= 0)
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
        if (delay <= 0)
        {
            m_ISAttack = false;
            m_ISNextAttack = false;
            m_ISReadAttack = false;
        }
    }
    private void Flip()
    {
        if (m_ISAttack == false)
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

    public bool m_ISAttack = false;         // 현재 공격중인가(첫번째 공격한정)
    public bool m_ISReadAttack = false;     // 다음연계공격을 할 준비가 되었는가?
    public bool m_ISNextAttack = false;     // 연계공격 중인가?

    private void Attack1()
    {
        if (m_ISReadAttack && Input.GetKeyDown(KeyCode.Mouse0))
        {
            m_ISNextAttack = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)
            && m_ISAttack == false && isJump == false
            && m_ISNextAttack == false)
        {
            delay = 0.4f;
            anime.SetBool("firstAttack", true);
            StartCoroutine(AttackCorutine2());
        }
    }

    void _On_CompareAttack()
    {
        if (m_ISNextAttack)
        {
            anime.SetBool("comboAttack", true);
            delay = 10f;       // 대충 시간 길게 해놔서 애니메이션 적용되게 해놓은 거
        }
    }
    void _On_endAttack()
    {
        delay = 0.2f;
        StartCoroutine(AttackCorutine2());
    }
    void Attack2()
    {
        m_ISReadAttack = true;

        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    m_ISNextAttack = true;
        //    anime.SetBool("comboAttack", true);
        //}
    }
    IEnumerator AttackCorutine2()
    {
        m_ISAttack = true;
        while (true)
        {
            delay -= Time.deltaTime;
            yield return null;
            if (delay <= 0)
            {
                anime.SetBool("firstAttack", false);
                anime.SetBool("comboAttack", false);
                break;
            }
        }//float currtime = Time.time + p_actdelay;
        //while (true)
        //{
        //    yield return null;

        //    if (currtime <= Time.time)
        //    {
        //        break;
        //    }
        //}
        
    }

    //IEnumerator DelayTimer(float actDelay)
    //{
    //    yield return new WaitForSecondsRealtime(actDelay); // 딜레이만큼 기다리기

    //    //float currtime = Time.time + actDelay;
    //    //while(true)
    //    //{
    //    //    yield return null;

    //    //    if(currtime <= Time.time)
    //    //    {
    //    //        break;
    //    //    }
    //    //} yield return new WaitForSecondsRealtime(actDelay); 이거 내부 코드임

    //    if (actDelay == delay)
    //    {
    //        transform.Find("AttackVisible").gameObject.SetActive(false);
    //        anime.SetBool("isAttack", false);
    //        anime.SetBool("comboAttack", false);
    //    }
    //}
}