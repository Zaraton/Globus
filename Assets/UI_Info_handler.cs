using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Info_handler : MonoBehaviour
{
    public GameObject Wrapper;
    public GameObject Search;
    public Text Info_Name;
    public Text Info_Text;

    public Image Info_Image;

    // Start is called before the first frame update
    void Start()
    {
        Wrapper.SetActive(false);
        Search.SetActive(false);
    }

    // Update is called once per frame
    public void Show_Object_Info(Spaceport Info_Object, GameObject Selected_Object)//ПЕРЕДЕЛАТЬ ПОД НОВУЮ СТРУКТУРУ ДЛЯ ВЫВОДА, НЕ ЗАБЫТЬ ИСПРАВИТЬ В Show_Name
    {
        Wrapper.SetActive(true);
        game_state.ChoosedObject = Selected_Object;

        // Parse text data
        Info_Name.text = Selected_Object.name.Replace("(Clone)","");//Info_Object.Name;
        Info_Text.text = Selected_Object.GetComponent<Show_name>().Information;//Info_Object.Info;
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
    public void Seatch_Button_Behaviour()
    {
        if (Search.activeSelf != true) 
        {
            Start_Search();
        }
        else { Close_Search(); }

    }
    public void Start_Search()//ПЕРЕДЕЛАТЬ ПОД НОВУЮ СТРУКТУРУ ДЛЯ ВЫВОДА, НЕ ЗАБЫТЬ ИСПРАВИТЬ В Show_Name
    {
        Search.SetActive(true);
    }
    public void Close_Search()
    {
        Search.SetActive(false);
        //ПОЧИСТИТЬ ОКНО ВВОДА

    }
    public void Find_Object(InputField Input)
    {
        Debug.Log(Input.text);
        SatelliteList SList = JsonUtility.FromJson<SatelliteList>(game_state.ReadFromFile("Satellites"));
        //Debug.Log("json: " + ReadFromFile("Satellites"));
        foreach (Satellite Sp in SList.SList)
        {
            if(Sp.Name.ToLower().Contains(Input.text.ToLower()))
            {
                // game_state.
                
                Spaceport info_struct = new Spaceport("asdasd", "", "", 0, 0, "asdasdasd"); // TODO: Поменять это просто на "взять поле у объекта" Артемий: учти что у спутника другая структура и больше параметров
                this.Show_Object_Info(info_struct, GameObject.Find(Sp.Name+ "(Clone)"));
                break;
            }
        }
    }
}
