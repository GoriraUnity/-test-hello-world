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
      forestPoint = forestObj.transform.position;//一定確立でフィールドの中心に呼出すための変数、Inforest関数で使う
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
        Vector3 NextPosi = new Vector3(forestPoint.x, 0, forestPoint.z);
        agent.speed = walkingSpeed;   
        agent.SetDestination(NextPosi);
        agent.stoppingDistance = 0; 
        animator.SetBool("WALK", true);
        TrexWalkFootStep(WalkStepSE);      
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "stone")
        {
            stoneAttack.Play();
            AnimStop();//Animationが切替わるタイミングで呼出し、対象のアニメーションを呼出す
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
                    Debug.Log("IDLEがよばれた");
                    if (CanSeePlayer())
                    {
                        state = STATE.CHASE;
                    }
                    else if (Random.Range(0, 250) < 1)
                    {
                        agent.ResetPath();  //目的値のリセット
                        state = STATE.WALK;
                    }
                    break;

                case STATE.WALK:
                    if (!agent.hasPath)//.hasPathはNevMesh関数の目的地を持っているかどうか確認する関数
                                       //目的地(SetDestinationで設定)が有ればTrueが返ってくる               
                    {
                        AnimStop();
                        TrexHowl.Stop(); //IDELから切り替わった際に咆哮を止める
                        animator.SetBool("WALK", true);
                        TrexWalkFootStep(WalkStepSE);
                        Debug.Log("WALKがよばれた");
                        float newX = transform.position.x + Random.Range(-10f,10f);
                        float newZ = transform.position.z + Random.Range(-10f,10f);
                        Vector3 NextPos = new Vector3(newX, transform.position.y, newZ);
                        /*配置している木のオブジェクトと同じ座標が選ばれたら再度計算するスクリプトを追加したい
                        if(NextPos == treeObj.transform.position)
                        { 
                          state = STATE.WALK;              
                        }*/
                        agent.SetDestination(NextPos);
                        agent.stoppingDistance = 0; //目的値からオブジェクトの止まる距離の設定、0は目的地と同じポジション       
                        agent.speed = walkingSpeed;
                    }

                    if (Random.Range(0, 2500) < 1)//ランダムで森の中に戻す
                    {
                        Debug.Log("Inforest");
                        InForest();
                    }


                    if (CanSeePlayer())//プレイヤーを発見したらCHASEモード
                    {
                        AnimStop();
                        state = STATE.CHASE;
                    }
                    break;

                case STATE.CHASE:
                    animator.SetBool("CHASE", true);
                    agent.SetDestination(player.transform.position);//プレイヤーの位置を目的地に設定
                    agent.stoppingDistance = 5;
                    agent.speed = runSpeed;
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
                     //TrexFootStep.Stop();//走るSEを止める
                     if (DistanceToPlayer() > agent.stoppingDistance)//距離が離れたらCHAS
                     {
                         state = STATE.CHASE;
                     }                 
                    break;

                case STATE.DAMEGE:
                    Debug.Log("石があたった");
                    agent.SetDestination(new Vector3(transform.position.x, transform.position.y, transform.position.z));
                    agent.stoppingDistance = 0;
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
        state = STATE.IDLE;
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
