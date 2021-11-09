using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;
using One_Sgp4;
using UnityEngine.UI;
using Vuforia;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;

public class game_state : MonoBehaviour
{
    private TrackableBehaviour mTrackableBehaviour;
    public static GameObject ChoosedObject = null;
    public float distanceToCam;
    public static GameObject ImageTarget;
    public static float TimeMultiplier = 0f;
    public static bool IsTracking = true;
    public static float RealEarthRad = 6378.135F; // 3 переменный для соотношения габаритов сцены и реальных
    public static float GameEarthRad = 6.0246F / 2; // для корректной работы земной шар должен находиться ровно в 0,0,0 координат; 155.9248; 21.67
    public static float GameToRealEarthCor = 1F; // и северный полюс расположен вдоль мировой оси y
    public static DateTime MultiplierStart = DateTime.UtcNow;
    public static EpochTime nowtime = new EpochTime(DateTime.UtcNow);
    public Text SimulationTime;
    public static GameObject LastTarget = null;
    public static GameObject earth = null;
    public static bool All_Added = false;
    public static string json = "OUT_Active";//"OUT_Active";


    public static void NewTarget(GameObject Target)
    {
        LastTarget = ImageTarget;
        ImageTarget = Target;
    }
    public static void Instantiate_Spaceport(Spaceport Sp)
    {
        Debug.Log("Spawning: " + Sp.Name);
        GameObject prefab = Resources.Load("Космодром") as GameObject;
        prefab.gameObject.name = Sp.Name;
        prefab.transform.GetComponent<GeoPoint>().latit = Sp.Latitude;
        prefab.transform.GetComponent<GeoPoint>().longit = Sp.Longitude;
        prefab.transform.GetComponent<GeoPoint>().target = earth.transform;
        prefab.transform.GetComponent<Show_name>().Information = Sp.Info;
        prefab.transform.GetComponent<Show_name>().Is_On_Scene = true;
        prefab.transform.GetComponent<Show_name>().Is_Showing_Name = false;
        if (ImageTarget)
        {
            GameObject newObject = Instantiate(prefab);
            newObject.transform.SetParent(ImageTarget.transform);
            newObject.transform.localScale = new Vector3(GameToRealEarthCor / 20f, GameToRealEarthCor / 20f, GameToRealEarthCor / 20f);
            foreach (Transform child in newObject.transform)
                child.GetComponent<MeshRenderer>().enabled = true;
        }
        else //для теста без AR (shit for debug)
        {
            Instantiate(prefab);
            // newObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
    }
    public static GameObject Instantiate_Satellite_Web_Scraped(Satellite Sp)
    {
        // if (Sp.Model_3D== "GPS-IIF" || Sp.Model_3D == "null")
        GameObject newObject = null;
        GameObject check = GameObject.Find(Sp.name + "(Clone)");
        if (Sp.TLE1 != "null" && check == null)
        {
            Debug.Log("Spawning: " + Sp.name);
            GameObject prefab = Resources.Load(Sp.Model_3D) as GameObject;
            prefab.gameObject.name = Sp.name;
            prefab.transform.GetComponent<Orbital_movement>().Sat_Name = Sp.name;
            prefab.transform.GetComponent<Orbital_movement>().tle1 = Sp.TLE1;
            prefab.transform.GetComponent<Orbital_movement>().tle2 = Sp.TLE2;
            prefab.transform.GetComponent<Orbital_movement>().target = earth.transform;
            prefab.transform.GetComponent<Show_name>().Information = "TLE1: " + Sp.TLE1 + "\n" + "TLE2: " + Sp.TLE2 + "\n";
            prefab.transform.GetComponent<Show_name>().Information += "Аргумент перицентра в градусах: " + Sp.argument_of_perigee + "\n";
            prefab.transform.GetComponent<Show_name>().Information += "Средняя аномалия: " + Sp.mean_anomaly + "\n";
            prefab.transform.GetComponent<Show_name>().Information += "Частота обращения (оборотов в день): " + Sp.mean_motion + "\n";
            prefab.transform.GetComponent<Show_name>().Information += "Номер витка на момент эпохи: " + Sp.revolution_number + "\n";
            prefab.transform.GetComponent<Show_name>().Information += "Эксцентриситет: " + Sp.eccentricity + "\n";
            prefab.transform.GetComponent<Show_name>().Information += "Наклонение в градусах: " + Sp.inclination_in_degrees + "\n";
            /*Debug.Log("0." + Sp.date_of_launch);
            if(float.Parse(("0." + Sp.date_of_launch), CultureInfo.InvariantCulture.NumberFormat)*100 < 22)
                prefab.transform.GetComponent<Show_name>().Information += "Год запуска: 19" + Sp.date_of_launch + "\n";
            else
                prefab.transform.GetComponent<Show_name>().Information += "Год запуска: 20" + Sp.date_of_launch + "\n";*/
            if (Sp.launch_number_of_the_year != "null" && null != Sp.launch_number_of_the_year)
                prefab.transform.GetComponent<Show_name>().Information += "Номер запуска в году: " + Sp.launch_number_of_the_year + "\n";
            if (Sp.launch_piece != "null" && null != Sp.launch_piece)
                prefab.transform.GetComponent<Show_name>().Information += "Международное обозначение номера запуска в году: " + Sp.launch_piece + "\n";
            if (Sp.epoch_year != "null" && null != Sp.epoch_year)
                prefab.transform.GetComponent<Show_name>().Information += "Год эпохи: " + Sp.epoch_year + "\n";
            if (Sp.epoch_day != "null" && null != Sp.epoch_day)
                prefab.transform.GetComponent<Show_name>().Information += "День эпохи:" + Sp.epoch_day + "\n";
            if (Sp.ballistic_coefficient != "null" && null != Sp.ballistic_coefficient)
                prefab.transform.GetComponent<Show_name>().Information += "Баллистический коэффициент: " + Sp.ballistic_coefficient + "\n";
            if (Sp.second_derivative_of_mean_motion != "null" && null != Sp.second_derivative_of_mean_motion)
                prefab.transform.GetComponent<Show_name>().Information += "Вторая производная от среднего движения, делённая на шесть: " + Sp.second_derivative_of_mean_motion + "\n";
            if (Sp.drag_term != "null" && null != Sp.drag_term)
                prefab.transform.GetComponent<Show_name>().Information += "Коэффициент торможения: " + Sp.drag_term + "\n";
            if (Sp.element_set_number != "null" && null != Sp.element_set_number)
                prefab.transform.GetComponent<Show_name>().Information += "Номер (версия) элемента: " + Sp.element_set_number + "\n";
            if (Sp.COSPAR_number != "null" && null != Sp.COSPAR_number)
                prefab.transform.GetComponent<Show_name>().Information += "Номер в каталоге COSPAR: " + Sp.COSPAR_number + "\n";
            if (Sp.NORAD_number != "null" && null != Sp.NORAD_number)
                prefab.transform.GetComponent<Show_name>().Information += "Номер в каталоге NORAD: " + Sp.NORAD_number + "\n";
            if (Sp.apogee_in_km != "null" && null != Sp.apogee_in_km)
                prefab.transform.GetComponent<Show_name>().Information += "Апогей: " + Sp.apogee_in_km + "\n";
            if (Sp.inclination != "null" && null != Sp.inclination)
                prefab.transform.GetComponent<Show_name>().Information += "Наклонение: " + Sp.inclination + "\n";
            if (Sp.period != "null" && null != Sp.period)
                prefab.transform.GetComponent<Show_name>().Information += "Период вращения в минутах: " + Sp.period + "\n";
            if (Sp.launch_site != "null" && null != Sp.launch_site)
                prefab.transform.GetComponent<Show_name>().Information += "Место запуска: " + Sp.launch_site + "\n";
            if (Sp.launch_vehicle != "null" && null != Sp.launch_vehicle)
                prefab.transform.GetComponent<Show_name>().Information += "Запущено на: " + Sp.launch_vehicle + "\n";
            if (Sp.country_or_org_of_UN_registry != "null" && null != Sp.country_or_org_of_UN_registry)
                prefab.transform.GetComponent<Show_name>().Information += "Зарегестрирован: " + Sp.country_or_org_of_UN_registry + "\n";
            if (Sp.country_of_operator_or_owner != "null" && null != Sp.country_of_operator_or_owner)
                prefab.transform.GetComponent<Show_name>().Information += "Страна владельца/оператора: " + Sp.country_of_operator_or_owner + "\n";
            if (Sp.operator_or_owner != "null" && null != Sp.operator_or_owner)
                prefab.transform.GetComponent<Show_name>().Information += "Оператор/владелец: " + Sp.operator_or_owner + "\n";
            if (Sp.users != "null" && null != Sp.users)
                prefab.transform.GetComponent<Show_name>().Information += "Пользователи: " + Sp.users + "\n";
            if (Sp.purpose != "null" && null != Sp.purpose)
                prefab.transform.GetComponent<Show_name>().Information += "Назначение: " + Sp.purpose + "\n";
            if (Sp.class_of_orbit != "null" && null != Sp.class_of_orbit)
                prefab.transform.GetComponent<Show_name>().Information += "Класс орбиты: " + Sp.class_of_orbit + "\n";
            if (Sp.type_of_orbit != "null" && null != Sp.type_of_orbit)
                prefab.transform.GetComponent<Show_name>().Information += "Тип орбиты: " + Sp.type_of_orbit + "\n";
            if (Sp.class_of_orbit == "GEO")
                prefab.transform.GetComponent<Show_name>().Information += "Широта геостационарной орбиты: " + Sp.longitude_of_GEO_in_degrees + "\n";
            //prefab.transform.GetComponent<Show_name>().Information += "Период в минутах: " + Sp.period_in_minutes + "\n";
            if (Sp.launch_mass_in_kg != "null" && null != Sp.launch_mass_in_kg)
                prefab.transform.GetComponent<Show_name>().Information += "Масса при запуске в кг: " + Sp.launch_mass_in_kg + "\n";
            if (Sp.expected_lifetime_in_years != "null" && null != Sp.expected_lifetime_in_years)
                prefab.transform.GetComponent<Show_name>().Information += "Ожидаемая продолжительность жизни: " + Sp.expected_lifetime_in_years + "\n";
            if (Sp.contractor != "null" && null != Sp.contractor)
                prefab.transform.GetComponent<Show_name>().Information += "Заказчик: " + Sp.contractor + "\n";
            if (Sp.country_of_contractor != "null" && null != Sp.country_of_contractor)
                prefab.transform.GetComponent<Show_name>().Information += "Страна заказчика: " + Sp.country_of_contractor + "\n";
            if (Sp.info != "null" && null != Sp.info)
                prefab.transform.GetComponent<Show_name>().Information += "Справочная информация: " + Sp.info + "\n";
            //prefab.transform.GetComponent<Show_name>().Information += "Источники информации: " + Sp.links.Replace(' ', '\n') + "\n";


            prefab.transform.GetComponent<Orbital_movement>().target = earth.transform;
            //prefab.transform.GetComponent<Show_name>().Source1 = Sp.Source1;
            prefab.transform.GetComponent<Show_name>().Is_On_Scene = false;
            prefab.transform.GetComponent<Show_name>().Is_Showing_Name = false;
            if (ImageTarget)
            {
                newObject = Instantiate(prefab);
                newObject.transform.SetParent(ImageTarget.transform);
                // foreach (Transform child in newObject.transform)
                //     child.GetComponent<MeshRenderer>().enabled = false;
            }
            else //для теста без AR (shit for debug)
            {
                Instantiate(prefab);
                // newObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            }
        }
        Debug.Log(newObject);
        return newObject;
    }
    public static GameObject Instantiate_Satellite(Satellite Sp)
    {
        // if (Sp.Model_3D== "GPS-IIF" || Sp.Model_3D == "null")
        GameObject newObject = null;
        GameObject check = GameObject.Find(Sp.name + "(Clone)");
        if (Sp.TLE1 != "null" && check == null)
        {
            Debug.Log("Spawning: " + Sp.name);
            GameObject prefab = Resources.Load(Sp.Model_3D) as GameObject;
            prefab.gameObject.name = Sp.name;
            prefab.transform.GetComponent<Orbital_movement>().Sat_Name = Sp.name;
            prefab.transform.GetComponent<Orbital_movement>().tle1 = Sp.TLE1;
            prefab.transform.GetComponent<Orbital_movement>().tle2 = Sp.TLE2;
            prefab.transform.GetComponent<Orbital_movement>().target = earth.transform;
            prefab.transform.GetComponent<Show_name>().Information = Sp.info;
            //prefab.transform.GetComponent<Show_name>().Source1 = Sp.Source1;
            prefab.transform.GetComponent<Show_name>().Is_On_Scene = false;
            prefab.transform.GetComponent<Show_name>().Is_Showing_Name = false;
            if (ImageTarget)
            {
                newObject = Instantiate(prefab);
                newObject.transform.SetParent(ImageTarget.transform);
                newObject.transform.localScale = new Vector3(GameToRealEarthCor * 2 / 3000f, GameToRealEarthCor * 2 / 3000f, GameToRealEarthCor * 2 / 3000f);
            }
            else //для теста без AR (shit for debug)
            {
                Instantiate(prefab);
                // newObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            }
        }
        return newObject;
    }
    public void Instantiate_All_Objects() //OBSOLET
    {
        //Instantiate satellites
        SatelliteList SList = JsonUtility.FromJson<SatelliteList>(ReadFromFile(json));
        //Debug.Log("json: " + ReadFromFile("Satellites"));
        if (!All_Added)
        {
            foreach (Satellite Sp in SList.SList)
            {
                Instantiate_Satellite_Web_Scraped(Sp);
                //Instantiate_Satellite(Sp);
            }
            All_Added = true;
        }
        else
        {
            foreach (Transform child in ImageTarget.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            All_Added = false;
            InstantiateObjects();
        }
    }
    public static void InstantiateEarth(bool IsVisible)
    {
        //Instantiete The Earth
        GameObject prefab;
        if (IsVisible)
            prefab = Resources.Load("Earth") as GameObject;
        else
            prefab = Resources.Load("Earth_no_renderer") as GameObject;
        if (ImageTarget)
        {
            earth = Instantiate(prefab);
            earth.transform.localScale = new Vector3(GameEarthRad * 2, GameEarthRad * 2, GameEarthRad * 2);
            earth.transform.SetParent(ImageTarget.transform);
        }
        else //для теста без AR
            earth = Instantiate(prefab);
    }
    public static void InstantiateObjects()
    {
        //Shit for Debug
        
        int SatsAmmount = 0;

        SatelliteList SList = JsonUtility.FromJson<SatelliteList>(ReadFromFile( "OUT_Active"));
        Debug.Log("json: " +SList);
        foreach (Satellite Sp in SList.SList)
        {
            if ((SatsAmmount < 100))
            {
                if (Sp.Model_3D != "null")
                {
                    //GameObject sat = Instantiate_Satellite_Web_Scraped(Sp);
                    GameObject sat = Instantiate_Satellite_Web_Scraped(Sp);

                    if (sat)
                        sat.transform.GetComponent<Show_name>().Is_On_Scene = true;
                    //  
                    SatsAmmount++;
                }

            }
            else
                break;
        }    

        //Instantiate spaceports
        SpaceportList SpList = JsonUtility.FromJson<SpaceportList>(ReadFromFile("SpacePorts"));
        //Debug.Log("json: " + ReadFromFile("SpacePorts"));
        Debug.Log("json: " + SpList);
        foreach (Spaceport Sp in SpList.SpList)
        {
            Instantiate_Spaceport(Sp);
        }
    }
    private void Start()
    {
        GameToRealEarthCor = GameEarthRad / RealEarthRad;
        //Get screen size to place choosed object near camera
        Debug.Log((float)Math.Tan(Camera.main.fieldOfView / 2));
        Debug.Log((Camera.main.fieldOfView));
        //Camera.main.transform.GetChild(0).transform.position = new Vector3(Camera.main.transform.position.x - ((distanceToCam * (float)Math.Tan(Camera.main.fieldOfView / 2 * Math.PI / 180))), 0, Camera.main.transform.position.z + distanceToCam);

        //Change canvas aspect ratio to screen's
        RectTransform info_text = GameObject.Find("UI_Canvas").GetComponent<RectTransform>();
        info_text.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width);
        info_text.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height);

        //Camera.main.transform.GetChild(0).transform.position = new Vector3(Camera.main.transform.position.x - distanceToCam * (float)Math.Tan(Camera.main.fieldOfView/2 * (float)Math.PI / 180), 0, Camera.main.transform.position.z + distanceToCam);

        ImageTarget = GameObject.Find("ImageTarget");

        //Instantiate objects
        InstantiateEarth(true);
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
            foreach(char b in Sp.name)
            {
                if (b != '(')
                    counter_for_removal = counter_for_removal + 1;
                else
                    break;
               
            }
            if (counter_for_removal<Sp.name.Length)
                Sp.Official_name=Sp.name.Remove(counter_for_removal-1);

            Debug.Log("Spawning: " + Sp.name);
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
        Debug.Log(filename);
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
