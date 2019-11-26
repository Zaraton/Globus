﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using One_Sgp4;
using System;

public class GeoPoint : MonoBehaviour
{
    public float Earth_Rad = 6378.135F;
    public double latit;
    public double longit;
    public float height;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        //Transform polar coordinates to scene coordinates 
        float x = (Earth_Rad + (float)height) * (float)Math.Cos(latit * Math.PI / 180) * (float)Math.Cos(longit * Math.PI / 180);
        float z = (Earth_Rad + (float)height) * (float)Math.Cos(latit * Math.PI / 180) * (float)Math.Sin(longit * Math.PI / 180);
        float y = (Earth_Rad + (float)height) * (float)Math.Sin(latit * Math.PI / 180);
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
        float size = dist / 400000;
        if(size < 0.03608918)
            transform.localScale = new Vector3(size,size,size);
        if (Physics.Raycast(transform.position, Camera.main.transform.position))
        {
            transform.GetComponent<Show_name>().enabled = false;
            transform.GetChild(0).transform.GetChild(0).transform.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            transform.GetComponent<Show_name>().enabled = true;
            transform.GetChild(0).transform.GetChild(0).transform.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}