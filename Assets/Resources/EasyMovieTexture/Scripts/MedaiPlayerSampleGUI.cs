using UnityEngine;
using System.Collections;

public class MedaiPlayerSampleGUI : MonoBehaviour {
	
	public MediaPlayerCtrl scrMedia;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		
	
		if( GUI.Button(new Rect(50,50,100,100),"Load"))
		{
			scrMedia.Load("EasyMovieTexture.mp4");
		}
		
		if( GUI.Button(new Rect(50,200,100,100),"Play"))
		{
			scrMedia.Play();
		}
	 	
		if( GUI.Button(new Rect(50,350,100,100),"stop"))
		{
			scrMedia.Stop();
		}
		
		if( GUI.Button(new Rect(50,500,100,100),"pause"))
		{
			scrMedia.Pause();
		}
		
		if( GUI.Button(new Rect(50,650,100,100),"Unload"))
		{
			scrMedia.UnLoad();
		}
		
		if( GUI.Button(new Rect(50,800,100,100),"Unload"))
		{
			scrMedia.SetSpeed(2.0f);
		}
		
	}
}
