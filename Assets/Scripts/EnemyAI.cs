using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    private int _currentTarget = 0;
    public Transform[] Targets;

    public float AggroLightLevel = 0.75f;

    // enemy uses a cone infront to try and detect player
    public float ConeAngle = 35.0f;
    public float ConeLength = 1.5f;
    public float MaxChaseDistance = 5.0f; // unity units
    private float _currentChaseDistance = 0.0f;
    private Vector3 _aggroPosition;

    private Controller3D _player;
    private float _speed;

    private bool _countUp = true;

    //public String SubtitleSound;
    private SubtitleComponent _subtitleComponent;

	// Use this for initialization
    void Start()
    {
        _subtitleComponent = GetComponent<SubtitleComponent>();
        //_subtitleComponent.SaveData("Enemy");
        _subtitleComponent.LoadData("Enemy");
        _agent = GetComponent<NavMeshAgent>();
        _agent.destination = Targets[_currentTarget].position;

        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller3D>();
        _speed = _agent.speed;
    }

	
	// Update is called once per frame
	void Update ()
	{
        // makes enemy move between targets
	    float t = (transform.position - Targets[_currentTarget].position).magnitude;

	    if (CheckAggro())
	    {
            // update distance the enemy have chased the player
	        _currentChaseDistance += Vector3.Distance(transform.position, _aggroPosition);
	        _aggroPosition = transform.position;

	        if (_currentChaseDistance >= MaxChaseDistance)
	        {
                Debug.Log("Chased for too long, resetting patrol");
	            _agent.destination = Targets[_currentTarget].position;
	            return;
	        }

            // maybe increase speed if found
            Debug.Log("Found player, chase");
	        _agent.destination = _player.transform.position;
	        _agent.speed *= 2.5f;

	    }
        else if (t <= 0.5f)
	    {
	        if (_currentTarget >= Targets.Length-1)
	            _countUp = false;
	        else if (_currentTarget <= 0)
	            _countUp = true;

	        if (_countUp)
	            _currentTarget++;
	        else
	            _currentTarget--;
            //_currentTarget = 0;

            _agent.destination = Targets[_currentTarget].position;
	        _currentChaseDistance = 0;
	        _agent.speed = _speed;
	    }
	    else
	    {
	        _agent.destination = Targets[_currentTarget].position;
	        _currentChaseDistance = 0;
	        _agent.speed = _speed;
	    }
        
	}

    /// <summary>
    /// slightly buggy
    /// </summary>
    /// <returns></returns>
    bool CheckAggro()
    {
        // we're gonna raycast in a cone infront of the enemy and check if we hit the player
        var rotationLeft = Quaternion.AngleAxis(-ConeAngle, Vector3.up);
        var left = rotationLeft * transform.forward;
        var rotationRight = Quaternion.AngleAxis(ConeAngle, Vector3.up);
        var right = rotationRight * transform.forward;

        var rayLeft = new Ray(transform.position, left);
        var rayRight = new Ray(transform.position, right);
        var rayForward = new Ray(transform.position, transform.forward);

        // we need to check and compare if the players light level is high enough for the enemy to actually see the player
        var playerLight = _player.LightLevel >= AggroLightLevel;

        var foundPlayer = Physics.Raycast(rayLeft, ConeLength);
        if (foundPlayer && playerLight)
            return true;
        foundPlayer = Physics.Raycast(rayForward, ConeLength);
        if (foundPlayer && playerLight)
            return true;
        foundPlayer = Physics.Raycast(rayRight, ConeLength);
        if (foundPlayer && playerLight)
            return true;

        return false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            // Kill player
            if (_player.LightLevel >= AggroLightLevel)
            {
                _player.Kill();
                Debug.Log("Kill player");
            }
        }
    }
}
