using UnityEngine;
using Game.Tools;

namespace Game {

public class TestCase : MonoBehaviour {
	
	static public bool DEBUG_MESSAGE_IN_LISTENERS = false;
	
	// -- //
	
	[Header("On Init")]
	public int _howMany = 1000;
	public GameObject _emitter;
	
	[Header("On Trigger")]
	public bool _pickRandomTarget = false;
	public bool _sendToFakeTarget = false;
	public bool _sendData         = false;
	public bool _debugMessageInListener = false;
	public GameObject _currentEmitter;
	
	// -- //
	
	DummyListener[] listeners;
	EventPayload data  = new EventPayload();
	object target      = null;
	object fakeTarget  = new object();
	
	// -- //
	
	void Start() {
		listeners = new DummyListener[_howMany];
		
		for(int i = 0; i < _howMany; ++ i) {
			listeners[i] = new DummyListener(_emitter);
		}
		
		data.Add("Text", "Hello World!");
	}
	
	void Update() {
		if(Input.GetKeyDown(KeyCode.T)) {
			DEBUG_MESSAGE_IN_LISTENERS = _debugMessageInListener;
			
			if(_pickRandomTarget) {
				target = listeners[Random.Range(0, _howMany)];
			} else if(_sendToFakeTarget) {
				target = fakeTarget;
			} else {
				target = null;
			}
			
			if(_sendData) {
				EventManager.Trigger("OnTest", data, target, _currentEmitter);
			} else {
				EventManager.Trigger("OnTest", target, _currentEmitter);
			}
		}
	}
	
}

}