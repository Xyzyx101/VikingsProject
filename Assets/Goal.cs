using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	public float hp;
	public GameObject rubblePrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ApplyDamage (float damage) {	
		hp -= damage;
		if (hp <= 0f) {
			MakeRubble();
		}
	}

	void MakeRubble () {
		for (uint i = 0; i < 20; ++i) {
			Instantiate( rubblePrefab, transform.position + Random.insideUnitSphere * 3, Quaternion.identity );
		}
		Destroy (gameObject);
	}
}
