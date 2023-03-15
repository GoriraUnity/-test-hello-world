using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class GoalArrow : MonoBehaviour
{
    //ゲームクリアに必要なフルーツを集めた後にゴールの場所を指す矢印を表示する

    public GameObject GoalPoint,ArrowOBJ;
    Vector3 ArrowPoint;
    private GameObject GAMESYSTEM;//GameSys.scの変数を使用するための変数
    private GameSys GAMESYS;      //GameSys.scの変数を使用するための変数
    MeshRenderer ArrowMesh;　　 　//スタート時メッシュレンダラーを消すための変数


    // Start is called before the first frame update
    void Start()
    {
      ArrowMesh = ArrowOBJ.GetComponent<MeshRenderer>();
      ArrowMesh.enabled = false;    
      GAMESYSTEM = GameObject.Find("GAMESYSTEM");
      GAMESYS = GAMESYSTEM.GetComponent<GameSys>();//オブジェクトのスクリプトを取得
    }


    // Update is called once per frame
    void Update()
    {
        if (GAMESYS.GetFruits >= GAMESYS.ClearCount)
        {
            ArrowMesh.enabled = true;
            ArrowPoint = GoalPoint.transform.position;
            transform.LookAt(new Vector3(ArrowPoint.x, transform.position.y, ArrowPoint.z));
        }

        if (GAMESYS.GameClearState == true)
        {
            ArrowMesh.enabled = false; 
        }

    }
}
