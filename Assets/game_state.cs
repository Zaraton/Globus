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

    private void InstantiateObjects()
    {
        GameObject earth = GameObject.Find("Earth");
        ImageTarget = GameObject.Find("ImageTarget");

        //Instantiate satellites
        List<string> sats = new List<string> { "METEOR-M1", "1 35865U 09049A   19271.04830571  .00000016  00000-0  25990-4 0  9993", "2 35865  98.4186 264.6244 0001402 215.9225 144.1859 14.22208579520476", "null", "null", "METEOR-M2", "1 40069U 14037A   19271.06744460 -.00000031  00000-0  51912-5 0  9993", "2 40069  98.5416 313.0536 0005326 336.2328  23.8611 14.20664853270816", "null", "null", "ISS 1", "1 25544U 98067A   19257.23636851 -.00000464  00000-0  00000+0 0  9992", "2 25544  51.6440 279.5905 0008750  38.3150  36.5419 15.50445690189071", "null", "null", "NAVSTAR 77 (USA 289)", "1 43873U 18109A   19305.97611391 -.00000006  00000-0  00000+0 0  9995", "2 43873  54.9968 187.7362 0004587 306.8511  53.1961  2.00567376  6502", "GPS-IIF", "null", "NAVSTAR 76 (USA-266)", "1 41328U 16007A   19297.28145056 +.00000017 +00000-0 +00000-0 0  9999", "2 41328 054.8171 185.8499 0033830 218.3397 141.4899 02.00567096027190", "GPS-IIF", "null", "NAVSTAR 75 (USA-265)", "1 41019U 15062A   19296.69210010  .00000081  00000-0  00000+0 0  9993", "2 41019  55.2317 126.1583 0050605 205.1854 154.6349  2.00564534 29122", "GPS-IIF", "null", "NAVSTAR 74 (USA-262)", "1 40730U 15033A   19297.14353066 -.00000076 +00000-0 +00000-0 0  9996", "2 40730 055.5923 005.4541 0046337 343.5724 016.3277 02.00575433031318", "GPS-IIF", "null", "NAVSTAR 73 (USA 260)", "1 40534U 15013A   19295.98422684 -.00000000  00000-0  00000+0 0  9995", "2 40534  54.4123 304.6775 0040443   3.5356 356.5270  2.00561785 33532", "GPS-IIF", "null", "NAVSTAR 72 (USA 258)", "1 40294U 14068A   19297.34538700 +.00000082 +00000-0 +00000-0 0  9996", "2 40294 055.2327 126.3132 0024319 040.7684 319.4826 02.00561950036517", "GPS-IIF", "null", "NAVSTAR 71 (USA 256)", "1 40105U 14045A   19296.97075800  .00000015  00000-0  00000+0 0  9992", "2 40105  54.5763 185.4385 0013245  96.4836 263.7374  2.00574762 38261", "GPS-IIF", "null", "NAVSTAR 70 (USA 251)", "1 39741U 14026A   19296.88076699 -.00000019  00000-0  00000+0 0  9997", "2 39741  55.9870  66.1749 0016778 288.6151  71.1988  2.00562805 39816", "GPS-IIF", "null", "NAVSTAR 69 (USA 248)", "1 39533U 14008A   19297.10269288 -.00000019  00000-0  00000+0 0  9990", "2 39533  53.8534 248.5233 0042349 196.7843 163.0584  2.00555394 41533", "GPS-IIF", "null", "NAVSTAR 68 (USA 242)", "1 39166U 13023A   19297.10136134 -.00000076  00000-0  00000+0 0  9994", "2 39166  56.0703   6.1742 0071494  27.4041 333.0198  2.00566087 47171", "GPS-IIF", "null", "NAVSTAR 67 (USA 239)", "1 38833U 12053A   19297.24537482 -.00000022  00000-0  00000+0 0  9993", "2 38833  53.7276 243.1078 0087861  33.9675 326.5750  2.00563097 51630", "GPS-IIF", "null", "NAVSTAR 66 (USA 232)", "1 37753U 11036A   19297.25758728 -.00000021  00000-0  00000+0 0  9991", "2 37753  56.0091  66.6338 0089757  42.8832 319.4998  2.00567159 60579", "GPS-IIF", "null", "NAVSTAR 65 (USA 213)", "1 36585U 10022A   19297.87405248 -.00000013  00000-0  00000+0 0  9992", "2 36585  55.4879 306.3949 0083092  49.9421 308.5486  2.00567145 68909", "GPS-IIF", "null", "NAVSTAR 64 (USA 206)", "1 35752U 09043A   19297.52515646  .00000079  00000-0  00000+0 0  9995", "2 35752  54.4813 124.6497 0057487  44.5578 315.9030  2.00550199 74649", "GPS-IIF", "null", "NAVSTAR 62 (USA 201)", "1 32711U 08012A   19297.55863690 -.00000022 +00000-0 +00000-0 0  9990", "2 32711 054.6898 247.0186 0133405 221.8321 137.1232 02.00557218085089", "GPS-IIF", "null", "NAVSTAR 61 (USA 199)", "1 32384U 07062A   19297.70235562 -.00000079 +00000-0 +00000-0 0  9990", "2 32384 056.4837 010.0729 0009198 124.5295 048.0150 02.00563664086867", "GPS-IIF", "null", "NAVSTAR 60 (USA 196)", "1 32260U 07047A   19297.64645461  .00000026  00000-0  00000+0 0  9992", "2 32260  53.1730 180.6057 0117350  46.6403 314.4111  2.00573149 88148", "GPS-IIF", "null", "NAVSTAR 59 (USA 192)", "1 29601U 06052A   19297.33006856 -.00000008 +00000-0 +00000-0 0  9999", "2 29601 056.2113 310.2093 0072837 062.4088 298.3710 02.00576713094713", "GPS-IIF", "null", "NAVSTAR 58 (USA 190)", "1 29486U 06042A   19297.91356288 -.00000024 +00000-0 +00000-0 0  9998", "2 29486 054.9475 247.8449 0094681 001.3929 018.1525 02.00558082095753", "GPS-IIF", "null", "NAVSTAR 57 (USA 183)", "1 28874U 05038A   19297.77675905 -.00000079 +00000-0 +00000-0 0  9996", "2 28874 056.3955 009.4178 0135167 262.9654 094.2419 02.00573923103149", "GPS-IIF", "null", "NAVSTAR 56 (USA 180)", "1 28474U 04045A   19297.86530404 -.00000038 +00000-0 +00000-0 0  9998", "2 28474 054.8109 062.4942 0195265 262.2810 053.8721 02.00576668109747", "GPS-IIF", "null", "SIRIUSSAT-1 (SXC1-181)", "1 43595U 98067PG  19297.76289908 +.00016989 +00000-0 +18100-3 0  9999", "2 43595 051.6355 057.5809 0003042 242.1498 117.9189 15.64098836067869", "Cubesat 1u", "null", "SIRIUSSAT-2 (SXC1-182)", "1 43596U 98067PH  19297.87054907  .00021399  00000-0  22784-3 0  9995", "2 43596  51.6366  57.5993 0003498 241.4371 118.6272 15.63934802 67876", "Cubesat 1u", "null" };
        for (int i = 0; i < sats.Count; i = i + 5)
        {
            if (sats[i + 3] == "GPS-IIF")
            {
                GameObject prefab = Resources.Load(sats[i + 3]) as GameObject;
                prefab.gameObject.name = sats[i];
                prefab.transform.GetComponent<Orbital_movement>().Sat_Name = sats[i];
                prefab.transform.GetComponent<Orbital_movement>().tle1 = sats[i + 1];
                prefab.transform.GetComponent<Orbital_movement>().tle2 = sats[i + 2];
                prefab.transform.GetComponent<Orbital_movement>().target = earth.transform;
                prefab.transform.GetComponent<Show_name>().Information = sats[i + 4];
                if (ImageTarget)
                    Instantiate(prefab, ImageTarget.transform);
                else //для теста без AR
                {
                    GameObject newObject = Instantiate(prefab);
                    // newObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                }
            }
        }


        //Instantiate spaceports
        SpaceportList SpList = JsonUtility.FromJson<SpaceportList>(ReadSpacePortsFromFile("SpacePorts"));
        Debug.Log("json: " + ReadSpacePortsFromFile("SpacePorts"));
        foreach (Spaceport Sp in SpList.SpList)
        {
            Debug.Log("Spawning: " + Sp.Name);
            GameObject prefab = Resources.Load("Космодром") as GameObject;
            prefab.gameObject.name = Sp.Name;
            prefab.transform.GetComponent<GeoPoint>().latit = Sp.Latitude;
            prefab.transform.GetComponent<GeoPoint>().longit = Sp.Longitude;
            prefab.transform.GetComponent<GeoPoint>().target = earth.transform;
            prefab.transform.GetComponent<Show_name>().Information = Sp.Info;
            if (ImageTarget)
            {
                GameObject newObject = Instantiate(prefab, ImageTarget.transform);
                newObject.transform.localScale = new Vector3(100, 100, 100);
            }
            else//для теста без AR
            {
                GameObject newObject = Instantiate(prefab);
                newObject.transform.localScale = new Vector3(100, 100, 100);
            }
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

        //Camera.main.transform.GetChild(0).transform.position = new Vector3(Camera.main.transform.position.x - distanceToCam * (float)Math.Tan(Camera.main.fieldOfView/2 * (float)Math.PI / 180), 0, Camera.main.transform.position.z + distanceToCam);
        //Instantiate objects
        InstantiateObjects(); 
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
    public static string ReadSpacePortsFromFile(string filename)
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
