using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalSys : MonoBehaviour
{
    //�S�[���������̃N���A����X�N���v�g

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject.Find("GAMESYSTEM").GetComponent<GameSys>().Gameclear();
            GameObject.Find("Trex_walk").GetComponent<Moving>().GameStop();
        
        }

    }

}
