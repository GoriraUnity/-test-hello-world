using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSys_Time : MonoBehaviour
{
    //���Ԍo�߂ŃI�u�W�F�N�g�𐶐�����X�N���v�g


    private float time = 0f;//�o�ߎ���
    private float interval;//�������ԊԊu

    [Header("Set Enemy Prefab")] //�w�b�_�[
     public GameObject SpawnObj;

    [Header("Set Interval Min and Max")]//�w�b�_�[
    [Range(1f, 4f)]public float minTime;//�������ԊԊu�̍ŏ��l
    [Range(5f, 10f)]public float maxTime;//�������ԊԊu�̍ő�l
  
    [Header("Set XYZ Position Min and Max")]//�w�b�_�[
    [Range(-24f, 25f)] public float xMinPosi;
    [Range(-24f, 25f)]public float xMaxPosi;
    [Range(-24f, 25f)]public float zMinPosi;
    [Range(-24f, 25f)]public float zMaxPosi;
    [Range(0f,2f)] public float yPosi;

    // Start is called before the first frame update
    void Start()
    {
        interval = GetRandomTime();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime; //���Ԍv��
        if (time > interval)
        { 
          GameObject pickUp = Instantiate(SpawnObj);//enemy�̃C���X�^���X��(��������)
          //pickUp.transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);//�C���X�^���X�̐����ʒu
          pickUp.transform.position = GetRandomPosition();//�C���X�^���X�ʒu�̃����_����
          time = 0f;//�������Ԃ̃��Z�b�g
          interval = GetRandomTime();    
        }
    }

    private float GetRandomTime()
    { 
     return Random.Range(minTime, maxTime);
    }

    private Vector3 GetRandomPosition()//x,z�|�W�V�������������_������Vector3�Ƃ��ēn��
    {
        float x = Random.Range(xMinPosi, xMaxPosi);
        float z = Random.Range(xMinPosi, xMaxPosi);
        return new Vector3(x, yPosi, z);
    }

}
