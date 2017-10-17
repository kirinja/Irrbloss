using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eye : MonoBehaviour {


	public Transform EyeUI;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Player"))
			EyeUI.gameObject.SetActive (true);
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag ("Player"))
			EyeUI.gameObject.SetActive (false);
	}
}
