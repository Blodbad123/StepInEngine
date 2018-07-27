using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class DisplayTitles : MonoBehaviour {


	// Get information on how many titles are currently displayed. 
	// Display new titles accordingly.

	public GameObject titlePrefab;

	public void DisplayNewTitle(Texture2D img, string title, string description){
		GameObject tempTitle; 
		tempTitle = (GameObject)Instantiate(titlePrefab);
		tempTitle.GetComponentInChildren<Renderer>().material.mainTexture = img;
	}

	public void DisplayNewTitle(GameObject prefab, Texture2D img, float x, float y) 
	{
		//GameObject tempTitle; 
		GameObject tempTitle = Instantiate (prefab) as GameObject;
		tempTitle.transform.parent = GameObject.Find("Canvas").GetComponent<Transform>();
		tempTitle.transform.localPosition = Vector2.zero; 
		tempTitle.GetComponentInChildren<Image>().material.mainTexture = img;
	}
}
