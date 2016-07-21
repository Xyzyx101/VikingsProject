using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {
	
	public float AttackDamage;
	public float AttackSpeed;
	private float AttackDelayTimer;
	private bool CanAttack = true;

	// Use this for initialization
	void Start () {
		AttackDelayTimer = AttackSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		if (!CanAttack) {
			AttackDelayTimer -= Time.deltaTime;
			if(AttackDelayTimer < 0) {
				CanAttack = true;
			}
		}
	}

	void OnTriggerStay(Collider c) {
		if (CanAttack ) {
			if ( c.gameObject.CompareTag ("wall") ) {
				Wall wall = c.gameObject.GetComponent<Wall>();
				if (wall.hp > 0) {
					DoDamage(c.gameObject);
				}
			}
			if ( c.gameObject.CompareTag ("goal") ) {
				Goal goal = c.gameObject.GetComponent<Goal>();
				if (goal.hp > 0) {
					DoDamage(c.gameObject);
				}
			}
		}
	}

	void DoDamage (GameObject other) {
		CanAttack = false;
		AttackDelayTimer = AttackSpeed;

		Debug.DrawLine(transform.position, other.transform.position, Color.red, 1f);
		
		Vector3 explosionPos = transform.position;
		float explosionForce = 3500f;
		float explosionRadius = 1f;
		float upwardsModifier = 1f;
		
		Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
		foreach (Collider hit in colliders) {
			if( !hit.gameObject.CompareTag("wall") ) continue;
			if (hit && hit.GetComponent<Rigidbody>()) {
				Debug.DrawLine(transform.position, hit.transform.position, Color.cyan);
				hit.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, explosionPos, explosionRadius, upwardsModifier, ForceMode.Impulse);
			}
		}
		
		other.SendMessage("ApplyDamage", AttackDamage, SendMessageOptions.DontRequireReceiver);

	}
}
