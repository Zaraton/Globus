﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Info_handler : MonoBehaviour
{
    public GameObject Wrapper;
    public Text Info_Name;
    public Text Info_Text;
    // Start is called before the first frame update
    void Start()
    {
        Close_Object_Info();
    }

    // Update is called once per frame
    public void Show_Object_Info(Spaceport Info_Object, GameObject Selected_Object)
    {
        Wrapper.SetActive(true);
        game_state.textDisplayed = true;
        game_state.ChoosedObject = Selected_Object;

        // Parse text data
        Info_Name.text = Info_Object.Name;
        Info_Text.text = Info_Object.Info;
    }

    public void Close_Object_Info()
    {
        Wrapper.SetActive(false);
        game_state.textDisplayed = false;

        game_state.ChoosedObject.transform.SetParent(GameObject.Find("ImageTarget").transform);
        game_state.ChoosedObject = null;
        
    }
}