using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIPathNode : MonoBehaviour {

	public enum NodeType {Wall, Jump, Move, Slide, Stomp, Stop, AI, Checkpoint};

	public NodeType type;
	public bool visible;
	public AIPathNode wallJumpTarget;
	Renderer r;

	AIPathNode parent;
	public AIPathNode Parent {
		get { return parent; }
		set { parent = value; }
	}
	float g, h, f;
	public float G {
		get { return g; }
		set { g = value; }
	}
	public float H {
		get { return h; }
		set { h = value; }
	}
	public float F {
		get { return f; }
		set { f = value; }
	}

	void Start () {
		r = this.GetComponent<Renderer> ();

		if (visible) {
			if (type == NodeType.Move || type == NodeType.Slide) {
				MoveToGroundHeight ();
				r.material.color = Color.blue;
			}
			else if (type == NodeType.Jump || type == NodeType.Stop) {
				MoveToGroundHeight ();
				r.material.color = Color.red;
			}
			else {	// Checkpoints, Walls, and Stomps
				r.material.color = Color.yellow;
			}
		} else {
			r.enabled = false;
			if (type == NodeType.Move || type == NodeType.Slide || type == NodeType.Jump) {
				MoveToGroundHeight ();
			}
		}
		
	}

	void MoveToGroundHeight () {
		Vector3 groundHit = NodeList.GetGroundHit (this.transform.position);
		this.transform.position = new Vector3 (this.transform.position.x, groundHit.y + AIPathing.characterRadius, this.transform.position.z);
	}
}
