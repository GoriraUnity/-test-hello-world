using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnSys : MonoBehaviour
{
    public GameObject[] SpawnPrefab;//配列、生成するオブジェクトを格納する変数
    public int number;//生成する数
    public float spawnRadius;//生成半径
    public bool spawnOnStart;//生成判定

    // Start is called before the first frame update
    void Start()
    {
        if (spawnOnStart)
        {
            SpawnPoint();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)//当たり判定、Spawnポイントのコライダーに触れた時に生成する
    {
        if (other.tag == "Player")//Spawnポイントのコライダーに触れたオブジェクトのタグがPlayerだった場合
        {
            SpawnPoint();
        }
    }


    public void SpawnPoint()
    {
        for (int i = 0; i < number; i++)//生成する数だけFor分を実行する
        {
            Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius; //生成ポイントをある程度ランダム化するコード
            int randomIndex = RandomIndex(SpawnPrefab);//配列に格納された敵(0始まりの番号が付く)からランダムで番号が選ばれ代入される

            NavMeshHit hit; //Navmeshの上(地面の上)にちゃんと生成されるようにするためのコード、配列に含めるオブジェクトはNavMesh付ける
            if (NavMesh.SamplePosition(randomPos, out hit, 5.0f, NavMesh.AllAreas))
            {
                Instantiate(SpawnPrefab[randomIndex], randomPos, Quaternion.identity);
                //World軸と同じ回転軸で生成
            }
            else
            {
                i--;//iから‐1を行い、処理回数を増やす
            }
        }
    }

    //生成するオブジェクトをランダムで配列から取り出す関数の作成
    public int RandomIndex(GameObject[] game)//引数として配列を渡す、Gameは引数の名前として設定、SpawnPointで呼出される
    {
        return Random.Range(0, game.Length);
        //0から配列に格納したオブジェクトの種類数までの間でランダムに選ばれた数値が返される
    }

}
