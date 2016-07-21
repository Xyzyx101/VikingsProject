using UnityEngine;
using System.Collections;

public class GuySpawner : MonoBehaviour {
	
	public GameObject prefab;
	public float spawnRate;
	
	private float timer;
	
	// Use this for initialization
	void Start () {
		timer = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0) {
			GameObject projectileBody;
			projectileBody = Instantiate( prefab, transform.position, transform.rotation ) as GameObject;
			timer = spawnRate;
		}
	}
}
