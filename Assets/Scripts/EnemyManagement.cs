using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagement : MonoBehaviour
{
    private float EnemyDamage;
    public int nextMove = 0;
    public int EnemyLife;
    SpriteRenderer sprite;
    Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Invoke("Think", 1f);

        sprite = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.4f, rigid.position.y - 1.8f);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D raycast = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));

        if (nextMove < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (nextMove > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        if (raycast.collider == null)
        {
            Debug.Log("∂•ø° ¥Í¿Ω");
            nextMove *= -1;
            CancelInvoke();
            Invoke("Think", 2f);
            Debug.Log("a");
        }
    }
    private void Think()
    {
        nextMove = Random.Range(-1, 2);
        Debug.Log(nextMove);

        float time = Random.Range(2f, 5f);
        Invoke("Think", time);
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