using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightCounter : MonoBehaviour
{
    public LightSource[] Lights;
    public Text Text;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerExit(Collider other)
    {
        Text.text = "";
    }

    private void OnTriggerStay(Collider other)
    {
        int cnt = 0;
        foreach (var l in Lights)
        {
            if (l.Enabled)
                cnt++;
        }
        Text.text = cnt + " / " + Lights.Length;
    }


}
