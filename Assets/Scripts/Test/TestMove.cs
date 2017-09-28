using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour {
	
	Rigidbody rb;
	
	// -- //
	
	void Start() {
		rb = GetComponent<Rigidbody>();
		
		StartCoroutine(_DoMove());
	}
	
	IEnumerator _DoMove() {
		WaitForSeconds wait = new WaitForSeconds(1);
		rb.velocity = Vector3.right * 3;
		
		while(true) {
			rb.velocity *= -1;
			yield return wait;
		}
	}
	
}
