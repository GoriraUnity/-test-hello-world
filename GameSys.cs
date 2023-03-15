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
�@�@/*�Q�[���V�X�e�����ꊇ�ŋL�ڂ��Ă���B
   �@ �v���C���[HP�⋰���̍U���́A�N���A�E�Q�[���I�[�o�[�����A�o�g���V�X�e��(�v�Z)
  �@�@�R�����g�\����\���Ȃ�*/


    [Header("BattlePoint")]
    [Range(1,10)] public int MaxHP;
    [Range(1, 10)] public int TrexAttack;

    [System.NonSerialized]
    public int PlayerHP;//Moving.sc�̍s������Ɏg�p���邽��poublic���AInspector����s��

    [Header("CleareCountRandom")]//�Q�[���N���A�ɕK�v�ȃt���[�c���������_����
    [Range(1, 10)] public int minClearCount;
    [Range(1, 10)] public int maxClearCount;

    [System.NonSerialized]
    public int  GetFruits,ClearCount; //GoalArrpw.sc�́��o������Ɏg�p���邽��poublic���AInspector����s�@�@
    [System.NonSerialized]
    public bool GameClearState = false;//Moving.sc�̍s������Ɏg�p���邽��poublic���AInspector����s��
    
    public GameObject GameClearText, GameOverText,GoHomeText,NeedFruitsText;//�\�������e�L�X�g�̕ϐ��ꗗ
    public TextMeshProUGUI FruitsCount, NeedFruits;//�t���[�c�̎擾��(TMP)��ω������邽�߂̕ϐ�
    public AudioSource GameOverMusic,ClearMusic;

    [SerializeField]//�_���[�W�ɂ���ĉ�ʂ��Ԃ����܂邽�߂̕ϐ�
    private Volume GV;//Hierarchy����GlobalVolume���i�[����ϐ�
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
      GV.profile.TryGet<Vignette>(out vignette);//GlobalVolume����Vigentte���擾�A�ϐ�vignette��Vignette���o�͂���
    }

    // Update is called once per frame
    void Update()
    {
      BattleDamege(); 
    }

    void BattleDamege()//�_���[�W�ʂɂ���ĉ�ʂ��Ԃ��Ȃ�
    {
        if (PlayerHP > 3)
        {
            vignette.intensity.value = 0.0f; //�r�l�b�g���ʂ̎��s
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

    public void ComentOut()//�R�����g���X�^�[�g���ɏ���
    {
        GameClearText.SetActive(false);
        GameOverText.SetActive(false);
        GoHomeText.SetActive(false);
    }

    public void ComentOut2()//���ԍ��ŃN���A����������
    {
        NeedFruitsText.SetActive(false);
    }

    public void Count()//Bag.sc�Ōďo�����Z�X�N���v�g
    {
        GetFruits++;
        Debug.Log("���Z����"+ GetFruits);
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
            //����Ȃ�����\������
            Debug.Log("����Ȃ�");
        }
    }

    public void Gameclear()
    {
        if (GetFruits >= ClearCount)//�N���A�����𖞂������ꍇ
        {
            GameClearText.SetActive(true);
            GameClearState = true;
            ClearMusic.Play();
            Invoke("Restart", 3f);
        }
        else
        {
            //����Ȃ�����\������
            Debug.Log("����Ȃ�");
        }
    }

    public void GameoverJudge()
    {
    �@ if(PlayerHP <= 0)
     �@{�@
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
