using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIPathing : MonoBehaviour {
	public static float characterRadius = .5f;
	static Vector3 jumpvector;
	public static Vector3 JumpVector {
		get { return jumpvector; }
	}

	GameObject nodeListObject;

	Vector3 target;
	LinkedList<AIPathNode> subTargets;

	AIPathNode aiPlayerNode;
	AIPathNode targetNode;

	AIController control;

	public float jumpHeight = 9f;
	public float slideDuration = 0.5f;

	Vector2 aiFlatPos;
	Vector2 nodeFlatPos;

	// Use this for initialization
	void Start () {
		// Debug
		target = GameObject.Find ("Target").transform.position;
		targetNode = GameObject.Find ("Target").transform.FindChild ("TargetNode").GetComponent<AIPathNode> ();
		NodeList.Nodes.Add (targetNode);
		// ------------------------------
		characterRadius = 1f;
		jumpvector = new Vector3 (0f, jumpHeight, 0f);
		control = this.GetComponent<AIController> ();
		dash = this.GetComponent<BurstDash> ();
		// subTargets = new LinkedList<Vector3> ();
		nodeListObject = GameObject.Find ("NodeList");

		aiPlayerNode = this.transform.FindChild ("PlayerNode").GetComponent<AIPathNode> ();
		// StartCoroutine (CreatePath ());

		Invoke ("ChangeTarget", 6.0f);
	}
	
	void ChangeTarget () {
		// target = t;
		//subTargets.Clear ();
		StopCoroutine (FollowPath ());
		subTargets = GetPath (target);
		DrawPath ();
		StartCoroutine (FollowPath ());
	}

	bool followingPath = false, stopping = false;
	LinkedListNode<AIPathNode> currentNode;
	float jumpDistance = 8f, currentDistance, lastDistance, nodeDistance, nextNodeDistance;
	IEnumerator FollowPath () {
		currentNode = subTargets.First;
		followingPath = true;
		while (followingPath) {
			Debug.DrawLine (this.transform.position, currentNode.Value.transform.position, Color.white);
			timeSinceAdvance += Time.deltaTime;
			if (timeSinceAdvance >= 10f) {
				NodeAdvance ();
			}

			// Stall prevention
			lastDistance = currentDistance;
			currentDistance = Vector3.Distance (this.transform.position, currentNode.Value.transform.position);
			if (StallCheck ()) {
				print ("STALLED");
				control.Jump ();
			}

			// Movement
			if (currentNode.Value.type != AIPathNode.NodeType.Wall) {
				control.MoveToward (currentNode.Value.transform.position);
			} else if (currentNode.Previous != null && currentNode.Previous.Value.type != AIPathNode.NodeType.Wall) {
				control.MoveToward (currentNode.Value.transform.position);
			} else {
				control.MoveFromWall ();
			}

			// Node progression
			if (!stopping) {
				if (currentNode == subTargets.Last) {
					nextNodeDistance = Mathf.Infinity;
					nodeDistance = Mathf.Infinity;
					if (currentDistance <= characterRadius*3) {
						ChangeTarget ();
					}
				}
				else {
					nextNodeDistance = Vector3.Distance (this.transform.position, currentNode.Next.Value.transform.position);
					nodeDistance = Vector3.Distance (currentNode.Value.transform.position, currentNode.Next.Value.transform.position);
				}


				aiFlatPos = new Vector2 (this.transform.position.x, this.transform.position.z);
				nodeFlatPos = new Vector2 (currentNode.Value.transform.position.x, currentNode.Value.transform.position.z);

				float flatDist = Vector2.Distance (aiFlatPos, nodeFlatPos);

				if (currentNode != subTargets.Last) {
					if ((currentNode.Next.Value.type != AIPathNode.NodeType.Wall && nextNodeDistance < nodeDistance) || (flatDist <= characterRadius*3)) {
						NodeAdvance ();
					}

					if (currentNode.Value.type == AIPathNode.NodeType.Wall) {
						Vector3 jumpTargetPos = currentNode.Value.wallJumpTarget.transform.position;
						if (this.transform.position.y > jumpTargetPos.y) {
							while (currentNode.Value.wallJumpTarget != null) {
								currentNode = currentNode.Next;
							}
						}
					}
				}
			} else {    // if stopping
				NodeAdvance ();
			}

			yield return null;
		}
	}

	int concurrentStalls = 0;
	bool StallCheck () {
		if (Mathf.Abs (currentDistance - lastDistance) <= .0001f) { // check float equivalence
			++concurrentStalls;
			if (concurrentStalls >= 10) {
				return true;
			}
		} else {
			concurrentStalls = 0;
		}
		return false;
	}

	float timeSinceAdvance = 0f;
	void NodeAdvance () {
		timeSinceAdvance = 0f;
		bool stop = false;
		if (currentNode == subTargets.Last) {
			followingPath = false;
		} else {
			if (currentNode.Value.type != AIPathNode.NodeType.Stop) {
				if (currentNode.Value.type != AIPathNode.NodeType.Move) {
					this.transform.position = currentNode.Value.transform.position;
					currentNode = currentNode.Next;
				} else {
					currentNode = currentNode.Next;
				}
				/*this.transform.position = currentNode.Value.transform.position;
				currentNode = currentNode.Next;*/

			} else {
				stop = true;
				stopping = true;
			}
		}

		if (stop) {
			if (control.CurrentSpeed <= 5f) {
				stopping = false;
				this.transform.position = currentNode.Value.transform.position;
				currentNode = currentNode.Next;
			}

			return;
		}

		if (currentNode.Previous.Value.type == AIPathNode.NodeType.Jump) {
			control.Jump ();
			Invoke ("AttemptBurstDash", dashDelay);
			print ("Node Jump");
		}
		else if (currentNode.Previous.Value.type == AIPathNode.NodeType.Slide) {
			control.Slide (slideDuration);
			print ("SLIP AND SLIIIIDE");
		}
		else if (currentNode.Previous.Value.type == AIPathNode.NodeType.Stomp) {
			control.Stomp ();
			print ("STOMP");
		}
	}

	BurstDash dash;
	public float dashDelay, dashLength;
	void AttemptBurstDash () {
		if (Vector2.Distance (aiFlatPos, nodeFlatPos) >= dashLength) {
			control.MoveToward (currentNode.Value.transform.position);
			dash.Dash ();
		}
	}

	bool ShouldJump (Vector3 currentNode) {
		float nodeDist = Vector3.Distance (this.transform.position, currentNode);
		Vector3 cHit = NodeList.ClosestHit (this.transform.position, currentNode - this.transform.position, nodeDist);
		if (cHit.x != Mathf.Infinity) { // Does not have LOS
			if (Vector3.Distance (this.transform.position, cHit) <= jumpDistance && Vector3.Distance (this.transform.position, cHit) > 5f) {
				print (Vector3.Distance (this.transform.position, cHit));
				Debug.DrawRay (this.transform.position,currentNode-this.transform.position, Color.red, 10f);
				return true;
			}
		} else {
			if (NeedsToJump (currentNode) && nodeDist <= jumpDistance) {
				print ("NEEDS JUMP");
				return true;
			}
		}

		return false;
	}

	float jumpHeightDiff = characterRadius*3f;
	bool NeedsToJump (Vector3 currentNode) {
		float heightDiff = currentNode.y - this.transform.position.y;
		return heightDiff > jumpHeightDiff;
	}

	bool NeedsToJump (Vector3 currentNode, Vector3 nextNode) {
		float heightDiff = nextNode.y - currentNode.y;
		return heightDiff > jumpHeightDiff;
	}

	bool NextNodeWithinHeight (Vector3 currentNode, Vector3 nextNode) {
		float heightDiff = nextNode.y - currentNode.y;
		return heightDiff <= jumpHeight;
	}

	bool NodePositionsSame (AIPathNode curr, AIPathNode iter) {
		if (curr.transform.position.x == iter.transform.position.x && curr.transform.position.z == iter.transform.position.z) {
			return true;
		}

		return false;
	}

	LinkedList<AIPathNode> GetPath (Vector3 target) {
		List<AIPathNode> open = new List<AIPathNode> ();
		LinkedList<AIPathNode> closed = new LinkedList<AIPathNode> ();

		AIPathNode currentNode = aiPlayerNode;
		closed.AddFirst (currentNode);

		AIPathNode iterNode;
		while (closed.Last.Value != targetNode) {
			AIPathNode closestNode = null;
			float closestDist = Mathf.Infinity;
			for (int i = 0; i < NodeList.Nodes.Count; ++i) {
				iterNode = NodeList.Nodes [i];
				if (iterNode != currentNode && !closed.Contains (iterNode) && !NodePositionsSame (currentNode, iterNode) &&
					(iterNode != targetNode || closed.Count > 5)) {
					float dist = Vector3.Distance (currentNode.transform.position, iterNode.transform.position);
					if (dist < closestDist) {
						closestNode = iterNode;
						closestDist = dist;
					}
				}
			}

			closed.AddLast (closestNode);
			currentNode = closestNode;
		}

		closed.RemoveFirst ();

		return closed;

		/*ClearHeuristics ();
		open.Add (aiPlayerNode);

		AIPathNode lowestF = aiPlayerNode;
		
		while (!closed.Contains (targetNode) && open.Count > 0) {
			CalculateHeuristics (open, target);
			bool noLowest = true;
			for (int i = 0; i < open.Count; ++i) {
				if (noLowest || open[i].F < lowestF.F) {
					lowestF = open[i];
					noLowest = false;
				}
			}

			closed.AddLast (lowestF);
			open.Remove (lowestF);

			print ("Node iterated on: " + lowestF);

			AIPathNode iterNode;
			for (int i = 0; i < NodeList.Nodes.Count; ++i) {
				iterNode = NodeList.Nodes [i];

				if (closed.Contains (iterNode) ||
					iterNode == lowestF ||
					!StraightPathPossible (lowestF.transform.position, iterNode.transform.position) ||
					!NextNodeWithinHeight (lowestF.transform.position, iterNode.transform.position) ||
					NextNodeStraightAbove (lowestF.transform.position, iterNode.transform.position)) {
					continue;
				}

				// print ("-----LOS node: " + iterNode);

				if (iterNode == targetNode) {
					closed.AddLast (iterNode);
					iterNode.Parent = lowestF;
				} else if (open.Contains (iterNode)) {
					float newG = lowestF.G + Vector3.Distance (iterNode.transform.position, lowestF.transform.position);
					if (newG < iterNode.G) {
						iterNode.Parent = lowestF;
						iterNode.G = newG;
						iterNode.F = iterNode.G + iterNode.H;
					}
				} else {
					open.Add (iterNode);
					iterNode.Parent = lowestF;
				}
			}
		}

		if (closed.Count == 0) {
			return new LinkedList<AIPathNode> ();
		}

		LinkedList<AIPathNode> output = new LinkedList<AIPathNode> ();
		AIPathNode next = closed.Last.Value;
		while (next != aiPlayerNode) {
			output.AddFirst (next);
			next = next.Parent;
		}


		return output;
		*/
	}

	bool StraightPathPossible (Vector3 v1, Vector3 v2) {
		if ((!NodeList.HasLOS (v1, v2)) && (!NodeList.HasLOS (v1 + jumpvector, v2))) {
			return false;
		} else {
			// height check
			return true;
		}
	}

	bool NextNodeStraightAbove (Vector3 v1, Vector3 v2) {
		if (v1.x == v2.x && v1.z == v2.z) {
			return true;
		}

		return false;
	}

	void CalculateHeuristics (List<AIPathNode> open, Vector3 target) {
		foreach (AIPathNode n in open) {
			n.G = Vector3.Distance (open [0].transform.position, n.transform.position);
			n.H = Vector3.Distance (target, n.transform.position);
			n.F = n.G + n.H;
		}
	}

	void ClearHeuristics () {
		for (int i = 0; i < NodeList.Nodes.Count; ++i) {
			NodeList.Nodes [i].F = 0;
			NodeList.Nodes [i].G = 0;
			NodeList.Nodes [i].H = 0;
			NodeList.Nodes [i].Parent = null;
		}
	}

	void DrawPath () {
		LinkedListNode<AIPathNode> currentNode = subTargets.First;
		Debug.DrawLine (currentNode.Value.transform.position, this.transform.position, Color.cyan, Mathf.Infinity);
		while (currentNode != subTargets.Last) {
			Debug.DrawLine (currentNode.Value.transform.position, currentNode.Next.Value.transform.position, Color.cyan, Mathf.Infinity);
			currentNode = currentNode.Next;
		}
	}

	// Update is called once per frame
	void Update () {
		/*subTargets = GetPath (target);
		DrawPath ();*/
		// ChangeTarget ();
	}
	
	// if n is higher, return positive
	float GetAIHeightDifference (Vector3 n) {
		return n.y - this.transform.position.y;
	}
}