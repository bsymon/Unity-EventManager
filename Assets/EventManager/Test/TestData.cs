using UnityEngine;
using Game.Tools;

namespace Game {

public class TestData : MonoBehaviour {
	
	public int _loop          = 1000;
	public bool _setWrongType = false;
	public bool _getWrongType = false;
	
	// -- //
	
	EventPayload payload = new EventPayload();
	
	// -- //
	
	void Update() {
		// Add
		if(Input.GetKeyDown(KeyCode.A)) {
			payload.Clear();
			
			for(int i = 0; i < _loop; ++ i) {
				payload.Add(i.ToString(), i.ToString());
			}
		}
		
		// Set
		if(Input.GetKeyDown(KeyCode.S)) {
			for(int i = 0; i < _loop; ++ i) {
				if(_setWrongType) {
					payload.Set(i.ToString(), i);
				} else {
					payload.Set(i.ToString(), i.ToString());
				}
			}
		}
		
		// Get
		if(Input.GetKeyDown(KeyCode.G)) {
			for(int i = 0; i < _loop; ++ i) {
				payload.Get<string>(i.ToString());
			}
		}
		
		// Get parameters name
		if(Input.GetKeyDown(KeyCode.H)) {
			string[] parametersName = new string[1000];
			
			if(_getWrongType) {
				payload.GetParametersOfType<float>(ref parametersName);
			} else {
				payload.GetParametersOfType<string>(ref parametersName);
			}
		}
	}
	
}

}