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

[Serializable]
public class SatelliteList
{
   public List<Satellite> SList;
   public SatelliteList(List<Satellite> list)
   {
       this.SList = list;
   }
}
[Serializable]
public class Satellite
{
    public string Name;
    public string tle1;
    public string tle2;
    public string Type;
    public string Info;

    public Satellite(string name, string tle_1, string tle_2, string type,string info)
    {
        this.Name = name;
        this.tle1 = tle_1;
        this.tle2 = tle_2;
        this.Type = type;
        this.Info = info;
    }
}