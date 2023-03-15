using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WatchSys : MonoBehaviour
{
    //腕時計のオブジェクトに触れるとフルーツの必要数を再表示させるスクリプト
    public GameObject NeedFruitsText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "hand")
        {
            NeedFruitsText.SetActive(true);
            Invoke("comentOut", 3f);
        }
    }

    void comentOut()
    {
        NeedFruitsText.SetActive(false);
    }
}
