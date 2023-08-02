using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showFlag : MonoBehaviour {

	public GameObject Flag;
	bool is_show = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void show_Flag()
	{		
		if (!is_show) {
			GameObject[] objs = GameObject.FindGameObjectsWithTag ("stick");
			for (int i = 0; i < objs.Length; i++) {
				objs[i].SetActive(false);
			}
			Flag.SetActive (true);
			is_show = true;
		} else {
			Flag.SetActive (false);
			is_show = false;
		}
	}
}
