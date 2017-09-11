using UnityEngine;

public class LightSource : MonoBehaviour
{
    private AudioSource _source;
    public AudioClip AudioClip;
    public float LightLevelToActivate = 0.90f;

    public bool Enabled
    {
        get { return GetComponent<Light>().enabled; }
    }

    public Door Door { private get; set; }

    // Use this for initialization
	void Start ()
	{
	    _source = gameObject.AddComponent<AudioSource>();
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
                GetComponent<Light>().enabled = true;
                Door.CheckLevelComplete();
                _source.PlayOneShot(AudioClip);
            }
        }
    }

}
