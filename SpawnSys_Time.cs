using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSys_Time : MonoBehaviour
{
    //時間経過でオブジェクトを生成するスクリプト


    private float time = 0f;//経過時間
    private float interval;//生成時間間隔

    [Header("Set Enemy Prefab")] //ヘッダー
     public GameObject SpawnObj;

    [Header("Set Interval Min and Max")]//ヘッダー
    [Range(1f, 4f)]public float minTime;//生成時間間隔の最小値
    [Range(5f, 10f)]public float maxTime;//生成時間間隔の最大値
  
    [Header("Set XYZ Position Min and Max")]//ヘッダー
    [Range(-24f, 25f)] public float xMinPosi;
    [Range(-24f, 25f)]public float xMaxPosi;
    [Range(-24f, 25f)]public float zMinPosi;
    [Range(-24f, 25f)]public float zMaxPosi;
    [Range(0f,2f)] public float yPosi;

    // Start is called before the first frame update
    void Start()
    {
        interval = GetRandomTime();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime; //時間計測
        if (time > interval)
        { 
          GameObject pickUp = Instantiate(SpawnObj);//enemyのインスタンス化(生成する)
          //pickUp.transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);//インスタンスの生成位置
          pickUp.transform.position = GetRandomPosition();//インスタンス位置のランダム化
          time = 0f;//生成時間のリセット
          interval = GetRandomTime();    
        }
    }

    private float GetRandomTime()
    { 
     return Random.Range(minTime, maxTime);
    }

    private Vector3 GetRandomPosition()//x,zポジションだけランダム化しVector3として渡す
    {
        float x = Random.Range(xMinPosi, xMaxPosi);
        float z = Random.Range(xMinPosi, xMaxPosi);
        return new Vector3(x, yPosi, z);
    }

}
