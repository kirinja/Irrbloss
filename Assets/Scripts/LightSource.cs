using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour {

    public bool Enabled
    {
        get { return GetComponent<Light>().enabled; }
    }

    public Door Door { private get; set; }

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Light>().enabled = true;
            Door.CheckLevelComplete();
        }
    }

}
