﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinding : MonoBehaviour {

	public Transform seeker, target;
	NodeGrid grid;

	int GetDistance(Node nodeA, Node nodeB) {
		int dX = Mathf.Abs (nodeA.gridX - nodeB.gridX), dY = Mathf.Abs (nodeA.gridY - nodeB.gridY);

		if (dX > dY)
			return 14 * dY + 10 * (dX - dY);
		return 14 * dX + 10 * (dY - dX);
	}

	void Awake() {
		grid = GetComponent<NodeGrid> ();
	}

	void Update() {
		FindPath (seeker.position, target.position);
	}

	void FindPath (Vector3 startPos, Vector3 targetPos) {
		Node startNode = grid.NodeFromWorldPoint (startPos), targetNode = grid.NodeFromWorldPoint (targetPos);
		List<Node> openSet = new List<Node> ();
		HashSet<Node> closedSet = new HashSet<Node> ();

		openSet.Add(startNode);

		while (openSet.Count > 0) {
			Node node = openSet[0];

			for (int i = 1; i < openSet.Count; i ++) {
				if (openSet [i].fCost <= node.fCost && openSet [i].hCost < node.hCost)
					node = openSet [i];
			}

			openSet.Remove(node);
			closedSet.Add(node);

			if (node == targetNode) {
				RetracePath (startNode, targetNode);
				return;
			}

			foreach (Node neighbour in grid.GetNeighbours(node)) {
				if (!neighbour.walkable || closedSet.Contains(neighbour))
					continue;

				int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);

				if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
					neighbour.gCost = newCostToNeighbour;
					neighbour.hCost = GetDistance(neighbour, targetNode);
					neighbour.parent = node;

					if (!openSet.Contains(neighbour))
						openSet.Add(neighbour);
				}
			}
		}
	}

	void RetracePath(Node startNode, Node endNode) {
		List<Node> path = new List<Node> ();
		Node currentNode = endNode;

		while (currentNode != startNode) {
			path.Add (currentNode);
			currentNode = currentNode.parent;
		}

		path.Reverse();

		grid.path = path;

	}
}