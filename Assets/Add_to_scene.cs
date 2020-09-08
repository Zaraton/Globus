using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Add_to_scene : MonoBehaviour
{
    UI_Info_handler Info_Handler;
    public void Add_Search_Result_To_Scene(GameObject Search_Result)
    {
        SatelliteList SList = JsonUtility.FromJson<SatelliteList>(game_state.ReadFromFile("OUT_Active"));
        Info_Handler = GameObject.Find("UI_Info").GetComponent<UI_Info_handler>();
        //Debug.Log("json: " + ReadFromFile("Satellites"));
        //GameObject newObj=new GameObject();
        GameObject sat = null;
        sat = GameObject.Find(Search_Result.transform.GetChild(0).GetComponent<Text>().text+"(Clone)");
        if (sat)
        {
            Info_Handler.Show_Object_Info(sat);
            var rendererComponents = sat.GetComponentsInChildren<MeshRenderer>(true);
            foreach (var component in rendererComponents)
                component.enabled = true;
        }
        else
            foreach (Satellite Sp in SList.SList)
            {

                if (Sp.TLE1 != "null" && Sp.Name == Search_Result.transform.GetChild(0).GetComponent<Text>().text)
                {
                    Debug.Log("Spawning: " + Sp.Name);
                    GameObject pref = Resources.Load(Sp.Model_3D) as GameObject;
                    pref.gameObject.name = Sp.Name;
                    pref.transform.GetComponent<Orbital_movement>().Sat_Name = Sp.Name;
                    pref.transform.GetComponent<Orbital_movement>().tle1 = Sp.TLE1;
                    pref.transform.GetComponent<Orbital_movement>().tle2 = Sp.TLE2;
                    pref.transform.GetComponent<Orbital_movement>().target = GameObject.Find("Earth(Clone)").transform;
                    pref.transform.GetComponent<Show_name>().Information = Sp.Info;
                    GameObject ImageTarget = GameObject.Find("ImageTarget");
                    if (ImageTarget)
                    {
                        GameObject newObj = Instantiate(pref);
                        newObj.transform.SetParent(ImageTarget.transform);
                        var rendererComponents = newObj.GetComponentsInChildren<MeshRenderer>(true);
                        foreach (var component in rendererComponents)
                            component.enabled = true;
                        Info_Handler.Show_Object_Info(newObj);
                    }
                    else //для теста без AR (shit for debug)
                    {
                        Instantiate(pref);
                        // newObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    }
                    break;
                }

            }
    }
}
