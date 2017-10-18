using System;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    private AudioSource _source;
    public AudioClip AudioClip;
    public float LightLevelToActivate = 0.90f;
    private bool _enabled = false;
    private ParticleSystem _particleSystem;

    //public String SubtitleSound;
    private SubtitleComponent _subtitleComponent;

    private SoundIcon _soundIcon;
    public bool Enabled
    {
        //get { return GetComponent<Light>().enabled; }
        get { return _enabled; }
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
	    _subtitleComponent = GetComponent<SubtitleComponent>();
        //_subtitleComponent.SaveData("LightSource");
        _subtitleComponent.LoadData("LightSource");
        _source = gameObject.AddComponent<AudioSource>();
	    _particleSystem = GetComponentInChildren<ParticleSystem>();
	    _soundIcon = GetComponent<SoundIcon>();
	    _soundIcon.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _enabled = true; // have to do this before we tell the doors to check if we can disable them
            foreach (var d in _doors)
            {
                d.CheckLevelComplete();
            }
            _particleSystem.Play(true);
            var child = transform.Find("PS_Light02");
            var ps = child.Find("Cirlce01");
            ps.GetComponent<ParticleSystem>().Play(true);
            //GetComponent<Light>().enabled = true;

            //Door.CheckLevelComplete();
            _source.PlayOneShot(AudioClip);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !Enabled && !other.isTrigger)
        {
            if (other.GetComponent<Controller3D>().LightLevel >= LightLevelToActivate)
            {
                _enabled = true; // have to do this before we tell the doors to check if we can disable them
                foreach (var d in _doors)
                {
                    d.CheckLevelComplete();
                    d.DoorSubtitles(other);
                }
                
                _particleSystem.Play(true);
                var child = transform.Find("PS_Light02");
                var ps = child.Find("Cirlce01");
                ps.GetComponent<ParticleSystem>().Play(true);
                //GetComponent<Light>().enabled = true;
                
                //Door.CheckLevelComplete();
                _source.PlayOneShot(AudioClip);
                other.GetComponent<SubtitleSystem>().AddSubtitle(_subtitleComponent);
                _soundIcon.enabled = true; // enable icon
            }
        }
    }

}
