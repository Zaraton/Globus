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

/*[Serializable]
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
}*///old stuff
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
    public string Official_Name;
    public string Country_or_Org_of_UN_Registry;
    public string Country_of_Operator_or_Owner;
    public string Operato_or_Owner;
    public string Users;
    public string Purpose;
    public string Detailed_Purpose;
    public string Class_of_Orbit;
    public string Type_of_Orbit;
    public string Longitude_of_GEO_in_degrees;
    public string Perigee_in_km;
    public string Apogee_in_km;
    public string Eccentricity;
    public string Inclination_in_degrees;
    public string Period_in_minutes;
    public string Launch_Mass_in_kg;
    public string Dry_Mass_in_kg;
    public string Power_in_watts;
    public string Date_of_Launch;
    public string Expected_Lifetime_in_years;
    public string Contractor;
    public string Country_of_Contractor;
    public string Launch_Site;
    public string Launch_Vehicle;
    public string COSPAR_Number;
    public string NORAD_Number;
    public string Comments;
    public string Orbital_Data_Source;
    public string Source1;
    public string Source2;
    public string Source3;
    public string Source4; 
    public string TLE1;
    public string TLE2;
    public string Model_3D;
    public string Info;

    public Satellite(string name, string tle_1, string tle_2, string type, string info)
    {
        this.Name = name;
        this.TLE1 = tle_1;
        this.TLE2 = tle_2;
        this.Model_3D = type;
        this.Info = info;
    }
}