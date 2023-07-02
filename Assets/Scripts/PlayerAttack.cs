//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerAttack : PlayerManagement
//{
//    int Hit = 0; // 중복 공격 안되게 하기

//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.gameObject.CompareTag("Enemy") && Hit == 0)
//        {
//            EnemyManagement.EnemyLife--;  // 공격력에 비례해서 적에게 공격하기
//            Hit++;
//        }
//    }
//}
