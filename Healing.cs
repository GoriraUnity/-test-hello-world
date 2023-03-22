using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
　//落ちているフルーツで体力を回復するスクリプト

   public AudioSource HealingSE;
   public GameObject GAMESYSTEM;//GameSys.scがインタラクトされているオブジェクトを格納
   private GameSys GAMESYS;//GameSys.scの変数を使用するための変数

    private void Start()
    {
        GAMESYS = GAMESYSTEM.GetComponent<GameSys>();//オブジェクトのスクリプトを取得
    }

    private void OnTriggerEnter(Collider other)//口付近のコライダーにフルーツを持ってきたときに回復する
    {
        if (other.gameObject.tag == "fruits")
        {
            Debug.Log("フルーツを食べた");
            GAMESYS.PlayerHP++;
            if (GAMESYS.PlayerHP > GAMESYS.MaxHP)
            {
                GAMESYS.PlayerHP = GAMESYS.MaxHP;
            }
            HealingSE.Play();
            Destroy(other.gameObject, 0.0f);
        }
    }
}
