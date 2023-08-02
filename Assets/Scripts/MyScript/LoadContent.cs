using UnityEngine;
using System.Collections;
using System.Xml;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO.Compression;
using LitJson;

using Vuforia;

public class LoadContent : MonoBehaviour {
	private List<GameObject> mTrackableView;
	private GameObject ARCamera;
	private bool is_destory;

	// Use this for initialization
	void Start () {
		is_destory = false;
		mTrackableView = new List<GameObject>();
		ARCamera = GameObject.Find ("ARCamera");
		CloudRecoTrackableEventHandler.EventFoundMarker += StartLoadContent;
		GUIManager.EventFindingMarker += OnFindingMarker;
	}

	void OnDestroy()
	{
		CloudRecoTrackableEventHandler.EventFoundMarker -= StartLoadContent;
		GUIManager.EventFindingMarker -= OnFindingMarker;
	}

	private void OnFindingMarker()
	{
		if (mTrackableView != null) 
		{
			foreach (GameObject viewObject in mTrackableView) 
			{
				GameObject.DestroyImmediate(viewObject);
			}
			mTrackableView.Clear();
			is_destory = true;
		}
		if(Screen.orientation != ScreenOrientation.Landscape)
			Screen.orientation = ScreenOrientation.Landscape;
	}

	// Update is called once per frame
	void Update () {
	}

	void StartLoadContent()
	{
		string url = "https://lab.flysas.com/sandbox/ar-map/api.php";
		StartCoroutine(getInfo(url));

    }
   
	IEnumerator getInfo(string url){
		WWWForm form = new WWWForm();
		Dictionary<string,string> headers = form.headers;
		byte[] rawData = form.data;

		WWW www = new WWW(url);
		yield return www;
		if (www.error == null) {
			JsonData data = JsonMapper.ToObject (www.text);
			yield return process_json(data);
		}
	}

	IEnumerator process_json(JsonData data)
	{
		WWWForm form = new WWWForm();
		Dictionary<string,string> headers = form.headers;

		JsonData dest_data = data ["destinations"];

		for (int i = 0; i < dest_data.Count; i++) {
			yield return new WaitForSeconds (0.1f);
			string destination = dest_data[i]["destinatin"].ToString();
			string iata = dest_data[i]["iata"].ToString();
			double longitude = Convert.ToDouble(dest_data[i]["longitude"].ToString());
			double latitude = Convert.ToDouble(dest_data[i]["latitude"].ToString());
			JsonData dates = dest_data[i]["dates"];

			string flag_date = "";
			for (int j = 0; j < dates.Count; j++) {
				flag_date += dates [j].ToString () + '\n';
			}
			showFlags (destination, flag_date, iata, longitude, latitude);
		}
		gbO.SetActive (false);
	}

	public GameObject gbO ;

	private void showFlags(string dest, string content, string iata, double longi, double lati)
	{
		string header = dest + " " + iata;


		double map_width = 1318;
		double map_height = 799;

		double width = 0;
		double height = 0;
		width = map_width / 2 + map_width / 360 * longi - 128;
		height = map_height / 2 + map_height / 180 * lati - 432;

		Debug.Log ("width=" + width);
		Debug.Log ("height=" + height);
		GameObject gbO1 = Instantiate (gbO);
		gbO1.transform.GetChild (0).transform.GetChild (1).transform.GetChild(1).GetComponent<UILabel>().text = content;
		gbO1.transform.GetChild (0).transform.GetChild (1).transform.GetChild(2).GetComponent<UILabel>().text = header;
		gbO1.transform.GetChild (0).transform.transform.localPosition = new Vector3 ((float)width, (float)height, 0);
	}
}
