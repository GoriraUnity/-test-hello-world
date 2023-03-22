using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalSys : MonoBehaviour
{
    //ゴールした時のクリア判定スクリプト

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject.Find("GAMESYSTEM").GetComponent<GameSys>().Gameclear();
            GameObject.Find("Trex_walk").GetComponent<Moving>().GameStop();
        
        }

    }

}
