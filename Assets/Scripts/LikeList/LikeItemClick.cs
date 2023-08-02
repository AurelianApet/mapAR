using UnityEngine;
using System.Collections;
using System.Xml;
using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LikeItemClick : MonoBehaviour {

	public delegate void DeleteLikeItem(int nIndex);
	public static event DeleteLikeItem EventDelelteLikeItem;

	private Texture	texTitleBack;
	private GUIStyle	blankStyle = new GUIStyle();
	private bool		bDelClicked = false;

	private int index;
	public int Index
	{
		get {	return index;}
		set {	index = value;}
	}
	// Use this for initialization
	void Start () {
		index = 0;
		
		// Item 객체의 부모(=Grid) 하위의 모든 자식요소(=Item들)을 스캔한다.
		foreach (Transform child in this.transform.parent) {
			// 특정 자식 요소와 나 자신이 동일하다면 반복을 멈춘다.
			if (child == transform) {
				// 여기서 멈추게 되면, 현재의 index값이 나 스스로의 번호가 된다.
				break;
			}
			// 인덱스 값을 1씩 증가한다.
			index++;
		}

		texTitleBack = Resources.Load ("like/DelPopBack") as Texture2D;
	}
	
	// Update is called once per frame
	void Update () {
	
	}	
}
