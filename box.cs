using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour {
    public float speed=0;
    public GameObject a,cr;
    public Transform tr;
    public AudioClip sound;
    public AudioSource bounce=null;
    GameObject UI_button;
    float cubeht;
    float ht = 0;
    float gravity = 0f;
    data scale;
    void Start()//초기 게임 셋팅 및 소리 ON/OFF
    {
        D_Option.Pause = false;
        UI_button = GameObject.Find("Canvas").transform.Find("pause").gameObject;
        scale = GameObject.Find("data_manager").GetComponent<data>();
        cr = GameObject.Find("Main Camera");
        bounce = this.GetComponent<AudioSource>();
        tr=a.GetComponent<Transform>();
        tr.localScale = new Vector3(1, 1, 1);
        StartCoroutine("Wait");
        ht = tr.position.y;
        if (D_Option.Sound == true)
            bounce.mute = false;
        else
            bounce.mute = true;
    }
   public void OnTriggerEnter(Collider coll)//충돌시 벽돌이면 벽돌위치 옮기기
    {
        StartCoroutine("j_wait");
        if (coll.transform.tag=="block"&&cr.GetComponent<cr>().count_barrier==0)
        {
        ht = tr.position.y;
        speed = 0.45f;
            scale.combo = 0;
            D_Option.c_ld = 0;
            bounce.PlayOneShot(sound,0.5f);
        }
    }
    IEnumerator Wait()//게임 시간에 따라 공 낙하 조절
    {
        if(D_Option.Pause==true)
        {
            Die_ani();
        }
        if (speed > -0.38f)
        {
            if (UI_button.GetComponent<UI_Button>().gametime < 30)
                gravity = 0.02f;
            else if(UI_button.GetComponent<UI_Button>().gametime < 60)
                gravity = 0.025f;
            else
                gravity = 0.03f;
            if (cr.GetComponent<cr>().count_barrier < 1)
                speed -= gravity;
            else
                speed -= (gravity) + 0.008f * cr.GetComponent<cr>().count_barrier;
        }
        transform.Translate(0, 0, -speed);
        if (ht > tr.position.y)
            cr.GetComponent<cr>().Movecr();
        if (Input.GetKeyDown(KeyCode.Escape))
            GameObject.Find("Canvas").transform.Find("pause").GetComponent<UI_Button>().T_time();
        yield return new WaitForSeconds(0.02f);
        StartCoroutine("Wait");
    }
    IEnumerator j_wait()
    {
        yield return new WaitForSeconds(0.01f);
    }
    public void Die_ani()//게임 오버시 애니메이션 재생
    {
        GameObject Ani = Resources.Load<GameObject>("prefab/ball_1");
        sound = Resources.Load<AudioClip>("sound/Breaking_Glass_Sound");
        Ani=Instantiate(Ani, this.transform.localPosition, this.transform.rotation);
        Ani.transform.GetComponent<AudioSource>().PlayOneShot(sound,0.25f);
        Destroy(this.gameObject);
    }
}
