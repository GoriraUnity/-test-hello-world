using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WatchSys : MonoBehaviour
{
    //�r���v�̃I�u�W�F�N�g�ɐG���ƃt���[�c�̕K�v�����ĕ\��������X�N���v�g
    public GameObject NeedFruitsText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "hand")
        {
            NeedFruitsText.SetActive(true);
            Invoke("comentOut", 3f);
        }
    }

    void comentOut()
    {
        NeedFruitsText.SetActive(false);
    }
}
