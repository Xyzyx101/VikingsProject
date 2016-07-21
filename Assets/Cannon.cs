using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

	public Transform spawnPoint;
	public GameObject shot;
	public float shotVelocity;
	public float spawnRate;
	public float variation;

	float timer;

	// Use this for initialization
	void Start () {
		timer = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0) {
			GameObject projectileBody;
			projectileBody = Instantiate( shot, spawnPoint.position, transform.rotation ) as GameObject;
			Vector3 variationVector = Random.onUnitSphere * variation;
			projectileBody.GetComponent<Rigidbody>().AddForce (transform.up * shotVelocity + variationVector, ForceMode.VelocityChange);
			timer = spawnRate;
		}
	}
}
