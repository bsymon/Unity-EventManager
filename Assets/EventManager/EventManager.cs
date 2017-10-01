using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Tools {

public class EventManager : MonoBehaviour {
	
	static EventManager instance;
	static public EventManager Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EventManager>();
			}
			
			return instance;
		}
	}
	
	// -- //
	
	Dictionary<string, EventInvoker<EventPayload>> events = new Dictionary<string, EventInvoker<EventPayload>>();
	
	// -- //
	
	void _AddEvent(string eventName) {
		if(!events.ContainsKey(eventName)) {
			events.Add(eventName, new EventInvoker<EventPayload>());
		}
	}
	
	void _AddListener(string eventName, ListenerAction action, object emitterToListen = null) {
		_AddEvent(eventName);
		events[eventName].AddListener(action, emitterToListen);
	}
	
	void _AddListener(string eventName, ListenerAction<EventPayload> action, object emitterToListen = null) {
		_AddEvent(eventName);
		events[eventName].AddListener(action, emitterToListen);
	}
	
	void _RemoveListener(string eventName, ListenerAction action) {
		EventInvoker<EventPayload> invoker;
		
		if(events.TryGetValue(eventName, out invoker)) {
			invoker.RemoveListener(action);
			
			if(events[eventName].ListenersCount == 0) {
				events.Remove(eventName);
			}
		}
	}
	
	void _RemoveListener(string eventName, ListenerAction<EventPayload> action) {
		EventInvoker<EventPayload> invoker;
		
		if(events.TryGetValue(eventName, out invoker)) {
			invoker.RemoveListener(action);
			
			if(events[eventName].ListenersCount == 0) {
				events.Remove(eventName);
			}
		}
	}
	
	void _Trigger(string eventName, EventPayload data = null, object target = null, object emitter = null) {
		if(events.ContainsKey(eventName)) {
			events[eventName].Emitter = emitter;
			events[eventName].Target  = target;
			
			if(data == null) {
				events[eventName].Invoke();
			} else {
				events[eventName].Invoke(data);
			}
		}
	}
	
	// -- //
	
	static public void AddListener(string eventName, ListenerAction action, object listenTo = null) {
		Instance._AddListener(eventName, action, listenTo);
	}
	
	static public void AddListener(string eventName, ListenerAction<EventPayload> action, object listenTo = null) {
		Instance._AddListener(eventName, action, listenTo);
	}
	
	static public void RemoveListener(string eventName, ListenerAction action) {
		Instance._RemoveListener(eventName, action);
	}
	
	static public void RemoveListener(string eventName, ListenerAction<EventPayload> action) {
		Instance._RemoveListener(eventName, action);
	}
	
	static public void Trigger(string eventName, object target = null, object emitter = null) {
		Instance._Trigger(eventName, null, target, emitter);
	}
	
	static public void Trigger(string eventName, EventPayload data, object target = null, object emitter = null) {
		Instance._Trigger(eventName, data, target, emitter);
	}
	
}

}