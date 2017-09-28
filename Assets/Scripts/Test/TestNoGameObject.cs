using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Manager;

namespace Game {

public class TestNoGameObject {
	
	string msg;
	
	public TestNoGameObject(string msg) {
		this.msg = msg;
		EventManager.AddListener("OnTest", OnTest);
	}
	
	// -- //
	
	void OnTest() {
		Debug.Log(msg);
	}
	
}

}