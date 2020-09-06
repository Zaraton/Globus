using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;
using One_Sgp4;
using UnityEngine.UI;
using Vuforia;

public class game_state : MonoBehaviour
{
    private TrackableBehaviour mTrackableBehaviour;
    public static GameObject ChoosedObject = null;
    public float distanceToCam;
    public static GameObject ImageTarget;
    public static float TimeMultiplier = 0f;
    public static bool IsTracking = true;
    public static float RealEarthRad = 6378.135F; // 3 переменный для соотношения габаритов сцены и реальных
    public static float GameEarthRad = 0.5F; // для корректной работы земной шар должен находиться ровно в 0,0,0 координат; 155.9248; 21.67
    public static float GameToRealEarthCor = 1F; // и северный полюс расположен вдоль мировой оси y
    public static DateTime MultiplierStart = DateTime.UtcNow;
    public static EpochTime nowtime = new EpochTime(DateTime.UtcNow);
    public Text SimulationTime;
    public static GameObject LastTarget=null;

    public static void NewTarget(GameObject Target) { LastTarget = Target; }

    private void InstantiateObjects()
    {
        //Shit for Debug
        ImageTarget = GameObject.Find("ImageTarget");


        //Instantiete The Earth

        GameObject prefab = Resources.Load("Earth") as GameObject;
        if (ImageTarget)
        {
            GameObject newObject = Instantiate(prefab);
            newObject.transform.SetParent(ImageTarget.transform);
            // newObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
        else //для теста без AR
            Instantiate(prefab);



        GameObject earth = GameObject.Find("Earth(Clone)");
        
        


        //Instantiate satellites
        SatelliteList SList = JsonUtility.FromJson<SatelliteList>(ReadFromFile("Satellites"));
        //Debug.Log("json: " + ReadFromFile("Satellites"));
        foreach (Satellite Sp in SList.SList)
        {

            // if (Sp.Model_3D== "GPS-IIF" || Sp.Model_3D == "null")
            if (Sp.TLE1!="null")
            {
                Debug.Log("Spawning: " + Sp.Name);
                prefab = Resources.Load(Sp.Model_3D) as GameObject;
                prefab.gameObject.name = Sp.Name;
                prefab.transform.GetComponent<Orbital_movement>().Sat_Name = Sp.Name;
                prefab.transform.GetComponent<Orbital_movement>().tle1 = Sp.TLE1;
                prefab.transform.GetComponent<Orbital_movement>().tle2 = Sp.TLE2;
                prefab.transform.GetComponent<Orbital_movement>().target = earth.transform;
                prefab.transform.GetComponent<Show_name>().Information = Sp.Info;
                if (ImageTarget)
                {
                    GameObject newObject = Instantiate(prefab);
                    newObject.transform.SetParent(ImageTarget.transform);
                }
                else //для теста без AR (shit for debug)
                {
                    Instantiate(prefab);
                    // newObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                }
            }
        }


        //Instantiate spaceports
        SpaceportList SpList = JsonUtility.FromJson<SpaceportList>(ReadFromFile("SpacePorts"));
        //Debug.Log("json: " + ReadFromFile("SpacePorts"));
        foreach (Spaceport Sp in SpList.SpList)
        {
            Debug.Log("Spawning: " + Sp.Name);
            prefab = Resources.Load("Космодром") as GameObject;
            prefab.gameObject.name = Sp.Name;
            prefab.transform.GetComponent<GeoPoint>().latit = Sp.Latitude;
            prefab.transform.GetComponent<GeoPoint>().longit = Sp.Longitude;
            prefab.transform.GetComponent<GeoPoint>().target = earth.transform;
            prefab.transform.GetComponent<Show_name>().Information = Sp.Info;
            if (ImageTarget)
            {
                GameObject newObject = Instantiate(prefab);
                newObject.transform.SetParent(ImageTarget.transform);
                newObject.transform.localScale = new Vector3(100f, 100f, 100f);
            }
            else //для теста без AR (shit for debug)
            {
                Instantiate(prefab);
                // newObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            }
            /* if (ImageTarget)
            {
                GameObject newObject = Instantiate(prefab, ImageTarget.transform);
                newObject.transform.localScale = new Vector3(100, 100, 100);
            }
            else//для теста без AR (shit for debug)
            {
                GameObject newObject = Instantiate(prefab);
                newObject.transform.localScale = new Vector3(100, 100, 100);
            }*/
        }
    }
    private void Start()
    {
        GameToRealEarthCor = GameEarthRad / RealEarthRad;
        //Get screen size to place choosed object near camera
        Debug.Log((float)Math.Tan(Camera.main.fieldOfView / 2));
        Debug.Log((Camera.main.fieldOfView));
        Camera.main.transform.GetChild(0).transform.position = new Vector3(Camera.main.transform.position.x - ((distanceToCam * (float)Math.Tan(Camera.main.fieldOfView / 2 * Math.PI / 180))), 0, Camera.main.transform.position.z + distanceToCam);

        //Change canvas aspect ratio to screen's
        RectTransform info_text = GameObject.Find("UI_Canvas").GetComponent<RectTransform>();
        info_text.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width);
        info_text.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height);

        Camera.main.transform.GetChild(0).transform.position = new Vector3(Camera.main.transform.position.x - distanceToCam * (float)Math.Tan(Camera.main.fieldOfView/2 * (float)Math.PI / 180), 0, Camera.main.transform.position.z + distanceToCam);
        //Instantiate objects
        InstantiateObjects(); 

        
        //Read the text from directly from the tle.txt file
        /*
        StreamReader reader = new StreamReader("Assets/Resources/tle.txt");
        var fcont=reader.ReadToEnd();
        reader.Close();
        var lines = fcont.Split("\n"[0]);
        
        
        //Read json with sats and add info
        SatelliteList SList = JsonUtility.FromJson<SatelliteList>(ReadFromFile("data_with_nulls2"));
        int counter = 0;
        bool found = false;
        int i = 1;
        foreach (Satellite Sp in SList.SList)
        {
            int counter_for_removal = 0;
            foreach(char b in Sp.Name)
            {
                if (b != '(')
                    counter_for_removal = counter_for_removal + 1;
                else
                    break;
               
            }
            if (counter_for_removal<Sp.Name.Length)
                Sp.Official_Name=Sp.Name.Remove(counter_for_removal-1);

            Debug.Log("Spawning: " + Sp.Name);
            //Debug.Log("line: " + lines[1]);
            //Debug.Log("line_norad: " + lines[1].Substring(2, 5).Replace(" ", ""));
            
            i = 1;
            found = false;
            while(i<lines.Length && !found)
            {
                 Debug.Log("line: " + lines[i]);
                 if (Sp.NORAD_Number== lines[i].Substring(2, 5).Replace(" ", ""))//названия совпадают
                 {
                     Sp.TLE1 = lines[i];
                     Sp.TLE2 = lines[i + 1];
                     counter = counter+1;
                     found = true;
                 }
                i = i + 3;
            }
            
            // Debug.Log("TLE1: " + Sp.TLE1);
            // Debug.Log("Norad: " + Sp.NORAD_Number);

        }
        string path = Application.dataPath + "/Resources/data_with_nulls3.json";
        Debug.Log("AssetPath:" + path);
        Debug.Log(JsonUtility.ToJson(SList));
        File.WriteAllText(path, JsonUtility.ToJson(SList));*/
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif


    }
     private void Update()
     {
         //World simulation timer
         nowtime = new EpochTime((DateTime.UtcNow.AddSeconds((DateTime.UtcNow - game_state.MultiplierStart).TotalSeconds * game_state.TimeMultiplier)).ToLocalTime());
         //Debug.Log(nowtime);
         DateTime runtimeKnowsThisIsUtc = DateTime.SpecifyKind(nowtime.toDateTime(), DateTimeKind.Utc);
         DateTime localVersion = runtimeKnowsThisIsUtc.ToLocalTime();
         SimulationTime.text = localVersion.ToString();

     }



    public static void WriteDataToFile(string jsonString)
    {
        string path = Application.dataPath + "/SpacePorts.json";
        Debug.Log("AssetPath:" + path);
        File.WriteAllText(path, jsonString);
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }



    public static string ReadFromFile(string filename)
    {
        TextAsset file = Resources.Load(filename) as TextAsset; 
        return file.ToString();
    }

    public void ChangeMultiplier(float speed)
    {
        if (TimeMultiplier == 0f)
        {
            MultiplierStart = DateTime.UtcNow;
        }
        if (TimeMultiplier == speed)
        {
            TimeMultiplier = 0f;
        }
        else
            TimeMultiplier = speed;
    }
}
