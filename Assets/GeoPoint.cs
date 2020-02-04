using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using One_Sgp4;
using System;

public class GeoPoint : MonoBehaviour
{
    public double latit;
    public double longit;
    public float height;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        //Transform polar coordinates to scene coordinates 
        float x = (game_state.GameEarthRad + (float)height * game_state.GameToRealEarthCor) * (float)Math.Cos(latit * Math.PI / 180) * (float)Math.Cos(longit * Math.PI / 180);
        float z = (game_state.GameEarthRad + (float)height * game_state.GameToRealEarthCor) * (float)Math.Cos(latit * Math.PI / 180) * (float)Math.Sin(longit * Math.PI / 180);
        float y = (game_state.GameEarthRad + (float)height * game_state.GameToRealEarthCor) * (float)Math.Sin(latit * Math.PI / 180);
        transform.position = new Vector3(x, y, z);
        if (target == null)
            target = GameObject.Find("EarthHigh").transform;
        Vector3 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction); //Rotation of object toward Earth
        transform.rotation = rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
        //Resize with distance to camera
        float dist = Vector3.Distance(Camera.main.transform.position, transform.position);
        float size = dist / 100;
        //if(size < 0.07608918)
            transform.localScale = new Vector3(size,size,size);
        Debug.DrawRay(transform.position, Camera.main.transform.position - transform.position);
        if (Physics.Raycast(transform.position, Camera.main.transform.position - transform.position) || (!game_state.IsTracking))
        {
            transform.GetComponent<Show_name>().enabled = false;
            transform.Find("default").transform.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
           transform.GetComponent<Show_name>().enabled = true;
           transform.Find("default").transform.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
