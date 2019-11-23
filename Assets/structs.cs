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