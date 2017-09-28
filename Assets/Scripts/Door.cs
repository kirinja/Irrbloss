using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    private AudioSource _source;
    public LightSource[] Lights;
    public AudioClip AudioClip;
    public bool EndLevel = false;

    //public String SubtitleSound;
    private SubtitleComponent _subtitleComponent;

    private bool _triggered = false;

	// Use this for initialization
	void Start ()
	{
	    _subtitleComponent = GetComponent<SubtitleComponent>();
        foreach (var l in Lights)
        {
            //l.Door = this;
            l.AddDoor(this);
        }
        _source = gameObject.AddComponent<AudioSource>();
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
        //GetComponent<Renderer>().material.color = new Color(0, 1, 0, 1);
        _source.PlayOneShot(AudioClip);
        _triggered = true; 
        if (EndLevel)
        {
            GetComponent<Renderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
        }
        else
        {
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }

    public void DoorSubtitles(Collider other)
    {
        if (other.GetComponent<SubtitleSystem>() && _triggered)
        {
            other.GetComponent<SubtitleSystem>().AddSubtitle(_subtitleComponent);
        }
    }
}
