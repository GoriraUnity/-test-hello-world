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
    public GameObject GAMESYSTEM;//GameSys.sc���C���^���N�g����Ă���I�u�W�F�N�g���擾
    private GameSys GAMESYS;//GameSys.sc�̕ϐ����g�p���邽�߂̕ϐ�
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
      GAMESYS = GAMESYSTEM.GetComponent<GameSys>();//GameSys.sc���擾�A�ȍ~�̃R�[�h��sc�̕ϐ����g�p����
      forestPoint = forestObj.transform.position;//���m���Ńt�B�[���h�̒��S�Ɍďo�����߂̕ϐ��AInforest�֐��Ŏg��
    }

    public void AnimStop()
    {
        animator.SetBool("WALK", false);
        animator.SetBool("ATTACK", false);
        animator.SetBool("CHASE", false);
        animator.SetBool("DAMEGE", false);
    }

    float DistanceToPlayer()//�v���C���[�Ƃ̋������v�Z����X�N���v�g
    {     
        return Vector3.Distance(player.transform.position, transform.position);
    }

    bool CanSeePlayer()//DistanceToPlayer�̋��������ɁA�v���C���[�𔭌�����̊֐����쐬
    {
        if (DistanceToPlayer() < 15)
        {
            return true;
        }
        return false;
    }

    bool ForGetPlayer()//DistanceToPlayer�̋��������ɁA�v���C���[�������������̊֐����쐬
    {
        if (DistanceToPlayer() > 20)
        {
            return true;
        }
        return false;
    }

    void InForest()//���m���ŐX�̒��Ɉړ�������
    {
        Debug.Log("�X�̒�����΂ꂽ");
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
            AnimStop();//Animation���ؑւ��^�C�~���O�Ōďo���A�Ώۂ̃A�j���[�V�������ďo��
            StopStep();
            Destroy(collision.gameObject,0.0f);    
            state = STATE.DAMEGE;  
        }
    }

    public void TrexWalkFootStep(AudioClip clip)//���������o��
    {
        TrexFootStep.loop = true;//�A���\�b�h�����s������Loop���L���ɂȂ�
        TrexFootStep.pitch = 1f;�@//�A���̍����𒲐�����
        TrexFootStep.clip = clip; //�����̐ݒ�
        TrexFootStep.Play(); //�����̍Đ�
    }

    public void TrexRunFootStep(AudioClip clip)//���鉹���o��
    {
        TrexFootStep.loop = true;//�A���\�b�h�����s������Loop���L���ɂȂ�
        TrexFootStep.pitch = 1.5f;//���̍����𒲐�����
        TrexFootStep.clip = clip; //�����̐ݒ�
        TrexFootStep.Play(); //�����̍Đ�
    }

    public void StopStep()//�����~�߂�
    {
        TrexFootStep.Stop();
        TrexFootStep.loop = false;
        TrexFootStep.pitch = 1f;
    }

    public void HowlSE(AudioClip clip)//�i���鉹
    {
        TrexHowl.pitch = 1.0f;
        TrexHowl.clip = clip;
        TrexHowl.Play();
    }

    public void HowlLowSE(AudioClip clip)//�i���鉹2
    {
        TrexHowl.pitch = 1.0f;
        TrexHowl.clip = clip;
        TrexHowl.Play();
    }

    public void BiteDrySE(AudioClip clip)//���������݂�����
    {
        TrexBite.pitch = 1.0f;
        TrexBite.clip = clip;
        TrexBite.Play();
    }

    public void BiteWetSE(AudioClip clip)//���������݂�����
    {
        TrexBite.pitch = 1.0f;
        TrexBite.clip = clip; 
        TrexBite.Play();
    }


    // Update is called once per frame
    void Update()
    {
        if (GAMESYS.PlayerHP > 0 && GAMESYS.GameClearState == false)//�v���C���[��HP���c���Ă��銎�Q�[���N���A���ĂȂ��󋵂ŋ����͓���
        {
            switch (state)
            {
                case STATE.IDLE:
                    Debug.Log("IDLE����΂ꂽ");
                    if (CanSeePlayer())
                    {
                        state = STATE.CHASE;
                    }
                    else if (Random.Range(0, 250) < 1)
                    {
                        agent.ResetPath();  //�ړI�l�̃��Z�b�g
                        state = STATE.WALK;
                    }
                    break;

                case STATE.WALK:
                    if (!agent.hasPath)//.hasPath��NevMesh�֐��̖ړI�n�������Ă��邩�ǂ����m�F����֐�
                                       //�ړI�n(SetDestination�Őݒ�)���L���True���Ԃ��Ă���               
                    {
                        AnimStop();
                        TrexHowl.Stop(); //IDEL����؂�ւ�����ۂə��K���~�߂�
                        animator.SetBool("WALK", true);
                        TrexWalkFootStep(WalkStepSE);
                        Debug.Log("WALK����΂ꂽ");
                        float newX = transform.position.x + Random.Range(-10f,10f);
                        float newZ = transform.position.z + Random.Range(-10f,10f);
                        Vector3 NextPos = new Vector3(newX, transform.position.y, newZ);
                        /*�z�u���Ă���؂̃I�u�W�F�N�g�Ɠ������W���I�΂ꂽ��ēx�v�Z����X�N���v�g��ǉ�������
                        if(NextPos == treeObj.transform.position)
                        { 
                          state = STATE.WALK;              
                        }*/
                        agent.SetDestination(NextPos);
                        agent.stoppingDistance = 0; //�ړI�l����I�u�W�F�N�g�̎~�܂鋗���̐ݒ�A0�͖ړI�n�Ɠ����|�W�V����       
                        agent.speed = walkingSpeed;
                    }

                    if (Random.Range(0, 2500) < 1)//�����_���ŐX�̒��ɖ߂�
                    {
                        Debug.Log("Inforest");
                        InForest();
                    }


                    if (CanSeePlayer())//�v���C���[�𔭌�������CHASE���[�h
                    {
                        AnimStop();
                        state = STATE.CHASE;
                    }
                    break;

                case STATE.CHASE:
                    animator.SetBool("CHASE", true);
                    agent.SetDestination(player.transform.position);//�v���C���[�̈ʒu��ړI�n�ɐݒ�
                    agent.stoppingDistance = 5;
                    agent.speed = runSpeed;
                    TrexBite.Stop();
                    if (agent.remainingDistance <= agent.stoppingDistance)//remain�͖ړI�n��agent�̋���
                    {                     
                        state = STATE.ATTACK;
                    }                   
                    if (ForGetPlayer())
                    {
                        StopStep();//����SE����U����
                        agent.ResetPath();
                        state = STATE.WALK;
                    }
                    break;

                case STATE.ATTACK:
                     AnimStop();
                     animator.SetBool("ATTACK", true);
                     //TrexFootStep.Stop();//����SE���~�߂�
                     if (DistanceToPlayer() > agent.stoppingDistance)//���������ꂽ��CHAS
                     {
                         state = STATE.CHASE;
                     }                 
                    break;

                case STATE.DAMEGE:
                    Debug.Log("�΂���������");
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

    public void Revival()//STATE.DAMEGE�̎��ɌĂ΂��
    {
        state = STATE.IDLE;
    }

    public void GameStop()
    {
        AnimStop();
        TrexBite.Stop();
        agent.ResetPath();
    }

    public void RexAttack()//Ataack�A�j���[�V�����ŌĂԃo�g���v�Z
    {
        GAMESYS.Battle();
    }
}
