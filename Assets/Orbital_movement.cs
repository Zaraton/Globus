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
        oldPos = transform;
        if (transform.gameObject != game_state.ChoosedObject)
        {
            //Parse three line element
            
            Sgp4Data satPos = SatFunctions.getSatPositionAtTime(tle, game_state.nowtime, Sgp4.wgsConstant.WGS_84);
            //Calculate Latitude, longitude and height for satellite on Earth
            Coordinate coordinate = SatFunctions.calcSatSubPoint(game_state.nowtime, satPos, Sgp4.wgsConstant.WGS_84);
            Point3d Spheric = SatFunctions.calcSphericalCoordinate(coordinate, game_state.nowtime, satPos);
            //Transform polar coordinates into decart
            double latit = (coordinate.getLatetude());
            double longit = (coordinate.getLongitude());
            height = (coordinate.getHeight());
            if (height > 15000) //If object too far from Earth - make it closer
                height = height / 2;
            float x = (game_state.GameEarthRad + (float)height * game_state.GameToRealEarthCor) * (float)Math.Cos(latit * Math.PI / 180) * (float)Math.Cos(longit * Math.PI / 180);
            float z = (game_state.GameEarthRad + (float)height * game_state.GameToRealEarthCor) * (float)Math.Cos(latit * Math.PI / 180) * (float)Math.Sin(longit * Math.PI / 180);
            float y = (game_state.GameEarthRad + (float)height * game_state.GameToRealEarthCor) * (float)Math.Sin(latit * Math.PI / 180);
            newPos = new Vector3(x, y, z);
            transform.position = Vector3.Lerp(newPos, oldPos.position, m);
            //Make object look at Earth
            if (target == null) // If no target - make Earth a target
                target = GameObject.Find("Earth").transform;
            Vector3 direction = target.position - transform.position; //Remove position info and left only rotation
            Quaternion rotation = Quaternion.LookRotation(direction); //Rotation of object toward Earth

            transform.rotation = rotation;

            if (Physics.Raycast(transform.position, Camera.main.transform.position))
            {
                var rendererComponents = transform.GetComponentsInChildren<MeshRenderer>(true);
                foreach (var component in rendererComponents)

                    component.enabled = false;
                transform.GetComponent<Show_name>().enabled = false;
            }
            else
            {
                var rendererComponents = transform.GetComponentsInChildren<MeshRenderer>(true);
                foreach (var component in rendererComponents)
                    component.enabled = true;
                transform.GetComponent<Show_name>().enabled = true;
            }
        }
        else //Get model close to camera
        {
            if (transform.parent != Camera.main.transform.GetChild(0))
            {
                transform.SetParent(Camera.main.transform.GetChild(0));
                
            }
            newPos = transform.parent.position;
            transform.position = Vector3.Lerp(newPos, oldPos.position, m);
        }
    }
}
