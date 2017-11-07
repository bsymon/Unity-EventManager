using UnityEngine;
using Game.Tools;

namespace Game {

public class DummyListener {
	
	public DummyListener(GameObject emitter) {
		EventManager.AddListener("OnTest", OnTest);
		EventManager.AddListener("OnTest", OnTestWithData);
		EventManager.AddListener("OnTest", OnTestForEmitter, emitter);
		EventManager.AddListener("OnTest", OnTestWithDataForEmitter, emitter);
	}
	
	// -- //
	
	void OnTest() {
		if(TestCase.DEBUG_MESSAGE_IN_LISTENERS) {
			Debug.Log("Event : " + EventManager.CurrentEvent + " | No Data");
		}
	}
	
	void OnTestWithData(EventPayload data) {
		if(TestCase.DEBUG_MESSAGE_IN_LISTENERS) {
			Debug.Log("Event : " + EventManager.CurrentEvent + " | Data : " + data.Get<string>("Text"));
		}
	}
	
	void OnTestForEmitter() {
		if(TestCase.DEBUG_MESSAGE_IN_LISTENERS) {
			Debug.Log("Event : " + EventManager.CurrentEvent + " | Emitter triggered the event");
		}
	}
	
	void OnTestWithDataForEmitter(EventPayload data) {
		if(TestCase.DEBUG_MESSAGE_IN_LISTENERS) {
			Debug.Log("Event : " + EventManager.CurrentEvent + " | Emitter triggered the event. Data : " + data.Get<string>("Text"));
		}
	}
	
}

}