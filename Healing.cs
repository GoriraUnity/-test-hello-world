using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
�@//�����Ă���t���[�c�ő̗͂��񕜂���X�N���v�g

   public AudioSource HealingSE;
   public GameObject GAMESYSTEM;//GameSys.sc���C���^���N�g����Ă���I�u�W�F�N�g���i�[
   private GameSys GAMESYS;//GameSys.sc�̕ϐ����g�p���邽�߂̕ϐ�

    private void Start()
    {
        GAMESYS = GAMESYSTEM.GetComponent<GameSys>();//�I�u�W�F�N�g�̃X�N���v�g���擾
    }

    private void OnTriggerEnter(Collider other)//���t�߂̃R���C�_�[�Ƀt���[�c�������Ă����Ƃ��ɉ񕜂���
    {
        if (other.gameObject.tag == "fruits")
        {
            Debug.Log("�t���[�c��H�ׂ�");
            GAMESYS.PlayerHP++;
            if (GAMESYS.PlayerHP > GAMESYS.MaxHP)
            {
                GAMESYS.PlayerHP = GAMESYS.MaxHP;
            }
            HealingSE.Play();
            Destroy(other.gameObject, 0.0f);
        }
    }
}
