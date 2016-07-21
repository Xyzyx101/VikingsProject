using UnityEngine;
using System.Collections;

public class NavAgentTest : MonoBehaviour {

	public Transform goal;
	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		agent.SetDestination(FindClosestGoal().transform.position); 
	}
	
	// Update is called once per frame
	void Update () {
		if (!goal) {
			GameObject targetObject = FindClosestGoal();
			if (targetObject) {
				goal = targetObject.transform;
			} else {
				agent.Stop();
			}
		}
		agent.SetDestination(goal.position); 
	}

	GameObject FindClosestGoal() {
		GameObject[] goals;
		goals = GameObject.FindGameObjectsWithTag("goal");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		foreach (GameObject newGoal in goals) {
			Vector3 diff = newGoal.transform.position - transform.position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closest = newGoal;
				distance = curDistance;
			}
		}
		return closest;
	}
}
