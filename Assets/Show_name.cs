﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using System.IO;

public class Show_name : MonoBehaviour
{
    UI_Info_handler Info_Handler;
    private string text;
    public int textSize = 14;
    public Font textFont;
    public Color textColor = Color.white;
    public float textHeight =250f*game_state.GameToRealEarthCor;
    public bool showShadow = true;
    public Color shadowColor = new Color(0, 0, 0, 0.5f);
    public Vector2 shadowOffset = new Vector2(1, 1);
    private string textShadow;
   // public string path;
   // private Show_name showname;
   // private MeshRenderer MeshRen;
    public string Information;
    public string Source1;
    Camera cam;
    public bool Is_On_Scene;
    public bool Is_Showing_Name = false;

    // Start is called before the first frame update
    void Start()
    {
        textHeight = 250f * game_state.GameToRealEarthCor;
        if (cam == null)
            cam = Camera.main;
        text = gameObject.name.Replace("(Clone)", string.Empty);

        Info_Handler = GameObject.Find("UI_Info").GetComponent<UI_Info_handler>();
    }
  
    // Update is called once per frame
    void Update()
    {
    }
    void OnGUI() //НАЗВАНИЕ спутника на экране
    {
        if (Is_Showing_Name && (!game_state.ChoosedObject) && game_state.LastTarget!=null && Info_Handler)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = textSize;
            style.richText = true;
            if (textFont) style.font = textFont;
            style.normal.textColor = textColor;
            style.alignment = TextAnchor.MiddleCenter;

            GUIStyle shadow = new GUIStyle();
            shadow.fontSize = textSize;
            shadow.richText = true;
            if (textFont) shadow.font = textFont;
            shadow.normal.textColor = shadowColor;
            shadow.alignment = TextAnchor.MiddleCenter;

            Vector3 worldPosition = new Vector3(transform.position.x, transform.position.y + textHeight, transform.position.z);
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
            screenPosition.y = Screen.height - screenPosition.y;

            if (showShadow) GUI.Label(new Rect(screenPosition.x + shadowOffset.x, screenPosition.y + shadowOffset.y, 0, 0), textShadow, shadow);
            GUI.Label(new Rect(screenPosition.x, screenPosition.y, 0, 0), text, style);
        }
    }

    void OnBecameVisible()
    {
        enabled = true;
    }

    void OnBecameInvisible()
    {
        enabled = false;
    }
    void OnMouseDown()//ПЕРЕДЕЛАТЬ ПОД НОВУЮ СТРУКТУРУ ДЛЯ ВЫВОДА
    {
        if (!game_state.ChoosedObject)
        {
            Debug.Log("My name is jfef oh soryy " + text);
            //Spaceport info_struct = new Spaceport(text, "", "", 0, 0, Information); // TODO: Поменять это просто на "взять поле у объекта" Артемий: учти что у спутника другая структура и больше параметров
            Info_Handler.Show_Object_Info(this.gameObject);
            Animator anim = this.gameObject.GetComponent<Animator>();
            if (anim)
            {
                anim.SetTrigger("ClickTrigger");
            }
            // this object was clicked - do something
            /*Text info_text = GameObject.Find("Text_Canvas").GetComponent<Text>();
            info_text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            info_text.text = Information;
            info_text.alignment = TextAnchor.MiddleCenter;
            //Text_window.transform.localPosition = new Vector3(0, 0, 0);
            info_text.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width);
            info_text.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height);*/
        }

    }
}
