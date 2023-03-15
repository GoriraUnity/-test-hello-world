using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagSys : MonoBehaviour
{
    //フルーツ回収籠のスクリプト

    public AudioSource ItemGetSE;

    private void OnTriggerEnter(Collider other)
    {
      if (other.gameObject.tag == "fruits")
      {
        Debug.Log("フルーツを拾った");
        GameObject.Find("GAMESYSTEM").GetComponent<GameSys>().Count();
        ItemGetSE.Play();
     　 Destroy(other.gameObject,0.0f);   
      }
    }
}
