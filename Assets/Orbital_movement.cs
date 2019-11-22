using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using One_Sgp4;
using System;


public class Orbital_movement : MonoBehaviour
{
    
    public string tle1= "1 25544U 98067A   19257.23636851 -.00000464  00000-0  00000+0 0  9992";
    public string tle2= "2 25544  51.6440 279.5905 0008750  38.3150  36.5419 15.50445690189071";
    public string Sat_Name= "ISS 1";
    private Tle tle;
    private Coordinate coordinate;
    public float Earth_Rad = 6378.135F;
    public Transform target;
    private Vector3 newPos;  //possition on last frame
    private Transform oldPos;  //position on new frame
    public double height;
    private float m = 0.9F;
    // Start is called before the first frame update    
    void Start()
    {
        Sat_Name = gameObject.name.Replace("(Clone)", string.Empty);
        tle = ParserTLE.parseTle(
            tle1,tle2,Sat_Name);
    }

    // Update is called once per frame
    void Update()
    {
        oldPos = this.transform;
        if (transform.gameObject != game_state.ChoosedObject)
        {
            //Parse three line element
            EpochTime nowtime = new EpochTime(DateTime.UtcNow);
            System.Console.Write(nowtime.ToString() + "\n");
            Sgp4Data satPos = SatFunctions.getSatPositionAtTime(tle, nowtime, One_Sgp4.Sgp4.wgsConstant.WGS_84);
            //Calculate Latitude, longitude and height for satellite on Earth
            Coordinate coordinate = SatFunctions.calcSatSubPoint(nowtime, satPos, One_Sgp4.Sgp4.wgsConstant.WGS_84);
            Point3d Spheric = SatFunctions.calcSphericalCoordinate(coordinate, nowtime, satPos);
            //Transform polar coordinates into decart
            double latit = (coordinate.getLatetude()); //В БИБЛИОТЕКЕ ПЕРЕПУТАНЫ ШИРОТА И ДОЛГОТА
            double longit = (coordinate.getLongitude());
            height = (coordinate.getHeight());
            if (height > 15000) //If object too far from Earth - make it closer
                height = height / 2;
            float x = (Earth_Rad + (float)height) * (float)Math.Cos(latit * Math.PI / 180) * (float)Math.Cos(longit * Math.PI / 180);
            float z = (Earth_Rad + (float)height) * (float)Math.Cos(latit * Math.PI / 180) * (float)Math.Sin(longit * Math.PI / 180);
            float y = (Earth_Rad + (float)height) * (float)Math.Sin(latit * Math.PI / 180);
            newPos = new Vector3(x, y, z);
            transform.position = Vector3.Lerp(newPos, oldPos.position, m);
            //Make object look at Earth
            if (target == null) // If no target - make Earth a target
                target = GameObject.Find("EarthHigh").transform;
            Vector3 direction = target.position - transform.position; //Remove position info and left only rotation
            Quaternion rotation = Quaternion.LookRotation(direction); //Rotation of object toward Earth

            transform.rotation = rotation;

            if (Physics.Raycast(transform.position, Camera.main.transform.position))
                transform.GetComponent<MeshRenderer>().enabled = false;
            else
                transform.GetComponent<MeshRenderer>().enabled = true;
        }
        else //Get model close to camera
        {
            if (transform.parent != Camera.main.transform.GetChild(0))
            {
                transform.SetParent(Camera.main.transform.GetChild(0));
                
            }
            newPos = transform.parent.position;
            transform.position = Vector3.Lerp(newPos, oldPos.position, m);
            /*
            Vector3 ToScreenPos = new Vector3(Screen.width / 4, Screen.height / 2, -100);
            //this.transform.parent.GetComponent<Orbital_movement>
            float z = Camera.main.transform.position.z+2000;
            Ray ray = Camera.main.ScreenPointToRay(ToScreenPos);
            Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
            float distance;
            xy.Raycast(ray, out distance);
            newPos = ray.GetPoint(distance);
            transform.position = Vector3.Lerp(newPos, oldPos.position, m);
            // this.transform.parent.transform.position = cam.ScreenToWorldPoint(ToScreenPos);*/
        }
    }
}
