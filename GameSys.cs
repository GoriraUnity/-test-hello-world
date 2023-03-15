using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSys : MonoBehaviour
{
　　/*ゲームシステムを一括で記載している。
   　 プレイヤーHPや恐竜の攻撃力、クリア・ゲームオーバー条件、バトルシステム(計算)
  　　コメント表示非表示など*/


    [Header("BattlePoint")]
    [Range(1,10)] public int MaxHP;
    [Range(1, 10)] public int TrexAttack;

    [System.NonSerialized]
    public int PlayerHP;//Moving.scの行動判定に使用するためpoublic化、Inspector操作不可

    [Header("CleareCountRandom")]//ゲームクリアに必要なフルーツ数をランダム化
    [Range(1, 10)] public int minClearCount;
    [Range(1, 10)] public int maxClearCount;

    [System.NonSerialized]
    public int  GetFruits,ClearCount; //GoalArrpw.scの→出現判定に使用するためpoublic化、Inspector操作不可　　
    [System.NonSerialized]
    public bool GameClearState = false;//Moving.scの行動判定に使用するためpoublic化、Inspector操作不可
    
    public GameObject GameClearText, GameOverText,GoHomeText,NeedFruitsText;//表示されるテキストの変数一覧
    public TextMeshProUGUI FruitsCount, NeedFruits;//フルーツの取得数(TMP)を変化させるための変数
    public AudioSource GameOverMusic,ClearMusic;

    [SerializeField]//ダメージによって画面が赤く狭まるための変数
    private Volume GV;//Hierarchy内のGlobalVolumeを格納する変数
    private Vignette vignette;   

    // Start is called before the first frame update
    void Start()
    {
      PlayerHP = MaxHP;
      ComentOut();
      GameOverMusic.Stop();
      ClearCount = Random.Range(minClearCount,maxClearCount);
      NeedFruits.text = ClearCount.ToString();
      Invoke("ComentOut2", 5);
      GV.profile.TryGet<Vignette>(out vignette);//GlobalVolume内のVigentteを取得、変数vignetteにVignetteを出力する
    }

    // Update is called once per frame
    void Update()
    {
      BattleDamege(); 
    }

    void BattleDamege()//ダメージ量によって画面が赤くなる
    {
        if (PlayerHP > 3)
        {
            vignette.intensity.value = 0.0f; //ビネット効果の実行
        }
        else if (PlayerHP == 3)
        {
            vignette.intensity.value = 0.3f;
        }
        else if (PlayerHP ==  2)
        {
            vignette.intensity.value = 0.5f; 
        }
        else if (PlayerHP == 1)
        {
            vignette.intensity.value = 0.7f;
        }
        else if (PlayerHP == 0)
        {
            vignette.intensity.value = 1.0f;
        }
    }

    public void ComentOut()//コメントをスタート時に消す
    {
        GameClearText.SetActive(false);
        GameOverText.SetActive(false);
        GoHomeText.SetActive(false);
    }

    public void ComentOut2()//時間差でクリア条件を消す
    {
        NeedFruitsText.SetActive(false);
    }

    public void Count()//Bag.scで呼出す加算スクリプト
    {
        GetFruits++;
        Debug.Log("加算する"+ GetFruits);
        FruitsCount.text= GetFruits.ToString();
        ClearJudge();      
    }

    public void Battle()
    {
        PlayerHP = PlayerHP - TrexAttack;
        GameoverJudge();
    }

    public void ClearJudge()
    {
        if (GetFruits >= ClearCount)
        {
            GoHomeText.SetActive(true);
            Invoke("ComentOut", 5);
        }
        else
        {
            //足りない個数を表示する
            Debug.Log("足りない");
        }
    }

    public void Gameclear()
    {
        if (GetFruits >= ClearCount)//クリア条件を満たした場合
        {
            GameClearText.SetActive(true);
            GameClearState = true;
            ClearMusic.Play();
            Invoke("Restart", 3f);
        }
        else
        {
            //足りない個数を表示する
            Debug.Log("足りない");
        }
    }

    public void GameoverJudge()
    {
    　 if(PlayerHP <= 0)
     　{　
         GameOverText.SetActive(true);
         GameOverMusic.Play();
         Invoke("Restart",3f);        
       }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
