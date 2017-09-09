using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Door : MonoBehaviour
{
    public LightSource[] Lights;

	// Use this for initialization
	void Start ()
    {
        foreach (var l in Lights)
        {
            l.Door = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CheckLevelComplete()
    {
        foreach (var l in Lights)
        {
            if (!l.Enabled)
                return;
        }
        // we can call level end sequence
        GetComponent<Renderer>().material.color = new Color(0, 1, 0, 1);
    }
}
