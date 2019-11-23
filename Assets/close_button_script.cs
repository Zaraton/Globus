using UnityEngine;

public class close_button_script : MonoBehaviour
{ 
    UI_Info_handler Info_Handler;

    void Start()
    {
        Info_Handler = GameObject.Find("UI_Info").GetComponent<UI_Info_handler>();
    }
    public void Close_Info()
    {
        // this object was clicked - do something
        Info_Handler.Close_Object_Info();
    }
}
