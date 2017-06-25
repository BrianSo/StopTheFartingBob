using System;
using UnityEngine;
using System.Collections.Generic;

public class DoubleTap : MonoBehaviour
{
	public static DoubleTap singleton;

	public float tapTime = 0.2f;
	public float doubleTapDistance = 1f;

	float lastUpdateTime;
	Vector2 lastPosition;
	Vector2 lastLastPosition;

	void Awake(){
		this.Singleton (ref singleton);
	}
	void OnDestroy(){
		this.RemoveSingleton (ref singleton);
	}

	void OnGUI(){
		GUI.Label (new Rect(10,0,500,20), "down: " + down.Count + ", taps: " + taps.Count + ", last position: " + lastPosition + " - " + lastLastPosition);
		GUI.Label (new Rect (10, 20, 200, 20), "occured: " + occurred);
		if (occurred) {
			GUI.Label (new Rect (10, 40, 200, 20), "position: " + tap.position);
		}
	}

	bool ResolveDoubleTap (Touch newTap){
		lastLastPosition = lastPosition;
		lastPosition = newTap.position;
		foreach(var t in taps){
			Debug.Log ("p1: " + t.position + ", p2: " + newTap.position);
			if (Vector2.Distance (t.position, newTap.position) < doubleTapDistance) {
				//DOUBLE TAP OCCURRED
				occurred = true;
				tap = newTap;
				taps.Remove (t);

				Debug.Log("DoubleTapped, position = " + tap.position);

				return true;
			}
		}
		return false;
	}

	public static void Resolve(){
		if (singleton != null) {
			singleton.Update ();
		} else {
			Debug.LogError ("DoubleTap singleton Not Found. Did you place an DoubleTap Object on the scene?");
		}
	}
	void Update(){
		if (lastUpdateTime == Time.time)
			return;
		var now = lastUpdateTime = Time.time;
		occurred = false;

		//Remove old taps
		for (int i = taps.Count - 1; i >= 0; i--) {
			if (now - taps [i].deltaTime > tapTime) {
				taps.RemoveAt (i);
			}
		}

		//handle new touch
		foreach (var it in Input.touches) {
			var t = it;
			if (t.phase == TouchPhase.Began) {
				t.deltaTime = Time.time;// save began time
				if (!ResolveDoubleTap (t)) {
					down.Add (t);
				}
			}
			if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled) {
				var i = down.FindIndex (x => x.fingerId == t.fingerId);
				if (i > -1) {
					var d = down [i];
					down.RemoveAt (i);
					if (t.phase == TouchPhase.Ended && Time.time - d.deltaTime < tapTime) {
						t.deltaTime = Time.time;
						taps.Add (t);
					}
				}
			}
		}

//		{
//			var t = new Touch();
//			t.position = Input.mousePosition;
//			if (Input.GetMouseButtonDown (0)) {
//				t.phase = TouchPhase.Began;
//			} else if (Input.GetMouseButtonUp (0)) {
//				t.phase = TouchPhase.Ended;
//			} else {
//				t.phase = TouchPhase.Moved;
//			}
//
//			if (t.phase == TouchPhase.Began) {
//				t.deltaTime = Time.time;// save began time
//				if (!ResolveDoubleTap (t)) {
//					down.Add (t);
//				}
//			}
//			if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled) {
//				var i = down.FindIndex (x => x.fingerId == t.fingerId);
//				if (i > -1) {
//					var d = down [i];
//					down.RemoveAt (i);
//					if (t.phase == TouchPhase.Ended && Time.time - d.deltaTime < tapTime) {
//						t.deltaTime = Time.time;
//						taps.Add (t);
//					}
//				}
//			}
//		}
	}

	List<Touch> taps = new List<Touch> ();
	List<Touch> down = new List<Touch> ();

	public static bool occurred;
	public static Touch tap;
}