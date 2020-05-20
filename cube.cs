using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cube : MonoBehaviour
{
    data data_tr;
    cr noob;
    score Tt;
    int loading;
    int idx;
    int m_d;
    bool timedata;
    AudioSource play;
    GameObject ani;
    void Start()//벽돌 초기 셋팅
    {
        data_tr = GameObject.Find("data_manager").GetComponent<data>();
        noob = GameObject.Find("Main Camera").GetComponent<cr>();
        Tt = GameObject.Find("Canvas").transform.Find("Text").GetComponent<score>();
        ani = Resources.Load<GameObject>("prefab/Ani");
        play = this.GetComponent<AudioSource>();
        m_d = D_Option.Mode_data;
        if (D_Option.Mode_data == 1)
        {
            idx = Random.Range(0, ((int)(data_tr.cl_material.Length * 0.5f)) + 1);
            GetComponent<Renderer>().material = data_tr.cl_material[idx * 2];
        }
        else
        {
            GetComponent<Renderer>().material = data_tr.cl_material[noob.Casual_br_cl()];
        }
        if (D_Option.Sound == true)
            play.mute = false;
        else
            play.mute = true;
    }
    void OnTriggerEnter(Collider coll)//방어막과 충돌시 일정 거리 내려가기
    {
        if (coll.transform.tag == "barrier"&&D_Option.Pause==false)
        {

            if ( this.GetComponent<MeshRenderer>().material.name== coll.transform.GetComponent<MeshRenderer>().material.name&&noob.count_barrier==coll.transform.GetComponent<barrier>().count+1)
            {
                if (D_Option.c_ld == 0)
                {
                D_Option.c_ld = 1;
                Material ma_l = GetComponent<Renderer>().material;
                Instantiate(ani, this.transform.position, this.transform.rotation).GetComponent<MeshRenderer>().material= ma_l;
                this.transform.Translate(0, -24, 0);
                Destroy(coll.transform.parent.gameObject);
                noob.count_barrier--;
                    timedata = GameObject.Find("Canvas").transform.Find("pause").GetComponent<UI_Button>().sw_cl;
                    if (m_d == 0) {
                        int num = noob.Casual_br_cl();
                        if(data_tr.combo > 5) { 
                            data_tr.combo = 0;
                        }
                        play.PlayOneShot(data_tr.bounce[data_tr.combo], 0.5f);
                        if (num > -1 && num < 10)
                            GetComponent<Renderer>().material = data_tr.cl_material[num];
                        else
                        {
                            
                            StartCoroutine("Disappear");
                        }
                        data_tr.score++;
                    }
                    else { 
                    if (timedata == true) {
                        idx = Random.Range(0, (int)(data_tr.cl_material.Length * 0.5f));
                        GetComponent<Renderer>().material = data_tr.cl_material[idx*2+1];
                        GameObject.Find("Canvas").transform.Find("pause").GetComponent<UI_Button>().sw_cl = false;
                    }
                    else {
                        idx = Random.Range(0,(int)(data_tr.cl_material.Length * 0.5f)+1);
                        GetComponent<Renderer>().material = data_tr.cl_material[idx * 2];
                    }
                        if (data_tr.combo > 5)
                        {
                            data_tr.combo = 0;
                        }
                        play.PlayOneShot(data_tr.bounce[data_tr.combo], 0.5f);
                        data_tr.score += (data_tr.combo+1) * 50;
                    }
                    Tt.Dis_sc(data_tr.score);
                    if (m_d == 0 && data_tr.score >= (D_Option.Casual_num * 5 + 10))
                    {
                        GameObject.Find("Canvas").transform.Find("pause").GetComponent<UI_Button>().GameClear();
                        
                    }
                    data_tr.combo++;
                    D_Option.c_ld = 0;
                }
            }
            else
            {
                GameObject.Find("Canvas").transform.Find("pause").GetComponent<UI_Button>().GameOver();
            }

        }
    }

    IEnumerator Disappear()//사라지기
    {
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
    }
}
