using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Info_handler : MonoBehaviour
{
    public GameObject Wrapper;
    public Text Info_Name;
    public Text Info_Text;

    public Image Info_Image;

    // Start is called before the first frame update
    void Start()
    {
        Wrapper.SetActive(false);
    }

    // Update is called once per frame
    public void Show_Object_Info(Spaceport Info_Object, GameObject Selected_Object)
    {
        Wrapper.SetActive(true);
        game_state.ChoosedObject = Selected_Object;

        // Parse text data
        Info_Name.text = Info_Object.Name;
        Info_Text.text = Info_Object.Info;
        //TODO: place info sprites here
        //Info_Image.sprite = Info_Object.Sprite;
    }

    public void Close_Object_Info()
    {
        Wrapper.SetActive(false);
        if(game_state.ImageTarget)
            game_state.ChoosedObject.transform.SetParent(game_state.ImageTarget.transform);
        else //для теста без AR
            game_state.ChoosedObject.transform.parent = null;
        game_state.ChoosedObject = null;
        
    }
}
