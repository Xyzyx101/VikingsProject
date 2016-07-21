using UnityEngine;
using System.Collections;

public class TestShot : MonoBehaviour {

	public float deathTimer = 4f;
	private bool initialHit = true;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		deathTimer -= Time.deltaTime;
		if (deathTimer < 0) {
			Destroy(gameObject);
		}
	}
	
	void OnCollisionEnter(Collision c) {
		if( !(initialHit && c.gameObject.CompareTag("wall")) ) return;
		initialHit = false;
		float momentum = c.relativeVelocity.magnitude * c.gameObject.GetComponent<Rigidbody>().mass;
		foreach (ContactPoint contact in c.contacts) {
			Debug.DrawRay(contact.point, contact.normal * 3f, Color.red);
		}

		Vector3 explosionPos = transform.position;
		float explosionForce = GetComponent<Rigidbody>().velocity.magnitude * GetComponent<Rigidbody>().mass * 10f;
		float explosionRadius = 1f;
		float upwardsModifier = 0f;

		//Debug.Log ("expF: " + explosionForce);

		Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
		foreach (Collider hit in colliders) {
			if( !hit.gameObject.CompareTag("wall") ) continue;
			if (hit && hit.GetComponent<Rigidbody>()) {
				Debug.DrawLine(transform.position, hit.transform.position, Color.cyan);
				hit.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, explosionPos, explosionRadius, upwardsModifier, ForceMode.Impulse);
			}
		}

		c.gameObject.SendMessage("ApplyDamage", momentum * 0.001f, SendMessageOptions.DontRequireReceiver);
	}

	void OnCollisionStay(Collision collision) {
		foreach (ContactPoint contact in collision.contacts) {
			Debug.DrawRay(contact.point, contact.normal * 1.5f, Color.yellow);
		}
	}		
}
