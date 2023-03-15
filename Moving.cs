using Oculus.Interaction;
using Oculus.Platform.Samples.VrHoops;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class Moving : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    GameObject player;

    [Header("SpeedAdjustment")]
    [Range(0f, 2f)] public float walkingSpeed;
    [Range(2f, 5f)] public float runSpeed;

    [Header("RivivalTime")]
    [Range(1, 10)] public int RevivalTime;
    public AudioSource TrexFootStep, TrexHowl, TrexBite, stoneAttack;
    public AudioClip WalkStepSE, RunStepSE, Howl, HowlLow, BiteDry, BiteWet;
    public GameObject GAMESYSTEM;//GameSys.scがインタラクトされているオブジェクトを取得
    private GameSys GAMESYS;//GameSys.scの変数を使用するための変数
    public GameObject forestObj;
    private Vector3 forestPoint;
  
    enum STATE { IDLE, WALK, ATTACK, CHASE, DAMEGE }
    STATE state = STATE.IDLE;

    // Start is called before the first frame update
    void Start()
    {
      animator = GetComponent<Animator>();
      agent = GetComponent<NavMeshAgent>();
      if (player == null)
      {
          player = GameObject.FindGameObjectWithTag("Player");
      }
      GAMESYS = GAMESYSTEM.GetComponent<GameSys>();//GameSys.scを取得、以降のコードでscの変数を使用する
    }

    public void AnimStop()
    {
        animator.SetBool("WALK", false);
        animator.SetBool("ATTACK", false);
        animator.SetBool("CHASE", false);
        animator.SetBool("DAMEGE", false);
    }

    float DistanceToPlayer()//プレイヤーとの距離を計算するスクリプト
    {     
        return Vector3.Distance(player.transform.position, transform.position);
    }

    bool CanSeePlayer()//DistanceToPlayerの距離を元に、プレイヤーを発見するの関数を作成
    {
        if (DistanceToPlayer() < 15)
        {
            return true;
        }
        return false;
    }

    bool ForGetPlayer()//DistanceToPlayerの距離を元に、プレイヤーを見失った時の関数を作成
    {
        if (DistanceToPlayer() > 20)
        {
            return true;
        }
        return false;
    }

    void InForest()//一定確立で森の中に移動させる
    {
        Debug.Log("森の中がよばれた");
        forestPoint = forestObj.transform.position;
        Vector3 NextPosi = new Vector3(forestPoint.x,0,forestPoint.z);
        agent.speed = walkingSpeed;//歩くスピードを変数から代入する       
        agent.SetDestination(NextPosi);//目的地を設定
        agent.stoppingDistance = 0; 
        animator.SetBool("WALK", true);//アニメーションを呼出す
        TrexWalkFootStep(WalkStepSE);      
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "stone")
        {
            AnimStop();
            agent.SetDestination(new Vector3(transform.position.x, transform.position.y, transform.position.z));
            agent.stoppingDistance = 0;
            stoneAttack.Play(); 
            StopStep();
            Destroy(collision.gameObject,0.0f);    
            state = STATE.DAMEGE;  
        }
    }

    public void TrexWalkFootStep(AudioClip clip)//歩く音を出す
    {
        TrexFootStep.loop = true;//、メソッドが実行されるとLoopが有効になる
        TrexFootStep.pitch = 1f;　//、音の高さを調整する
        TrexFootStep.clip = clip; //音源の設定
        TrexFootStep.Play(); //音源の再生
    }

    public void TrexRunFootStep(AudioClip clip)//走る音を出す
    {
        TrexFootStep.loop = true;//、メソッドが実行されるとLoopが有効になる
        TrexFootStep.pitch = 1.5f;//音の高さを調整する
        TrexFootStep.clip = clip; //音源の設定
        TrexFootStep.Play(); //音源の再生
    }

    public void StopStep()//音を止める
    {
        TrexFootStep.Stop();
        TrexFootStep.loop = false;
        TrexFootStep.pitch = 1f;
    }

    public void HowlSE(AudioClip clip)//吠える音
    {
        TrexHowl.pitch = 1.0f;
        TrexHowl.clip = clip;
        TrexHowl.Play();
    }

    public void HowlLowSE(AudioClip clip)//吠える音2
    {
        TrexHowl.pitch = 1.0f;
        TrexHowl.clip = clip;
        TrexHowl.Play();
    }

    public void BiteDrySE(AudioClip clip)//乾いた噛みついた音
    {
        TrexBite.pitch = 1.0f;
        TrexBite.clip = clip;
        TrexBite.Play();
    }

    public void BiteWetSE(AudioClip clip)//湿った噛みついた音
    {
        TrexBite.pitch = 1.0f;
        TrexBite.clip = clip; 
        TrexBite.Play();
    }


    // Update is called once per frame
    void Update()
    {
        if (GAMESYS.PlayerHP > 0 && GAMESYS.GameClearState == false)//プレイヤーのHPが残っている且つゲームクリアしてない状況で恐竜は動く
        {
            switch (state)
            {
                case STATE.IDLE:

                    if (CanSeePlayer())
                    {
                        state = STATE.CHASE;
                    }
                    else if (Random.Range(0, 1000) < 5)
                    {
                        agent.ResetPath();  //目的値のリセット
                        state = STATE.WALK;
                    }
                    break;

                case STATE.WALK:
                    if (!agent.hasPath)//.hasPathはNevMesh関数の目的地を持っているかどうか確認する関数
                                       //目的地(SetDestinationで設定)が有ればTrueが返ってくる               
                    {
                        TrexHowl.Stop(); //IDELから切り替わった際に咆哮を止める
                        if (Random.Range(0, 2000) < 5)//ランダムで森の中に戻す
                        {
                            InForest();
                            state = STATE.IDLE; //アイドル状態に遷移  
                        }
                        Debug.Log("WALKがよばれた");
                        float newX = transform.position.x + Random.Range(-10f,10f);
                        float newZ = transform.position.z + Random.Range(-10f,10f);
                        Vector3 NextPos = new Vector3(newX, transform.position.y, newZ);//ゾンビのY軸は変更させない
                        /*配置している木のオブジェクトと同じ座標が選ばれたら再度計算するスクリプトを追加したい
                        if(NextPos == treeObj.transform.position)
                        { 
                          state = STATE.WALK;              
                        }*/
                        agent.SetDestination(NextPos);
                        agent.stoppingDistance = 0; //目的値からオブジェクトの止まる距離の設定、0は目的地と同じポジション
                        AnimStop();
                        agent.speed = walkingSpeed;
                        animator.SetBool("WALK", true);
                        TrexWalkFootStep(WalkStepSE);
                    }

                    if (Random.Range(0, 2500) < 5)//ランダムでIDEL
                    {
                        Debug.Log("IDLEがよばれた");
                        agent.ResetPath();  //目的地のリセット
                        StopStep();
                        state = STATE.IDLE; //アイドル状態に遷移
                    }

                    if (CanSeePlayer())//プレイヤーを発見したらCHASEモード
                    {
                        state = STATE.CHASE;
                    }
                    break;

                case STATE.CHASE:
                    agent.SetDestination(player.transform.position);//プレイヤーの位置を目的地に設定
                    agent.stoppingDistance = 5;
                    AnimStop();
                    agent.speed = runSpeed;
                    animator.SetBool("CHASE", true);
                    TrexBite.Stop();
                    if (agent.remainingDistance <= agent.stoppingDistance)//remainは目的地とagentの距離
                    {
                        state = STATE.ATTACK;
                    }                   
                    if (ForGetPlayer())
                    {
                        StopStep();//走るSEを一旦消す
                        agent.ResetPath();
                        state = STATE.WALK;
                    }
                    break;

                case STATE.ATTACK:
                    AnimStop();
                     animator.SetBool("ATTACK", true);
                     TrexFootStep.Stop();//走るSEを止める
                     if (DistanceToPlayer() > agent.stoppingDistance)//距離が離れたらCHAS
                     {
                         state = STATE.CHASE;
                     }                 
                    break;
                case STATE.DAMEGE:
                    Debug.Log("石があたった");
                    animator.SetBool("DAMEGE", true);
                    StopStep();
                    Invoke("Revival", RevivalTime);
                    break;
            }
        }
        else if(GAMESYS.PlayerHP <= 0 || GAMESYS.GameClearState == true )
        {
            GameStop();
        }
    }

    public void Revival()//STATE.DAMEGEの時に呼ばれる
    {
        state = STATE.WALK;
    }

    public void GameStop()
    {
        AnimStop();
        TrexBite.Stop();
        agent.ResetPath();
    }

    public void RexAttack()//Ataackアニメーションで呼ぶバトル計算
    {
        GAMESYS.Battle();
    }
}
