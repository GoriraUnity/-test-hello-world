using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WatchSys2 : MonoBehaviour
{
    //腕時計のオブジェクトに触れるとフルーツの必要数を再表示させるスクリプト
    public GameObject Restart,ClearCount;
    bool GearStatus = false;    

    private void Start()
    {       
        Restart.SetActive(false);
        ClearCount.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "hand" && GearStatus == false)
        {
            Restart.SetActive(true);
            ClearCount.SetActive(true);        
            GearStatus = true;  
        }

        else if (other.gameObject.tag == "hand" && GearStatus == true)
        {
            Restart.SetActive(false);
            ClearCount.SetActive(false);
            GearStatus = false;
        }

    }

}
