using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

[Serializable]
public class SpaceportList
{
   public List<Spaceport> SpList;
   public SpaceportList(List<Spaceport> list)
   {
       this.SpList = list;
   }
}
[Serializable]
public class Spaceport
{
    public string Name;
    public string Country;
    public string LocationName;
    public double Latitude;
    public double Longitude;
    public string Info;

    public Spaceport(string name, string country, string locationName, double latitude,double longitude,string info)
    {
        this.Name = name;
        this.Country = country;
        this.LocationName = locationName;
        this.Latitude = latitude;
        this.Longitude = longitude;
        this.Info = info;
    }
}




public class game_state : MonoBehaviour
{
    public static bool textDisplayed = false;
    public static GameObject ChoosedObject = null;
    public static float distanceToCam = 3000f;
    private void Start()
    {
        //Get screen size to place choosed object near camera
        Camera.main.transform.GetChild(0).transform.position = new Vector3(Camera.main.transform.position.x - Screen.width/2, 0, Camera.main.transform.position.z + distanceToCam);
        //Camera.main.transform.GetChild(0).transform.position = new Vector3(Camera.main.transform.position.x - distanceToCam * (float)Math.Tan(Camera.main.fieldOfView/2 * (float)Math.PI / 180), 0, Camera.main.transform.position.z + distanceToCam);
        //Instantiate objects
        GameObject earth = GameObject.Find("EarthHigh");
        GameObject target = GameObject.Find("ImageTarget");
        //Instantiate satellites
        List<string> sats = new List<string> { "METEOR-M1", "1 35865U 09049A   19271.04830571  .00000016  00000-0  25990-4 0  9993", "2 35865  98.4186 264.6244 0001402 215.9225 144.1859 14.22208579520476", "null", "null", "METEOR-M2", "1 40069U 14037A   19271.06744460 -.00000031  00000-0  51912-5 0  9993", "2 40069  98.5416 313.0536 0005326 336.2328  23.8611 14.20664853270816", "null", "null", "ISS 1", "1 25544U 98067A   19257.23636851 -.00000464  00000-0  00000+0 0  9992", "2 25544  51.6440 279.5905 0008750  38.3150  36.5419 15.50445690189071", "null", "null", "NAVSTAR 77 (USA 289)", "1 43873U 18109A   19305.97611391 -.00000006  00000-0  00000+0 0  9995", "2 43873  54.9968 187.7362 0004587 306.8511  53.1961  2.00567376  6502", "GPS-IIF", "null", "NAVSTAR 76 (USA-266)", "1 41328U 16007A   19297.28145056 +.00000017 +00000-0 +00000-0 0  9999", "2 41328 054.8171 185.8499 0033830 218.3397 141.4899 02.00567096027190", "GPS-IIF", "null", "NAVSTAR 75 (USA-265)", "1 41019U 15062A   19296.69210010  .00000081  00000-0  00000+0 0  9993", "2 41019  55.2317 126.1583 0050605 205.1854 154.6349  2.00564534 29122", "GPS-IIF", "null", "NAVSTAR 74 (USA-262)", "1 40730U 15033A   19297.14353066 -.00000076 +00000-0 +00000-0 0  9996", "2 40730 055.5923 005.4541 0046337 343.5724 016.3277 02.00575433031318", "GPS-IIF", "null", "NAVSTAR 73 (USA 260)", "1 40534U 15013A   19295.98422684 -.00000000  00000-0  00000+0 0  9995", "2 40534  54.4123 304.6775 0040443   3.5356 356.5270  2.00561785 33532", "GPS-IIF", "null", "NAVSTAR 72 (USA 258)", "1 40294U 14068A   19297.34538700 +.00000082 +00000-0 +00000-0 0  9996", "2 40294 055.2327 126.3132 0024319 040.7684 319.4826 02.00561950036517", "GPS-IIF", "null", "NAVSTAR 71 (USA 256)", "1 40105U 14045A   19296.97075800  .00000015  00000-0  00000+0 0  9992", "2 40105  54.5763 185.4385 0013245  96.4836 263.7374  2.00574762 38261", "GPS-IIF", "null", "NAVSTAR 70 (USA 251)", "1 39741U 14026A   19296.88076699 -.00000019  00000-0  00000+0 0  9997", "2 39741  55.9870  66.1749 0016778 288.6151  71.1988  2.00562805 39816", "GPS-IIF", "null", "NAVSTAR 69 (USA 248)", "1 39533U 14008A   19297.10269288 -.00000019  00000-0  00000+0 0  9990", "2 39533  53.8534 248.5233 0042349 196.7843 163.0584  2.00555394 41533", "GPS-IIF", "null", "NAVSTAR 68 (USA 242)", "1 39166U 13023A   19297.10136134 -.00000076  00000-0  00000+0 0  9994", "2 39166  56.0703   6.1742 0071494  27.4041 333.0198  2.00566087 47171", "GPS-IIF", "null", "NAVSTAR 67 (USA 239)", "1 38833U 12053A   19297.24537482 -.00000022  00000-0  00000+0 0  9993", "2 38833  53.7276 243.1078 0087861  33.9675 326.5750  2.00563097 51630", "GPS-IIF", "null", "NAVSTAR 66 (USA 232)", "1 37753U 11036A   19297.25758728 -.00000021  00000-0  00000+0 0  9991", "2 37753  56.0091  66.6338 0089757  42.8832 319.4998  2.00567159 60579", "GPS-IIF", "null", "NAVSTAR 65 (USA 213)", "1 36585U 10022A   19297.87405248 -.00000013  00000-0  00000+0 0  9992", "2 36585  55.4879 306.3949 0083092  49.9421 308.5486  2.00567145 68909", "GPS-IIF", "null", "NAVSTAR 64 (USA 206)", "1 35752U 09043A   19297.52515646  .00000079  00000-0  00000+0 0  9995", "2 35752  54.4813 124.6497 0057487  44.5578 315.9030  2.00550199 74649", "GPS-IIF", "null", "NAVSTAR 62 (USA 201)", "1 32711U 08012A   19297.55863690 -.00000022 +00000-0 +00000-0 0  9990", "2 32711 054.6898 247.0186 0133405 221.8321 137.1232 02.00557218085089", "GPS-IIF", "null", "NAVSTAR 61 (USA 199)", "1 32384U 07062A   19297.70235562 -.00000079 +00000-0 +00000-0 0  9990", "2 32384 056.4837 010.0729 0009198 124.5295 048.0150 02.00563664086867", "GPS-IIF", "null", "NAVSTAR 60 (USA 196)", "1 32260U 07047A   19297.64645461  .00000026  00000-0  00000+0 0  9992", "2 32260  53.1730 180.6057 0117350  46.6403 314.4111  2.00573149 88148", "GPS-IIF", "null", "NAVSTAR 59 (USA 192)", "1 29601U 06052A   19297.33006856 -.00000008 +00000-0 +00000-0 0  9999", "2 29601 056.2113 310.2093 0072837 062.4088 298.3710 02.00576713094713", "GPS-IIF", "null", "NAVSTAR 58 (USA 190)", "1 29486U 06042A   19297.91356288 -.00000024 +00000-0 +00000-0 0  9998", "2 29486 054.9475 247.8449 0094681 001.3929 018.1525 02.00558082095753", "GPS-IIF", "null", "NAVSTAR 57 (USA 183)", "1 28874U 05038A   19297.77675905 -.00000079 +00000-0 +00000-0 0  9996", "2 28874 056.3955 009.4178 0135167 262.9654 094.2419 02.00573923103149", "GPS-IIF", "null", "NAVSTAR 56 (USA 180)", "1 28474U 04045A   19297.86530404 -.00000038 +00000-0 +00000-0 0  9998", "2 28474 054.8109 062.4942 0195265 262.2810 053.8721 02.00576668109747", "GPS-IIF", "null", "SIRIUSSAT-1 (SXC1-181)", "1 43595U 98067PG  19297.76289908 +.00016989 +00000-0 +18100-3 0  9999", "2 43595 051.6355 057.5809 0003042 242.1498 117.9189 15.64098836067869", "Cubesat 1u", "null", "SIRIUSSAT-2 (SXC1-182)", "1 43596U 98067PH  19297.87054907  .00021399  00000-0  22784-3 0  9995", "2 43596  51.6366  57.5993 0003498 241.4371 118.6272 15.63934802 67876", "Cubesat 1u", "null"};
        for(int i = 0; i <sats.Count; i=i+5)
        {
            if (sats[i+3] == "GPS-IIF")
            {
                GameObject prefab = Resources.Load(sats[i + 3]) as GameObject;
                prefab.gameObject.name = sats[i];
                prefab.transform.GetComponent<Orbital_movement>().Sat_Name = sats[i];
                prefab.transform.GetComponent<Orbital_movement>().tle1 = sats[i + 1];
                prefab.transform.GetComponent<Orbital_movement>().tle2 = sats[i + 2];
                prefab.transform.GetComponent<Orbital_movement>().target = earth.transform;
                prefab.transform.GetComponent<Show_name>().Information =sats[i+4];
                Instantiate(prefab, target.transform);
            }
        }
        //Instantiate spaceports
        List<string>spaceports = new List<string> { "Плесецк", "Россия", "Мирный", "62,7", "40,3", "https://ru,wikipedia,org/wiki/%D0%9F%D0%BB%D0%B5%D1%81%D0%B5%D1%86%D0%BA_(%D0%BA%D0%BE%D1%81%D0%BC%D0%BE%D0%B4%D1%80%D0%BE%D0%BC)", 
        "Байконур", "Казахстан(Россия)", "Казылы", "45,6", "63,3", "https://ru,wikipedia,org/wiki/%D0%91%D0%B0%D0%B9%D0%BA%D0%BE%D0%BD%D1%83%D1%80", 
        "Свободный", "Россия", "Циолковский", "51,5", "138,5", "https://ru,wikipedia,org/wiki/%D0%A1%D0%B2%D0%BE%D0%B1%D0%BE%D0%B4%D0%BD%D1%8B%D0%B9_(%D0%BA%D0%BE%D1%81%D0%BC%D0%BE%D0%B4%D1%80%D0%BE%D0%BC)", 
        "Восточный", "Россия", "Циолковский", "51,5", "138,5", "https://ru,wikipedia,org/wiki/%D0%92%D0%BE%D1%81%D1%82%D0%BE%D1%87%D0%BD%D1%8B%D0%B9_(%D0%BA%D0%BE%D1%81%D0%BC%D0%BE%D0%B4%D1%80%D0%BE%D0%BC)", 
        "Капустин Яр", "Россия", "Знаменск", "48,5", "45,8", "https://ru,wikipedia,org/wiki/Капустин_Яр", 
        "Цзьюцуань", "Китай", "Цзьюцуань", "41,1", "100,3", "https://ru,wikipedia,org/wiki/%D0%A6%D0%B7%D1%8E%D1%86%D1%8E%D0%B0%D0%BD%D1%8C_(%D0%BA%D0%BE%D1%81%D0%BC%D0%BE%D0%B4%D1%80%D0%BE%D0%BC)", 
        "Тайюань", "Китай", "Синьчжоу", "37,8", "11,5", "https://ecoruspace,me/%D0%A2%D0%B0%D0%B9%D1%8E%D0%B0%D0%BD%D1%8C,html", 
        "Сичан", "Китай", "Сичан", "28,1", "102,3", "https://ru,wikipedia,org/wiki/%D0%A1%D0%B8%D1%87%D0%B0%D0%BD_(%D0%BA%D0%BE%D1%81%D0%BC%D0%BE%D0%B4%D1%80%D0%BE%D0%BC)",
         "Тонхэ(бывший Мусудан)", "КНДР", "Musudan-ri", "40,8", "129,4", "https://ru,wikipedia,org/wiki/%D0%A2%D0%BE%D0%BD%D1%85%D1%8D_(%D0%BA%D0%BE%D1%81%D0%BC%D0%BE%D0%B4%D1%80%D0%BE%D0%BC)", 
         "Утиноура", "Япония", "Кимоцуки(Выбший Утиноура)", "31,25", "131,1", "https://ru,wikipedia,org/wiki/%D0%9A%D0%BE%D1%81%D0%BC%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D0%B9_%D1%86%D0%B5%D0%BD%D1%82%D1%80_%D0%A3%D1%82%D0%B8%D0%BD%D0%BE%D1%83%D1%80%D0%B0", 
         "Танегасима", "Япония", "Остров Танегасима (Кагосима)", "30,4", "131", "https://ru,wikipedia,org/wiki/%D0%9A%D0%BE%D1%81%D0%BC%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D0%B9_%D1%86%D0%B5%D0%BD%D1%82%D1%80_%D0%A2%D0%B0%D0%BD%D1%8D%D0%B3%D0%B0%D1%81%D0%B8%D0%BC%D0%B0", 
         "Космический центр имени Сатиша Дхавана", "Индия", "Шрихарикота", "13,8", "80,3", "https://ru,wikipedia,org/wiki/%D0%9A%D0%BE%D1%81%D0%BC%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D0%B9_%D1%86%D0%B5%D0%BD%D1%82%D1%80_%D0%B8%D0%BC%D0%B5%D0%BD%D0%B8_%D0%A1%D0%B0%D1%82%D0%B8%D1%88%D0%B0_%D0%94%D1%85%D0%B0%D0%B2%D0%B0%D0%BD%D0%B0", 
         "Пальмахим", "Израиль", "Ришон-ле-Цион", "31,9", "34,7", "https://ru,wikipedia,org/wiki/%D0%9F%D0%B0%D0%BB%D1%8C%D0%BC%D0%B0%D1%85%D0%B8%D0%BC_(%D0%B0%D0%B2%D0%B8%D0%B0%D0%B1%D0%B0%D0%B7%D0%B0)", 
         "Вумера", "Австралия(арендуется Великобританией)", "", "31,1", "136,8,", "https://ru,wikipedia,org/wiki/%D0%92%D1%83%D0%BC%D0%B5%D1%80%D0%B0", 
         "Сан Марко", "Кения(арендуется Италией)", "Малинди", "2,9", "40,2", "https://ru,wikipedia,org/wiki/%D0%A1%D0%B0%D0%BD-%D0%9C%D0%B0%D1%80%D0%BA%D0%BE_(%D0%BC%D0%BE%D1%80%D1%81%D0%BA%D0%BE%D0%B9_%D0%BA%D0%BE%D1%81%D0%BC%D0%BE%D0%B4%D1%80%D0%BE%D0%BC)", 
         "Хаммагир", "Алжир(арендуется Францией)", "", "30,9", "3,1,", "https://ru,wikipedia,org/wiki/%D0%A5%D0%B0%D0%BC%D0%BC%D0%B0%D0%B3%D0%B8%D1%80", 
         "Алкантара", "Бразилия", "", "2,3", "44,4,", "https://ru,wikipedia,org/wiki/%D0%90%D0%BB%D0%BA%D0%B0%D0%BD%D1%82%D0%B0%D1%80%D0%B0_(%D0%BA%D0%BE%D1%81%D0%BC%D0%BE%D0%B4%D1%80%D0%BE%D0%BC)", 
         "Куру", "Франзуцская Гвиана", "Куру и Синнамари", "5,2", "52,73", "https://ru,wikipedia,org/wiki/%D0%9A%D1%83%D1%80%D1%83_(%D0%BA%D0%BE%D1%81%D0%BC%D0%BE%D0%B4%D1%80%D0%BE%D0%BC)", 
         "Мыс Канаверал", "США", "Штаб квартира Флорида", "28,4", "80,5,", "https://ru,wikipedia,org/wiki/%D0%91%D0%B0%D0%B7%D0%B0_%D0%92%D0%92%D0%A1_%D0%A1%D0%A8%D0%90_%D0%BD%D0%B0_%D0%BC%D1%8B%D1%81%D0%B5_%D0%9A%D0%B0%D0%BD%D0%B0%D0%B2%D0%B5%D1%80%D0%B0%D0%BB", 
         "Уоллопс", "США", "Штат Вирджиния", "37,8", "75,5,", "https://ru,wikipedia,org/wiki/%D0%A3%D0%BE%D0%BB%D0%BB%D0%BE%D0%BF%D1%81", 
         "Ванденберг", "США", "Ломпок", "34,6", "120,5,", "https://ru,wikipedia,org/wiki/%D0%91%D0%B0%D0%B7%D0%B0_%D0%92%D0%B0%D0%BD%D0%B4%D0%B5%D0%BD%D0%B1%D0%B5%D1%80%D0%B3", 
         "Морской старт", "Международный", "Тихий океан", "3", "157,", "https://ru,wikipedia,org/wiki/%D0%9C%D0%BE%D1%80%D1%81%D0%BA%D0%BE%D0%B9_%D1%81%D1%82%D0%B0%D1%80%D1%82", 
         "Кадьяк", "США", "Аляска", "57,3", "152,2,", "https://ru,wikipedia,org/wiki/%D0%9A%D0%B0%D0%B4%D1%8C%D1%8F%D0%BA_(%D0%BA%D0%BE%D1%81%D0%BC%D0%BE%D0%B4%D1%80%D0%BE%D0%BC)", 
         "Аль-анбар", "Ирак", "", "32,5", "44,6", "https://astro-obzor,ru/al-anbar-al-anbar-irak/", "Семнан", "Иран", "", "35,1", "53,5", "https://ru,wikipedia,org/wiki/%D0%A1%D0%B5%D0%BC%D0%BD%D0%B0%D0%BD_(%D0%BA%D0%BE%D1%81%D0%BC%D0%BE%D0%B4%D1%80%D0%BE%D0%BC)", 
         "Ясный", "Россия", "", "51", "59,5", "https://ru,wikipedia,org/wiki/%D0%AF%D1%81%D0%BD%D1%8B%D0%B9_(%D0%BF%D1%83%D1%81%D0%BA%D0%BE%D0%B2%D0%B0%D1%8F_%D0%B1%D0%B0%D0%B7%D0%B0)", 
         "Кваджелейн", "США", "Атолл в тихом океане", "", "", "https://ecoruspace,me/%D0%9A%D0%B2%D0%B0%D0%B4%D0%B6%D0%B5%D0%BB%D0%B5%D0%B9%D0%BD,html", 
         "Вэньчан", "Китай", "о, Хайнань", "19,4", "110,5", "https://ru,wikipedia,org/wiki/%D0%92%D1%8D%D0%BD%D1%8C%D1%87%D0%B0%D0%BD_(%D0%BA%D0%BE%D1%81%D0%BC%D0%BE%D0%B4%D1%80%D0%BE%D0%BC)" };
        
        
        List<Spaceport> list = new List<Spaceport>();
        list.Add(new Spaceport("Плесецк", "Россия", "Мирный", 62.7, 40.3,"TestInfo"));
        list.Add(new Spaceport("Байконур", "Казахстан(Россия)", "Казылы", 45.6, 63.3,"TestInfo1"));
        list.Add(new Spaceport("Свободный", "Россия", "Циолковский", 51.5, 138.5,"___"));
        list.Add(new Spaceport("Восточный", "Россия", "Циолковский", 51.5, 138.5,"___"));       
        list.Add(new Spaceport("Капустин Яр", "Россия", "Знаменск", 48.5, 45.8,"___"));   
        list.Add(new Spaceport("Цзьюцуань", "Китай", "Цзьюцуань", 41.1, 100.3,"___"));   
        list.Add(new Spaceport("Тайюань", "Китай", "Синьчжоу", 37.8, 11.5,"___"));   
        list.Add(new Spaceport("Сичан", "Китай", "Сичан", 28.1, 102.3,"___"));
        list.Add(new Spaceport("Тонхэ(бывший Мусудан)", "КНДР", "Musudan-ri", 40.8, 129.4,"___"));
        list.Add(new Spaceport("Утиноура", "Япония", "Кимоцуки(Выбший Утиноура)", 31.25, 131.1,"___"));   
        list.Add(new Spaceport("Танегасима", "Япония", "Остров Танегасима (Кагосима)", 30.4, 131,"___"));
        list.Add(new Spaceport("Космический центр имени Сатиша Дхавана", "Индия", "Шрихарикота", 13.8, 80.3,"___"));
        list.Add(new Spaceport("Пальмахим", "Израиль", "Ришон-ле-Цион", 31.9, 34.7,"___"));
        list.Add(new Spaceport("Вумера", "Австралия(арендуется Великобританией)", "", 31.1, 136.8,"___"));
        list.Add(new Spaceport("Сан Марко", "Кения(арендуется Италией)", "Малинди", 2.9, 40.2,"___"));
        list.Add(new Spaceport("Хаммагир", "Алжир(арендуется Францией)", "", 30.9, 3.1,"___"));
        list.Add(new Spaceport("Алкантара", "Бразилия", "", 2.3, 44.4,"___"));
        list.Add(new Spaceport("Куру", "Франзуцская Гвиана", "Куру и Синнамари", 5.2, 52.73,"___"));
        list.Add(new Spaceport("Мыс Канаверал", "США", "Штаб квартира Флорида", 28.4, 80.5,"___"));
        list.Add(new Spaceport("Уоллопс", "США", "Штат Вирджиния", 37.8, 75.5,"___"));
        list.Add(new Spaceport("Ванденберг", "США", "Ломпок", 34.6, 120.5,"___"));
        list.Add(new Spaceport("Морской старт", "Международный", "Тихий океан", 3, 157,"___"));
        list.Add(new Spaceport("Кадьяк", "США", "Аляска", 57.3, 152.2,"___"));
        list.Add(new Spaceport("Аль-анбар", "Ирак", "", 32.5, 44.6,"___"));
        list.Add(new Spaceport("Ясный", "Россия", "", 51, 59.5,"___"));
        list.Add(new Spaceport("Кваджелейн", "США", "Атолл в тихом океане", 0, 0,"___"));
        list.Add(new Spaceport("Вэньчан", "Китай", "о, Хайнань", 19.4, 110.5,"___"));



        

        SpaceportList SpList = new SpaceportList(list);
        Debug.Log("string1 = "+SpList.SpList[0].Name); 


        string json = JsonUtility.ToJson(SpList,true);
        Debug.Log("Json string = "+json);      

        WriteDataToFile(json);

        SpaceportList SpList1 = JsonUtility.FromJson<SpaceportList>(json);

        Debug.Log("After Json = "+SpList1.SpList[0].Name); 

        foreach (Spaceport Sp in SpList1.SpList)
        {
            Debug.Log("Spawning: "+ Sp.Name);
            GameObject prefab = Resources.Load("Космодром") as GameObject;
            prefab.gameObject.name = Sp.Name;
            prefab.transform.GetComponent<GeoPoint>().latit = Sp.Latitude; //В БИБЛИОТЕКЕ ПЕРЕПУТАНЫ ШИРОТА И ДОЛГОТА
            prefab.transform.GetComponent<GeoPoint>().longit = Sp.Longitude;
            prefab.transform.GetComponent<GeoPoint>().target = earth.transform;
            prefab.transform.GetComponent<Show_name>().Information = Sp.Info;
            Instantiate(prefab, target.transform);
        }
    }
    public static void WriteDataToFile (string jsonString)
    {
     string path = Application.dataPath+ "/SpacePorts.json";
     Debug.Log ("AssetPath:" + path);
     File.WriteAllText (path, jsonString);
     #if UNITY_EDITOR
     UnityEditor.AssetDatabase.Refresh ();
     #endif
    }
}
