using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public Text Text;

    private bool _end;

    private Transform _transform;
	// Use this for initialization
	void Start ()
	{
	    _transform = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
	    if (_end)
	    {
	        _transform.position += new Vector3(0, 10, 0) * Time.deltaTime;
	    }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            Text.gameObject.SetActive(true);
            other.attachedRigidbody.useGravity = false;
            other.attachedRigidbody.isKinematic = true;
            other.GetComponent<Controller3D>().enabled = false;
            other.GetComponent<Collider>().enabled = false;
            
            _end = true;

            StartCoroutine(EndGame());
        }
    }


    private IEnumerator EndGame()
    {
        // end the game 5 seconds after you step into the light beam
        yield return new WaitForSeconds(5.0f);
        Debug.Log("Ending game");
        Cursor.lockState = CursorLockMode.None;
	    Cursor.visible = true;
        SceneManager.LoadScene("Menu");
        // Application.Quit();
    }

}
