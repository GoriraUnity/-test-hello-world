using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTime : MonoBehaviour
{
    //���Ԑ����Ő΂ƃt���[�c�������X�N���v�g
    [SerializeField] float destroyTime;
    float count;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       count += Time.deltaTime;
        if (count >= destroyTime)
        {
            Destroy(this.gameObject);   
        }
    }
}
