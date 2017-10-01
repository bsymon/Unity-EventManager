using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Tools {

public class EventPayload {
	
	Hashtable parameters;
	
	// -- //
	
	public EventPayload() {
		parameters = new Hashtable();
	}
	
	public bool Add(string parameterName, object value) {
		if(parameters.ContainsKey(parameterName)) {
			Debug.LogError("EventPayload : The key '" + parameterName + "' already exists");
			return false;
		}
		
		parameters.Add(parameterName, value);
		return true;
	}
	
	public T Get<T>(string parameterName) {
		if(!parameters.ContainsKey(parameterName)) {
			return default(T);
		}
		
		object value = parameters[parameterName];
		
		if(!(value is T)) {
			Debug.LogWarning("EventPayload : The key '" + parameterName + "' is not of type '" + typeof(T).Name + "'. Returning default value of type '" + typeof(T).Name + "'");
		}
		
		return value is T ? (T) parameters[parameterName] : default(T);
	}
	
	public void Clear() {
		parameters.Clear();
	}
	
	
	
}

}