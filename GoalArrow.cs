using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class GoalArrow : MonoBehaviour
{
    //�Q�[���N���A�ɕK�v�ȃt���[�c���W�߂���ɃS�[���̏ꏊ���w������\������

    public GameObject GoalPoint,ArrowOBJ;
    Vector3 ArrowPoint;
    private GameObject GAMESYSTEM;//GameSys.sc�̕ϐ����g�p���邽�߂̕ϐ�
    private GameSys GAMESYS;      //GameSys.sc�̕ϐ����g�p���邽�߂̕ϐ�
    MeshRenderer ArrowMesh;�@�@ �@//�X�^�[�g�����b�V�������_���[���������߂̕ϐ�


    // Start is called before the first frame update
    void Start()
    {
      ArrowMesh = ArrowOBJ.GetComponent<MeshRenderer>();
      ArrowMesh.enabled = false;    
      GAMESYSTEM = GameObject.Find("GAMESYSTEM");
      GAMESYS = GAMESYSTEM.GetComponent<GameSys>();//�I�u�W�F�N�g�̃X�N���v�g���擾
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
