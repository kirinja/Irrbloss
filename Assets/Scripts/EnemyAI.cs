using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    private int _currentTarget = 0;
    public Transform[] Targets;
	// Use this for initialization
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.destination = Targets[_currentTarget].position;
    }

	
	// Update is called once per frame
	void Update ()
	{
        // makes enemy move between targets
	    float t = (transform.position - Targets[_currentTarget].position).magnitude;
        //Debug.Log(t);

	    if (t <= 0.5f)
	    {
	        _currentTarget++;
	        if (_currentTarget >= Targets.Length)
	            _currentTarget = 0;

	        _agent.destination = Targets[_currentTarget].position;
	    }
	}
}
