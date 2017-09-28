using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Manager {

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
	
	/**
		Key   : event name
		Value : listeners
	*/
	Dictionary<string, EventInvoker<EventPayload>> eventsArgs = new Dictionary<string, EventInvoker<EventPayload>>();
	Dictionary<string, EventInvoker> eventsNoArgs = new Dictionary<string, EventInvoker>();
	
	// -- //
	
	void _AddListener(string eventName, ListenerAction<EventPayload> action, object emitterToListen = null) {
		if(!eventsArgs.ContainsKey(eventName)) {
			eventsArgs.Add(eventName, new EventInvoker<EventPayload>());
		}
		
		if(emitterToListen != null) {
			eventsArgs[eventName].AddListener(emitterToListen, action.Target, action);
		} else {
			eventsArgs[eventName].AddListener(action.Target, action);
		}
	}
	
	void _AddListener(string eventName, ListenerAction action, object emitterToListen = null) {
		if(!eventsNoArgs.ContainsKey(eventName)) {
			eventsNoArgs.Add(eventName, new EventInvoker());
		}
		
		if(emitterToListen != null) {
			eventsNoArgs[eventName].AddListener(emitterToListen, action.Target, action);
		} else {
			eventsNoArgs[eventName].AddListener(action.Target, action);
		}
	}
	
	void _RemoveListener(string eventName, ListenerAction<EventPayload> action) {
		if(!eventsArgs.ContainsKey(eventName)) {
			// TODO debug error ??
			return;
		}
		
		eventsArgs[eventName].RemoveListener(action.Target, action);
		
		if(eventsArgs[eventName].ListenersCount == 0) {
			eventsArgs.Remove(eventName);
		}
	}
	
	void _RemoveListener(string eventName, ListenerAction action) {
		if(!eventsNoArgs.ContainsKey(eventName)) {
			// TODO debug error ??
			return;
		}
		
		eventsNoArgs[eventName].RemoveListener(action.Target, action);
		
		if(eventsNoArgs[eventName].ListenersCount == 0) {
			eventsNoArgs.Remove(eventName);
		}
	}
	
	void _Trigger(string eventName, EventPayload data = null, object target = null, object emitter = null) {
		if(eventsArgs.ContainsKey(eventName) && data != null) {
			eventsArgs[eventName].SetEmitter(emitter);
			eventsArgs[eventName].SetTarget(target);
			eventsArgs[eventName].Invoke(data);
		}
		
		if(eventsNoArgs.ContainsKey(eventName)) {
			eventsNoArgs[eventName].SetEmitter(emitter);
			eventsNoArgs[eventName].SetTarget(target);
			eventsNoArgs[eventName].Invoke();
		}
	}
	
	// -- //
	
	static public void AddListener(string eventName, ListenerAction<EventPayload> action, object listenTo = null) {
		Instance._AddListener(eventName, action, listenTo);
	}
	
	static public void AddListener(string eventName, ListenerAction action, object listenTo = null) {
		Instance._AddListener(eventName, action, listenTo);
	}
	
	static public void RemoveListener(string eventName, ListenerAction<EventPayload> action) {
		Instance._RemoveListener(eventName, action);
	}
	
	static public void RemoveListener(string eventName, ListenerAction action) {
		Instance._RemoveListener(eventName, action);
	}
	
	/**
		Trigger an event on all listeners, with data
	*/
	static public void Trigger(string eventName, EventPayload data, object target = null, object emitter = null) {
		Instance._Trigger(eventName, data, target, emitter);
	}
	
	/**
		Trigger an event on all listeners, without data
	*/
	static public void Trigger(string eventName, object target = null, object emitter = null) {
		Instance._Trigger(eventName, null, target, emitter);
	}
	
}

}