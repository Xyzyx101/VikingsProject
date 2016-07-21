using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	public float hp = 300;
	public bool isTouchingGround = false;
	public GameObject rubblePrefab;
	private Rigidbody myRigidbody;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody> ();
		CreateWallJoints ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter(Collision c) {
		if (!isTouchingGround && c.gameObject.CompareTag ("ground")) {
			MakeRubble();
		}
	}

	public void ApplyDamage (float damage) {

		hp -= damage;
		if (hp <= 0f) {
			MakeRubble();
		}
	}

	void CreateWallJoints () {
		Vector3 dir = new Vector3(1,0,0);
		RaycastHit hit;
		if ( Physics.Raycast (GetComponent<Collider>().bounds.center, dir, out hit, GetComponent<Collider>().bounds.extents.x * 2) ) {
			Debug.DrawRay (GetComponent<Collider>().bounds.center, dir * GetComponent<Collider>().bounds.extents.x * 2, Color.blue, 3f);
			gameObject.AddComponent<HingeJoint>();

			Joint joint = GetComponent(typeof(HingeJoint)) as HingeJoint;
			joint.autoConfigureConnectedAnchor = true;
			joint.connectedBody = hit.rigidbody;

			joint.anchor = transform.InverseTransformPoint(GetComponent<Collider>().bounds.center);

			joint.axis =- new Vector3(0,1,0);

			//joint.connectedAnchor = new Vector3(-1f, 0.25f, 0f);
			//joint.enableCollision = false;

			//joint.breakForce = 100000f;
			joint.breakTorque = 50000f;
		}

	}

	/*void OnJointBreak(float breakForce) {
		Debug.Log("A joint has just been broken!, force: " + breakForce);
	}*/

	void MakeRubble () {
		Instantiate (rubblePrefab, transform.position, Quaternion.identity );
		gameObject.SendMessageUpwards("WallBroke", null, SendMessageOptions.DontRequireReceiver);
		Destroy (gameObject);
	}
}
