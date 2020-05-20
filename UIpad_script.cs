using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIpad_script : MonoBehaviour{

    data color_touch;
    int screen_w = Screen.width;
    int i=0,touchcount=0;
    Vector2 t_d;
    GameObject drag_s,drag_e;
    bool touch;
    void Start()
    {
        color_touch = GameObject.Find("data_manager").GetComponent<data>();
        drag_e = GameObject.Find("Canvas").transform.Find("drag_e").gameObject;
        drag_s = GameObject.Find("Canvas").transform.Find("drag_s").gameObject;
    }
    
	public void OnBeginDrag()//드래그 시작할때 터치한 버튼 색깔 가져오기
    {
        touchcount++;
        if (touch == false) {
            touch = true;
        t_d = Input.mousePosition;
        if (t_d.x>=0&& t_d.x <= screen_w * 0.25f){
            color_touch.color_name[i] = 1;
            drag_s.transform.position = new Vector2(screen_w*0.125f, drag_s.transform.position.y);
        }
        else if (t_d.x > screen_w * 0.25f && t_d.x <= (screen_w * 0.25f) *2){
            color_touch.color_name[i] = 3;
            drag_s.transform.position = new Vector2((screen_w * 0.125f) *3, drag_s.transform.position.y);
        }
        else if (t_d.x > (screen_w * 0.25f) * 2 && t_d.x <= (screen_w * 0.25f) * 3){
            color_touch.color_name[i] = 5;
            drag_s.transform.position = new Vector2((screen_w * 0.125f) *5, drag_s.transform.position.y);
        }
        else if (t_d.x > (screen_w * 0.25f) * 3 && t_d.x <= (screen_w * 0.25f) * 4)
        {
            color_touch.color_name[i] = 7;
            drag_s.transform.position = new Vector2((screen_w / 8) * 7, drag_s.transform.position.y);
        }
        drag_s.SetActive(true);
        if (i > 0)
        {
            GameObject.Find("Main Camera").GetComponent<cr>().Create_br(color_touch.color_name[0] + color_touch.color_name[1]);
            i = 0;
            goto jump;
        }
        if (i < 1)
            i++;
        }
    jump:;
        
    }
    public void OnDrag()//드래그 도중 닿은 버튼 테두리 이미지 표시
    {
t_d = Input.mousePosition;
        if (t_d.x < drag_s.transform.position.x- (screen_w * 0.125f) && t_d.y < 250)
        {
            drag_e.transform.position= new Vector2(drag_s.transform.position.x- screen_w / 4, drag_s.transform.position.y);
        }
        else if(t_d.x > drag_s.transform.position.x + (screen_w * 0.125f) && t_d.y < 250)
        {
            drag_e.transform.position = new Vector2(drag_s.transform.position.x + screen_w / 4, drag_s.transform.position.y);
        }
        else
        {
            drag_e.transform.position = new Vector2(drag_s.transform.position.x, drag_s.transform.position.y);
        }
        drag_e.SetActive(true);
    }
    public void OnEndDrag()//드래그 끝날때 터치한 버튼 색깔 가져오기,(드래그 시작 색깔+드래그 끝남 색깔)/2 색깔 배리어 생성
    {
t_d = Input.mousePosition;
        touchcount--;
        if (i != 0 && touchcount == 0) {
            
            if (t_d.x < drag_s.transform.position.x - (screen_w * 0.125f) && t_d.y < 250)
            {
                color_touch.color_name[i] = color_touch.color_name[0] - 2;
            }
            else if (t_d.x > drag_s.transform.position.x + (screen_w * 0.125f) && t_d.y < 250)
            {
                color_touch.color_name[i] = color_touch.color_name[0] + 2;
            }
            else
            {
                color_touch.color_name[i] = color_touch.color_name[0];
            }
            if(D_Option.c_ld==0)
                     GameObject.Find("Main Camera").GetComponent<cr>().Create_br(color_touch.color_name[0] + color_touch.color_name[1]);
            drag_s.SetActive(false);
            drag_e.SetActive(false);
            color_touch.color_name[1] = 0;
            color_touch.color_name[0] = 0;
            i = 0;
            touch = false;
        }
        
    }


}
