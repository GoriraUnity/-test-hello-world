using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnSys : MonoBehaviour
{
    public GameObject[] SpawnPrefab;//�z��A��������I�u�W�F�N�g���i�[����ϐ�
    public int number;//�������鐔
    public float spawnRadius;//�������a
    public bool spawnOnStart;//��������

    // Start is called before the first frame update
    void Start()
    {
        if (spawnOnStart)
        {
            SpawnPoint();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)//�����蔻��ASpawn�|�C���g�̃R���C�_�[�ɐG�ꂽ���ɐ�������
    {
        if (other.tag == "Player")//Spawn�|�C���g�̃R���C�_�[�ɐG�ꂽ�I�u�W�F�N�g�̃^�O��Player�������ꍇ
        {
            SpawnPoint();
        }
    }


    public void SpawnPoint()
    {
        for (int i = 0; i < number; i++)//�������鐔����For�������s����
        {
            Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius; //�����|�C���g��������x�����_��������R�[�h
            int randomIndex = RandomIndex(SpawnPrefab);//�z��Ɋi�[���ꂽ�G(0�n�܂�̔ԍ����t��)���烉���_���Ŕԍ����I�΂��������

            NavMeshHit hit; //Navmesh�̏�(�n�ʂ̏�)�ɂ����Ɛ��������悤�ɂ��邽�߂̃R�[�h�A�z��Ɋ܂߂�I�u�W�F�N�g��NavMesh�t����
            if (NavMesh.SamplePosition(randomPos, out hit, 5.0f, NavMesh.AllAreas))
            {
                Instantiate(SpawnPrefab[randomIndex], randomPos, Quaternion.identity);
                //World���Ɠ�����]���Ő���
            }
            else
            {
                i--;//i����]1���s���A�����񐔂𑝂₷
            }
        }
    }

    //��������I�u�W�F�N�g�������_���Ŕz�񂩂���o���֐��̍쐬
    public int RandomIndex(GameObject[] game)//�����Ƃ��Ĕz���n���AGame�͈����̖��O�Ƃ��Đݒ�ASpawnPoint�Ōďo�����
    {
        return Random.Range(0, game.Length);
        //0����z��Ɋi�[�����I�u�W�F�N�g�̎�ސ��܂ł̊ԂŃ����_���ɑI�΂ꂽ���l���Ԃ����
    }

}
