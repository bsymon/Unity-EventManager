using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Manager;

public class TestTrigger : MonoBehaviour {
	
	public int _howMany = 100;
	public Transform _toSpawn;
	
	public TestListener _target;
	
	EventPayload eventData = new EventPayload();
	
	// -- //
	
	void Start() {
		// for(int i = 0; i < _howMany; ++i) {
		// 	Instantiate(_toSpawn, Vector3.zero, Quaternion.identity);
		// }
	}
	
	void Update() {
		if(Input.GetKeyDown(KeyCode.A)) {
			eventData.Add("msg", "Ceci est un message");
			EventManager.Trigger("OnTest", eventData, _target, this);
			eventData.Clear();
		}
	}
	
}
