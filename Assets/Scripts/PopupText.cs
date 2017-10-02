using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupText : MonoBehaviour {

	public float timeToDie;
	private float elapsedTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		elapsedTime += Time.deltaTime; // För varje frame uppdateras timern med den tid som gått.
		if (elapsedTime >= timeToDie) {
			Destroy (gameObject); //Förstör objektet
		}

	}

	public static void showPopup(string text){
		
		var window = GameObject.Find ("PopupText");
		var instance = Instantiate (Resources.Load ("Popup", typeof(GameObject)), window.transform)as GameObject; //Skapar ett nytt gameobject och lägger in den i scenen från Popup i Resource filen, window är parent till obejktet
		instance.GetComponent <Text>().text = text; //Instansierar objektet som en text



	}



}
