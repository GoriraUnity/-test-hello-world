using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagSys : MonoBehaviour
{
    //フルーツ回収籠のスクリプト

    public AudioSource ItemGetSE;
    public GameSys GAMESYS;//GameSys.scの変数を使用するための変数


    private void OnTriggerEnter(Collider other)
    {
      if (other.gameObject.tag == "fruits")
      {
        Debug.Log("フルーツを拾った");
       //GameObject.Find("GAMESYSTEM").GetComponent<GameSys>().Count();
        GAMESYS.Count();
        ItemGetSE.Play();
     　 Destroy(other.gameObject,0.0f);   
      }
    }



    /*Bagオブジェクト(Meshcollider)とフルーツオブジェクトをOncorillsionで
      衝突判定を設定し実行するとUnity上では処理されるが、実際にMeta2でフルーツを掴んで
    　Bagに入れても反応しなかった。
    　OnTriggerであれば処理された
     
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "fruits")
        {
            Debug.Log("フルーツを拾った");
            GameObject.Find("GAMESYSTEM").GetComponent<GameSys>().Count();
            ItemGetSE.Play();
        }

    }*/

}
