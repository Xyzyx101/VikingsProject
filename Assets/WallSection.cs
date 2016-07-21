using UnityEngine;
using System.Collections;

public class WallSection : MonoBehaviour {

	public float percentBroken;
	private float breakableSections;

	// Use this for initialization
	void Start () {
		foreach (Transform child in transform) {
			GameObject childObject = child.gameObject;
			if( childObject.CompareTag("wall") ) {
				Wall wall = childObject.GetComponent<Wall>();
					++breakableSections;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void WallBroke () {
		percentBroken += 1f / breakableSections;
		if (percentBroken > 0.6) {
			foreach (Transform child in transform) {
				GameObject childObject = child.gameObject;
				if( childObject.name == "PathBlocker" ) {
					Destroy(childObject);
				}
			}
		}
	}
}
