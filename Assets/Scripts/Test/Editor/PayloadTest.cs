using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Game.Manager;

public class PayloadTest {
	
	EventPayload pl;
	
	// -- //
	
	public PayloadTest() {
		pl = new EventPayload();
	}
	
	[Test]
	public void EventPayloadAddParameter() {
		pl.Add("param1", "salut");
		
		Assert.IsTrue(pl.Add("param2", true));
		Assert.IsFalse(pl.Add("param1", "déjà dedans"));
		
		pl.Add("myObject", new EventPayload());
	}
	
	[Test]
	public void EventPayloadGetParameter() {
		Assert.AreEqual(typeof(string), pl.Get<string>("param1").GetType());
		Assert.AreEqual(typeof(bool), pl.Get<bool>("param2").GetType());
		Assert.AreEqual(typeof(float), pl.Get<float>("param2").GetType());
		Assert.AreEqual(typeof(EventPayload), pl.Get<EventPayload>("myObject").GetType());
	}
	
}
