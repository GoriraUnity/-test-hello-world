using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTime : MonoBehaviour
{
    //時間制限で石とフルーツを消すスクリプト
    [SerializeField] float destroyTime;
    float count;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       count += Time.deltaTime;
        if (count >= destroyTime)
        {
            Destroy(this.gameObject);   
        }
    }
}
