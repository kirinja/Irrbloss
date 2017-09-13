using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    private AudioSource _source;
    public AudioClip AudioClip;
    public float LightLevelToActivate = 0.90f;

    private ParticleSystem _particleSystem;

    public bool Enabled
    {
        get { return GetComponent<Light>().enabled; }
    }

    // need to change this from a single reference to an array of references
    //public Door Door { private get; set; }
    
    private readonly List<Door> _doors = new List<Door>();

    public void AddDoor(Door door)
    {
        _doors.Add(door);
    }

    // Use this for initialization
	void Start ()
	{
	    _source = gameObject.AddComponent<AudioSource>();
	    _particleSystem = GetComponentInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !GetComponent<Light>().enabled)
        {
            if (other.GetComponent<Controller3D>().LightLevel >= LightLevelToActivate)
            {
                _particleSystem.Play(true);
                GetComponent<Light>().enabled = true;
                foreach (var d in _doors)
                {
                    d.CheckLevelComplete();
                }
                //Door.CheckLevelComplete();
                _source.PlayOneShot(AudioClip);
            }
        }
    }

}
