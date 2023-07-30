//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerAttack : PlayerManagement
//{
//    int Hit = 0; // 중복 공격 안되게 하기
//    EnemyManagement enemy;

//    private void Awake()
//    {
//        Hit = 0;
//        enemy = GameObject.FindWithTag("Enemy").GetComponent<EnemyManagement>();
//    }

//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.gameObject.CompareTag("Enemy") && Hit == 0)
//        {
//            enemy.EnemyLife--;
//            Hit++;
//        }
//    }
//}
