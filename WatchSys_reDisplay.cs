using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WatchSys_reDisplay : MonoBehaviour
{
    public GameObject NeedFruitsText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "hand")
        {
            FruitsText();
            Invoke("comentOut", 3f);
        }        
    }


    public void FruitsText()
    {
        NeedFruitsText.SetActive(true);
    }

    void comentOut()
    {
        NeedFruitsText.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
