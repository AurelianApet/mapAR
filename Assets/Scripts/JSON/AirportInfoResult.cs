using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AirportResult {
	public Airport[] result;
}

[System.Serializable]
public class Airport
{

	public string Code;

	public string City;

	public string country_code;

	public string direction;

	public int distance;

	public float elevation;

	public int heading;

	public float latitude;

	public float longitude;

	public string name;

	public string state;

	public string timezone;

	public string wiki_url;

}