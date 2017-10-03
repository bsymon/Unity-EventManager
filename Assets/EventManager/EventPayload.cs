using System.Collections.Generic;
using UnityEngine;

namespace Game.Tools {

public class EventPayload {
	
	Dictionary<string, object> parameters = new Dictionary<string, object>();
	List<string> keys = new List<string>();
	
	// -- //
	
	public bool Add(string parameterName, object value) {
		try {
			parameters.Add(parameterName, value);
			keys.Add(parameterName);
			return true;
		} catch(System.ArgumentException) {
			Debug.LogError("EventPayload : The key '" + parameterName + "' already exists");
		}
		
		return false;
	}
	
	public bool Has(string parameterName) {
		return parameters.ContainsKey(parameterName);
	}
	
	public T Get<T>(string parameterName) {
		bool goodType = false;
		object value;
		
		if(parameters.TryGetValue(parameterName, out value)) {
			goodType = value is T;
			if(!goodType) {
				Debug.LogWarning("EventPayload : The key '" + parameterName + "' is not of type '" + typeof(T).Name + "'. Returning default value of type '" + typeof(T).Name + "'");
			}
		}
		
		return goodType ? (T) value : default(T);
	}
	
	public bool Set(string parameterName, object value) {
		try {
			if(value.GetType() == parameters[parameterName].GetType()) {
				parameters[parameterName] = value;
				return true;
			}
		} catch(KeyNotFoundException) {
			Debug.LogError("EventPayload : The key '" + parameterName + "' doesn't exist");
		}
		
		return false;
	}
	
	public int GetParametersOfType<T>(ref string[] output) {
		int nbOfParameters = 0;
		int maxLoop        = Mathf.Min(output != null ? output.Length : 0, keys.Count);
		
		for(int i = 0; i < maxLoop; ++ i) {
			string key = keys[i];
			
			if(parameters[key] is T) {
				output[nbOfParameters] = key;
				nbOfParameters++;
			}
		}
		
		return nbOfParameters;
	}
	
	public void Clear() {
		parameters.Clear();
		keys.Clear();
	}
	
}

}