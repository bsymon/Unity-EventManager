using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

namespace Game.Tools {

public delegate void ListenerAction();
public delegate void ListenerAction<T>(T Data);
	
public class EventInvoker<T> {
	
	public object Target { get; set; }
	public object Emitter { get; set; }
	
	// -- //
	
	static object NO_EMITTER = new object();
	
	// -- //
	
	/** Key : emitter to listen | Value : listeners */
	Dictionary<object, OrderedDictionary> listenersWithoutData = new Dictionary<object, OrderedDictionary>();
	Dictionary<object, OrderedDictionary> listenersWithData    = new Dictionary<object, OrderedDictionary>();
	int listenersCount = 0;
	
	// -- //
	
	public int ListenersCount {
		get { return listenersCount; }
	}
	
	// -- //
	
	void _AddListener(ref Dictionary<object, OrderedDictionary> dico, System.Delegate listenerAction, object emitterToListen = null) {
		emitterToListen = emitterToListen == null ? NO_EMITTER : emitterToListen;
		OrderedDictionary listeners;
		
		if(!dico.TryGetValue(emitterToListen, out listeners)) {
			// Create an entry for the emitter
			listeners = new OrderedDictionary();
			dico.Add(emitterToListen, listeners);
		}
		
		listeners[listenerAction.Target] = System.Delegate.Combine(listeners[listenerAction.Target] as System.Delegate, listenerAction);
		listenersCount++;
	}
	
	void _RemoveListener(ref Dictionary<object, OrderedDictionary> dico, System.Delegate listenerAction) {
		System.Delegate theDelegate;
		
		foreach(OrderedDictionary listeners in dico.Values) {
			// Delete the listener from the delegate
			theDelegate = System.Delegate.RemoveAll(listeners[listenerAction.Target] as System.Delegate, listenerAction);
			listeners[listenerAction.Target] = theDelegate;
			
			if(theDelegate == null) {
				// There no more listener in the delegate
				listeners.Remove(listenerAction.Target);
			}
			
			listenersCount--;
		}
	}
	
	void _CallDelegate(object theDelegate, bool sendData, T data) {
		if(theDelegate != null) {
			if(sendData) {
				(theDelegate as ListenerAction<T>)(data);
			} else {
				(theDelegate as ListenerAction)();
			}
		}
	}
	
	void _Invoke(ref Dictionary<object, OrderedDictionary> dico, bool sendData, T data = default(T)) {
		OrderedDictionary listeners = dico[NO_EMITTER];
		int nbListeners = listeners.Count;
		object emitter  = Emitter;
		
		while(listeners != null) {
			if(Target != null) {
				_CallDelegate(listeners[Target], sendData, data);
			} else {
				for(int i = 0; i < nbListeners; ++ i) {
					_CallDelegate(listeners[i], sendData, data);
				}
			}
			
			if(emitter != null && dico.TryGetValue(emitter, out listeners)) {
				nbListeners = listeners.Count;
				emitter     = null;
			} else {
				listeners = null;
			}
		}
	}
	
	// -- //
	
	public void AddListener(ListenerAction listenerAction, object emitterToListen = null) {
		_AddListener(ref listenersWithoutData, listenerAction, emitterToListen);
	}
	
	public void AddListener(ListenerAction<T> listenerAction, object emitterToListen = null) {
		_AddListener(ref listenersWithData, listenerAction, emitterToListen);
	}
	
	public void RemoveListener(ListenerAction listenerAction) {
		_RemoveListener(ref listenersWithoutData, listenerAction);
	}
	
	public void RemoveListener(ListenerAction<T> listenerAction) {
		_RemoveListener(ref listenersWithData, listenerAction);
	}
	
	public void Invoke() {
		_Invoke(ref listenersWithoutData, false);
	}
	
	public void Invoke(T data) {
		_Invoke(ref listenersWithData, true, data);
		Invoke();
	}
	
}

}
