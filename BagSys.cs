using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagSys : MonoBehaviour
{
    //�t���[�c����ẴX�N���v�g

    public AudioSource ItemGetSE;
    public GameSys GAMESYS;//GameSys.sc�̕ϐ����g�p���邽�߂̕ϐ�


    private void OnTriggerEnter(Collider other)
    {
      if (other.gameObject.tag == "fruits")
      {
        Debug.Log("�t���[�c���E����");
       //GameObject.Find("GAMESYSTEM").GetComponent<GameSys>().Count();
        GAMESYS.Count();
        ItemGetSE.Play();
     �@ Destroy(other.gameObject,0.0f);   
      }
    }



    /*Bag�I�u�W�F�N�g(Meshcollider)�ƃt���[�c�I�u�W�F�N�g��Oncorillsion��
      �Փ˔����ݒ肵���s�����Unity��ł͏�������邪�A���ۂ�Meta2�Ńt���[�c��͂��
    �@Bag�ɓ���Ă��������Ȃ������B
    �@OnTrigger�ł���Ώ������ꂽ
     
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "fruits")
        {
            Debug.Log("�t���[�c���E����");
            GameObject.Find("GAMESYSTEM").GetComponent<GameSys>().Count();
            ItemGetSE.Play();
        }

    }*/

}
