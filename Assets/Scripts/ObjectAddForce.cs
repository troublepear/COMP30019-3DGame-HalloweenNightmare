using UnityEngine;
using System.Collections;

public class ObjectAddForce : MonoBehaviour {

	public int force;	

	
	void FixedUpdate () {
		gameObject.GetComponent<Rigidbody>()		
			.AddForce (new Vector3(0,-force,0));	
	}

}
