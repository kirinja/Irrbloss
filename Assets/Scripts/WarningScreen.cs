using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningScreen : MonoBehaviour {

	public Transform player;
	private Transform enemy;
	public float maxDistance = 20;
	public float minDistance = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (enemy == null) {
			return;
		}
		var alpha = 1-(Mathf.Clamp ((player.position - enemy.position).magnitude, minDistance, maxDistance)- minDistance) / (maxDistance - minDistance);
		GetComponent<Image> ().color = new Color (1, 1, 1, alpha ); //hur nära ska fienden vara för att skiten ska synas/inte synas  
	}


	public void checkEnemyPosition(Transform position){

		if (enemy == null || (player.position - enemy.position).magnitude > (position.position - enemy.position).magnitude){ //Får reda på avståndet mellan spelaren och fienden
			enemy = position;
		}


	}
}
