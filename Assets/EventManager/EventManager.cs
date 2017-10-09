using System.Collections.Generic;
using UnityEngine;

namespace Game.Tools {

public class EventManager {
	
	static EventManager instance;
	static public EventManager Instance {
		get {
			if(instance == null) {
				instance = new EventManager();
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
		EventInvoker<EventPayload> invoker;
		
		if(events.TryGetValue(eventName, out invoker)) {
			invoker.Emitter = emitter;
			invoker.Target  = target;
			
			if(data == null) {
				invoker.Invoke();
			} else {
				invoker.Invoke(data);
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