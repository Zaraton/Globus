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
    public string name;
    public string Country;
    public string Locationname;
    public double Latitude;
    public double Longitude;
    public string Info;

    public Spaceport(string name, string country, string locationname, double latitude,double longitude,string info)
    {
        this.name = name;
        this.Country = country;
        this.Locationname = locationname;
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
    public string name;
    public string tle1;
    public string tle2;
    public string Type;
    public string Info;

    public Satellite(string name, string tle_1, string tle_2, string type,string info)
    {
        this.name = name;
        this.tle1 = tle_1;
        this.tle2 = tle_2;
        this.Type = type;
        this.Info = info;
    }
}*///old stuff
[Serializable]
public class Satellite
{
    public string name;//public string NAVSTAR 58 (USA 190)public string ,
    public string official_name;//public string NAVSTAR 58 (USA 190)public string ,
    public string TLE1;//public string 1 29486U 06042A   21140.25886115  .00000009  00000-0  00000-0 0  9994public string ,
    public string TLE2;//public string 2 29486  54.7748 224.9296 0098674  13.7964 346.5444  2.00549927107213public string ,
    public string classification;//public string Upublic string ,
    public string argument_of_perigee;//public string  13.7964public string ,
    public string mean_anomaly;//public string 346.5444public string ,
    public string mean_motion;//public string  2.00549927public string ,
    public string revolution_number;//public string 10721public string ,
    public string eccentricity;//public string 009867public string ,
    public string inclination_in_degrees;//public string  54.774public string ,
    public string date_of_launch;//public string 06public string ,
    public string launch_number_of_the_year;//public string 042public string ,
    public string launch_piece;//public string A  public string ,
    public string epoch_year;//public string 21public string ,
    public string epoch_day;//public string 140.25886115public string ,
    public string ballistic_coefficient;//public string  .00000009public string ,
    public string second_derivative_of_mean_motion;//public string  00000-0public string ,
    public string drag_term;//public string  00000-0public string ,
    public string ephemeris_type;//public string  public string ,
    public string element_set_number;//public string  999public string ,
    public string COSPAR_number;//public string 06042A  public string ,
    public string NORAD_number;//public string 29486public string ,
    public string apogee_in_km;//public string 20,452.7 km public string ,
    public string inclination;//public string 54.8 \u00b0 public string ,
    public string period;//public string 718.0 minutes public string ,
    public string launch_site;//public string AIR FORCE EASTERN TEST RANGE (AFETR)public string ,
    public string info;//public string GPS 2R-15 - американский навигационный спутник в составе флота GPS, который был запущен ракетой Delta 2 с мыса Канаверал в 18:50 UT 25 сентября 2006 года. Он будет размещен в плоскости A, слот 2, взамен устаревшего GPS 2. -12, спущенный на воду в 1992 году, который, в свою очередь, будет передан на А-4 в качестве запасного до окончания срока его полезного использования. В настоящее время во флоте 24 действующих судна, плюс 5 запасных. Первая версия GPS Block IIR-M, которая добавляла дополнительные навигационные сигналы как для гражданских, так и для военных пользователей.public string ,
    public string country_or_org_of_UN_registry;//public string USApublic string ,
    public string country_of_operator_or_owner;//public string USApublic string ,
    public string operator_or_owner;//public string DoD/US Air Forcepublic string ,
    public string users;//public string Military/Commercialpublic string ,
    public string purpose;//public string Navigation/Global Positioningpublic string ,
    public string class_of_orbit;//public string MEOpublic string ,
    public string type_of_orbit;//public string Non-Polar Inclinedpublic string ,
    public string longitude_of_GEO_in_degrees;//public string 0.00public string ,
    public string period_in_minutes;//public string 717.93public string ,
    public string launch_mass_in_kg;//public string 2,060public string ,
    public string expected_lifetime_in_years;//public string 10public string ,
    public string contractor;//public string Lockheed Martin Missiles & Spacepublic string ,
    public string country_of_contractor;//public string USApublic string ,
    public string launch_vehicle;//public string Delta 2public string ,
    public string links;//public string http://www.lib.cas.cz/knav/space.40/2006/I042A.HTM    http://nssdc.gsfc.nasa.gov/spacewarn/spx635.html    ftp://tycho.usno.navy.mil/pub/gps/gpstd.txt    public string 
    public string Model_3D;
}
[Serializable]
public class SatelliteList
{
    public List<Satellite> SList;
    public SatelliteList(List<Satellite> list)
    {
        this.SList = list;
    }
}/*
public class SatelliteList2
{
    public List<Satellite> SList;
    public SatelliteList(List<Satellite> list)
    {
        this.SList = list;
    }
}
[Serializable]
public class Satellite2
{
    public string name;//public string NAVSTAR 58 (USA 190)public string ,
    public string official_name;//public string NAVSTAR 58 (USA 190)public string ,
    public string TLE1;//public string 1 29486U 06042A   21140.25886115  .00000009  00000-0  00000-0 0  9994public string ,
    public string TLE2;//public string 2 29486  54.7748 224.9296 0098674  13.7964 346.5444  2.00549927107213public string ,
    public string classification;//public string Upublic string ,
    public string argument_of_perigee;//public string  13.7964public string ,
    public string mean_anomaly;//public string 346.5444public string ,
    public string mean_motion;//public string  2.00549927public string ,
    public string revolution_number;//public string 10721public string ,
    public string eccentricity;//public string 009867public string ,
    public string inclination_in_degrees;//public string  54.774public string ,
    public string date_of_launch;//public string 06public string ,
    public string launch_number_of_the_year;//public string 042public string ,
    public string launch_piece;//public string A  public string ,
    public string epoch_year;//public string 21public string ,
    public string epoch_day;//public string 140.25886115public string ,
    public string ballistic_coefficient;//public string  .00000009public string ,
    public string second_derivative_of_mean_motion;//public string  00000-0public string ,
    public string drag_term;//public string  00000-0public string ,
    public string ephemeris_type;//public string  public string ,
    public string element_set_number;//public string  999public string ,
    public string COSPAR_number;//public string 06042A  public string ,
    public string NORAD_number;//public string 29486public string ,
    public string apogee_in_km;//public string 20,452.7 km public string ,
    public string inclination;//public string 54.8 \u00b0 public string ,
    public string period;//public string 718.0 minutes public string ,
    public string launch_site;//public string AIR FORCE EASTERN TEST RANGE (AFETR)public string ,
    public string info;//public string GPS 2R-15 - американский навигационный спутник в составе флота GPS, который был запущен ракетой Delta 2 с мыса Канаверал в 18:50 UT 25 сентября 2006 года. Он будет размещен в плоскости A, слот 2, взамен устаревшего GPS 2. -12, спущенный на воду в 1992 году, который, в свою очередь, будет передан на А-4 в качестве запасного до окончания срока его полезного использования. В настоящее время во флоте 24 действующих судна, плюс 5 запасных. Первая версия GPS Block IIR-M, которая добавляла дополнительные навигационные сигналы как для гражданских, так и для военных пользователей.public string ,
    public string country_or_org_of_UN_registry;//public string USApublic string ,
    public string country_of_operator_or_owner;//public string USApublic string ,
    public string operator_or_owner;//public string DoD/US Air Forcepublic string ,
    public string users;//public string Military/Commercialpublic string ,
    public string purpose;//public string Navigation/Global Positioningpublic string ,
    public string class_of_orbit;//public string MEOpublic string ,
    public string type_of_orbit;//public string Non-Polar Inclinedpublic string ,
    public string longitude_of_GEO_in_degrees;//public string 0.00public string ,
    public string period_in_minutes;//public string 717.93public string ,
    public string launch_mass_in_kg;//public string 2,060public string ,
    public string expected_lifetime_in_years;//public string 10public string ,
    public string contractor;//public string Lockheed Martin Missiles & Spacepublic string ,
    public string country_of_contractor;//public string USApublic string ,
    public string launch_vehicle;//public string Delta 2public string ,
    public string links;//public string http://www.lib.cas.cz/knav/space.40/2006/I042A.HTM    http://nssdc.gsfc.nasa.gov/spacewarn/spx635.html    ftp://tycho.usno.navy.mil/pub/gps/gpstd.txt    public string 
    public string Model_3D;

    public Satellite(string name, string tle_1, string tle_2, string type, string info) // obsolete
    {
        this.name = name;
        this.TLE1 = tle_1;
        this.TLE2 = tle_2;
        this.Model_3D = type;
        this.info = info;
    }
}*/