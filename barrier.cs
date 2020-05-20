using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barrier : MonoBehaviour
{
    public int count = 0;
    public int ct_num;
    data scale;
    cr noob;

    void Start()
    {
        this.GetComponent<Transform>().localScale = new Vector3(110, 25, 110);
        this.transform.parent.transform.parent = GameObject.Find("Cube").transform;
        scale = GameObject.Find("data_manager").GetComponent<data>();
        noob = GameObject.Find("Main Camera").GetComponent<cr>();
        StartCoroutine("Mk_cl");
    }//방어막 기본 세팅
    
    IEnumerator Mk_cl()//이 스크립트의 방어막이 가장 바깥에 있을땐 콜리더 활성화
    {
        yield return new WaitForSeconds(0.01f);
        if (noob.count_barrier - 1 == count)
        {
            IEnumerator stop = Mk_cl();
            this.GetComponent<BoxCollider>().enabled = true;
            StopCoroutine(stop);
        }
        StartCoroutine("Mk_cl");
    }
    public void Change_scale()//보호막 중첩시 보호막 크기 조절
    {
        this.GetComponent<Transform>().localScale = scale.br_scael[count];
        if (count==0)
            transform.Translate(0.05f, 0, -0.75f);
        else
            transform.Translate(0.05f, 0, -0.93f);
        
        count++;
    }

}
