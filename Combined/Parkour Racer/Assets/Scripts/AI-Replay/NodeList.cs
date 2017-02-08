using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeList : MonoBehaviour {

	static List<AIPathNode> nodeList;
	public static List<AIPathNode> Nodes {
		get { return nodeList; }
	}

	// Use this for initialization
	void Awake () {
		nodeList = new List<AIPathNode> ();
		for (int i = 0; i < this.transform.childCount; ++i) {
			nodeList.Add (this.transform.GetChild(i).GetComponent<AIPathNode> ());
		}
	}

	public static bool HasLOS (Vector3 source, Vector3 t) {
		float distance = Vector3.Distance (source, t);
		Vector3 cHit = ClosestHit (source, t - source, distance);
		if (cHit.x == Mathf.Infinity) { // If no obstruction
			return true;
		} else {
			// CreateHitmarker (cHit);
			return false;
		}
	}

	public static Vector3 GetGroundHit (Vector3 v) {
		return ClosestHit (v, Vector3.down, 10000f);
	}

	public static Vector3 ClosestHit (Vector3 origin, Vector3 direction, float rayDistance) {
		int racerLayerMask = 1 << 8;
		racerLayerMask = ~racerLayerMask;
		RaycastHit hit = new RaycastHit ();
		bool hitSomething = Physics.Raycast (origin, direction, out hit, rayDistance, racerLayerMask, QueryTriggerInteraction.Ignore);
		// Debug.DrawRay (origin, direction * rayDistance, Color.green, .5f);

		Vector3 hitPosition;
		if (hitSomething) {
			hitPosition = hit.point;
		} else {
			hitPosition = new Vector3 (Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
		}

		return hitPosition;
	}
}
