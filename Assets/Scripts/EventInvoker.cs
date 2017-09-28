using System.Collections.Generic;
using UnityEngine;

namespace Game.Manager {

public delegate void ListenerAction<T_L>(T_L data);
public delegate void ListenerAction();

public abstract class BaseInvoker<T> {
	
	protected object target;
	protected object emitter;
	protected Dictionary<object, List<T>> listeners = new Dictionary<object, List<T>>();
	protected Dictionary<object, Dictionary<object, List<T>>> listenersWithEmitter = new Dictionary<object, Dictionary<object, List<T>>>();
	protected int listenersCount = 0;
	
	// -- //
	
	public int ListenersCount {
		get { return listenersCount; }
	}
	
	// -- //
	
	public void AddListener(object listenerObject, T listenerAction) {
		if(!listeners.ContainsKey(listenerObject)) {
			listeners.Add(listenerObject, new List<T>());
		}
		
		// TODO check si le listener n'est pas déjà enregistré
		
		listeners[listenerObject].Add(listenerAction);
		listenersCount++;
	}
	
	public void AddListener(object emitterToListen, object listenerObject, T listenerAction) {
		if(!listenersWithEmitter.ContainsKey(emitterToListen)) {
			listenersWithEmitter.Add(emitterToListen, new Dictionary<object, List<T>>());
		}
		
		if(!listenersWithEmitter[emitterToListen].ContainsKey(listenerObject)) {
			listenersWithEmitter[emitterToListen].Add(listenerObject, new List<T>());
		}
		
		// TODO check si le listener n'est pas déjà enregistré
		
		listenersWithEmitter[emitterToListen][listenerObject].Add(listenerAction);
		listenersCount++;
	}
	
	public void RemoveListener(object listenerObject, T listenerAction) {
		if(listeners.ContainsKey(listenerObject)) {
			if(listeners[listenerObject].Remove(listenerAction)) {
				listenersCount--;
			}
		}
		
		foreach(object emitter in listenersWithEmitter.Keys) {
			if(listenersWithEmitter[emitter].ContainsKey(listenerObject)) {
				if(listenersWithEmitter[emitter][listenerObject].Remove(listenerAction)) {
					listenersCount--;
				}
			}
		}
		
		// TODO peut-être pouvoir supprimer séparement les listeners qui écoute un object
	}
	
	public void SetTarget(object newTarget) {
		// TODO check dans le cas d'un emitter
		
		if(newTarget != null && !listeners.ContainsKey(newTarget)) {
			// Silent fail
			// Debug.LogError("EventInvoker : The target has no listener");
			// return;
		}
		
		target = newTarget;
	}
	
	public void SetEmitter(object newEmitter) {
		if(newEmitter != null && !listenersWithEmitter.ContainsKey(newEmitter)) {
			// Silent fail
			// Debug.LogError("EventInvoker : There is no listener waiting for this emitter");
			return;
		}
		
		emitter = newEmitter;
	}
	
	// -- //
	
	protected IEnumerable<T> NextListener() {
		if(emitter != null) {
			if(target != null) {
				if(listenersWithEmitter.ContainsKey(target)) {
					foreach(T action in listenersWithEmitter[emitter][target]) {
						yield return action;
					}
				}
			} else {
				foreach(object listenerObject in listenersWithEmitter[emitter].Keys) {
					foreach(T action in listenersWithEmitter[emitter][listenerObject]) {
						yield return action;
					}
				}
			}
		}
		
		if(target != null) {
			if(listeners.ContainsKey(target)) {
				foreach(T action in listeners[target]) {
					yield return action;
				}
			}
		} else {
			foreach(object listenerObject in listeners.Keys) {
				foreach(T action in listeners[listenerObject]) {
					yield return action;
				}
			}
		}
	}
	
}

public class EventInvoker : BaseInvoker<ListenerAction> {
	
	public void Invoke() {
		foreach(ListenerAction action in base.NextListener()) {
			action();
		}
	}
	
}

public class EventInvoker<T0> : BaseInvoker<ListenerAction<T0>> {
	
	public void Invoke(T0 data) {
		foreach(ListenerAction<T0> action in base.NextListener()) {
			action(data);
		}
	}
	
}

}
