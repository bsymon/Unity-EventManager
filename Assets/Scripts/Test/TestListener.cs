using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Manager;

public class TestListener : MonoBehaviour {
	
	public TestTrigger listenTo;
	
	int a = 0;
	public string msg = "SALUT";
	
	Game.TestNoGameObject ngo;
	
	// -- //
	
	void Start() {
		EventManager.AddListener("OnTest", OnTest, listenTo);
		
		ngo = new Game.TestNoGameObject("OBJECT : " + msg);
	}
	
	void OnDestroy() {
		EventManager.RemoveListener("OnTest", OnTest);
	}
	
	// -- EVENTS -- //
	
	void OnTest(EventPayload data) {
		// int m = Random.Range(10000, 50000);
		// for(int i = 0; i < m; ++i) {
		// 	a++;
		// }
		
		Debug.Log(data.Get<string>("msg"));
	}
	
}
