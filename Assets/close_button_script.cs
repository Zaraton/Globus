using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using System.IO;

public class close_button_script : MonoBehaviour
{
    public string path;
    private Show_name showname;
    private MeshRenderer MeshRen;
   
    // Start is called before the first frame update
    void Start()
    {
        GameObject button;
        button = GameObject.Find("Close_Button");
        Image ImageScript = button.GetComponent<Image>();
        ImageScript.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Close_Info()
    {
        // this object was clicked - do something

        game_state.textDisplayed = false;
        
        Debug.Log("clicked_button");
        GameObject button;
        button = GameObject.Find("Close_Button");
        Image ImageScript = button.GetComponent<Image>();
        ImageScript.enabled = false;
        game_state.ChoosedObject.transform.SetParent(GameObject.Find("ImageTarget").transform);
        game_state.ChoosedObject = null;

        GameObject window;
        window = GameObject.Find("Text_Canvas");
        Text info_text = window.GetComponent<Text>();
        info_text.text = "";
    }
}
