using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;	
using Vuforia;

public class GUIManager : MonoBehaviour
{
	public delegate void FindingMarker();
	public static event FindingMarker	EventFindingMarker;
	private bool is_closed = false;
	public GameObject gbO;
	float startTime;

	void Start () {
		CloudRecoTrackableEventHandler.EventFoundMarker += OnFoundMarker;
	}

	void update(){
		if(is_closed)
			GameObject.FindWithTag ("closeBtn").SetActive (true);
		else
			GameObject.FindWithTag ("closeBtn").SetActive (false);
	}

	public void OnDestroy()
	{
		GameObject.FindWithTag ("closeBtn").SetActive (false);
		GameObject.FindWithTag ("back").SetActive (false);
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("flag");
		for (int i = 0; i < objs.Length; i++) {
			DestroyImmediate (objs[i]);
		}
		gbO.SetActive (true);
		CloudRecoTrackableEventHandler.EventFoundMarker -= OnFoundMarker;
		EventFindingMarker();
	}

	void OnFoundMarker()
	{
		is_closed = true;
	}
}
