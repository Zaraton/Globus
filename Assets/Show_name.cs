using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using System.IO;

public class Show_name : MonoBehaviour
{
    private string text;
    public int textSize = 14;
    public Font textFont;
    public Color textColor = Color.white;
    public float textHeight =250f;
    public bool showShadow = true;
    public Color shadowColor = new Color(0, 0, 0, 0.5f);
    public Vector2 shadowOffset = new Vector2(1, 1);
    private string textShadow;
    public bool ObjectChoosed = false;
   // public string path;
   // private Show_name showname;
   // private MeshRenderer MeshRen;
    public string Information;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        if (cam == null)
            cam = Camera.main;
        text = gameObject.name.Replace("(Clone)", string.Empty);
    }
  
    // Update is called once per frame
    void Update()
    {
    }
    void OnGUI() //НАЗВАНИЕ спутника на экране
    {
        if ((transform.gameObject == game_state.ChoosedObject) || (!game_state.ChoosedObject))
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
    void OnMouseDown()
    {
        if (!game_state.textDisplayed)
        {
            // this object was clicked - do something
            GameObject.Find("Close_Button").GetComponent<Image>().enabled = true;
            Text info_text = GameObject.Find("Text_Canvas").GetComponent<Text>();
            info_text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            info_text.text = Information;
            info_text.alignment = TextAnchor.MiddleCenter;
            //Text_window.transform.localPosition = new Vector3(0, 0, 0);
            info_text.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width);
            info_text.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height);
            
            game_state.ChoosedObject = transform.gameObject;
            game_state.textDisplayed = true;
        }

    }
}
