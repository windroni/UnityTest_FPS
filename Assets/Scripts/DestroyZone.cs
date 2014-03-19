﻿using UnityEngine;
using System.Collections;

public class DestroyZone : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter (Collider other) 
	{
		Debug.Log ("Enter Object : " + other.gameObject.name);
		if(other.gameObject.name.Contains("Player"))
		{
			other.gameObject.transform.position = new Vector3(0.0f, 100.0f, 0.0f);
		}
		else if(other.gameObject.name.Contains("EyeBall"))
		{
			Destroy(other.gameObject);
		}
	}
}
