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

    public GameObject Search_Result_Content;

    public Satellite Sat_To_Add;

    // Start is called before the first frame update
    void Start()
    {
        Wrapper.SetActive(false);
        Search.SetActive(false);
    }

    // Update is called once per frame
    public void Show_Object_Info(GameObject Selected_Object)//ПЕРЕДЕЛАТЬ ПОД НОВУЮ СТРУКТУРУ ДЛЯ ВЫВОДА, НЕ ЗАБЫТЬ ИСПРАВИТЬ В Show_Name
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
    public void Search_Button_Behaviour()
    {
        if (Search.activeSelf != true) 
        {
            Start_Search();
        }
        else { Close_Search(); }

    }
    private void Start_Search()//ПЕРЕДЕЛАТЬ ПОД НОВУЮ СТРУКТУРУ ДЛЯ ВЫВОДА, НЕ ЗАБЫТЬ ИСПРАВИТЬ В Show_Name
    {
        Search.SetActive(true);
    }
    private void Close_Search()
    {
        Clear_Search();
        Search.SetActive(false);
    }
    private void Clear_Search()
    {
        int children = Search_Result_Content.transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            Transform child = Search_Result_Content.transform.GetChild(i);
            if (child.name != "Info_Text")
                Destroy(child.gameObject);

        }
    }
    public void Find_Object(InputField Input)
    {
        Debug.Log(Input.text);
        SatelliteList SList = JsonUtility.FromJson<SatelliteList>(game_state.ReadFromFile("OUT_Active"));
        //Debug.Log("json: " + ReadFromFile("Satellites"));
        Clear_Search();
        if (Input.text!="")
        {
            foreach (Satellite Sp in SList.SList)
            {
                if (Sp.Name.ToLower().Contains(Input.text.ToLower())&&Sp.TLE1!="null")
                {
                    // game_state.
                    Add_Search_Result(Sp);
                    /*
                    Spaceport info_struct = new Spaceport("asdasd", "", "", 0, 0, "asdasdasd"); // TODO: Поменять это просто на "взять поле у объекта" Артемий: учти что у спутника другая структура и больше параметров
                    this.Show_Object_Info(GameObject.Find(Sp.Name + "(Clone)"));
                    break;
                    */
                }
            }
        }

    }
    private void Add_Search_Result(Satellite Sp)
    {
        
        GameObject prefab = Resources.Load("Search_Result") as GameObject;
        prefab.transform.GetChild(0).GetComponent<Text>().text = Sp.Name;
        GameObject newObject = Instantiate(prefab);
        newObject.transform.SetParent(Search_Result_Content.transform);
        newObject.transform.localScale = new Vector3(1f, 12.58164f, 1f);
        Debug.Log(Sp.Name);
        /*prefab.gameObject.name = Sp.Name;
        prefab.transform.GetComponent<Orbital_movement>().Sat_Name = Sp.Name;
        prefab.transform.GetComponent<Orbital_movement>().tle1 = Sp.TLE1;
        prefab.transform.GetComponent<Orbital_movement>().tle2 = Sp.TLE2;
        prefab.transform.GetComponent<Orbital_movement>().target = earth.transform;
        prefab.transform.GetComponent<Show_name>().Information = Sp.Info;*/
    }
    
}
