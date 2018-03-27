using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGrid : MonoBehaviour {
	// Contient tous les nodes.
	private Node[,] grid;
	// Les zones ou l'IA ne peut pas marcher.
	public LayerMask unwalkableMask;

	// La taille de la grille.
	public Vector2 gridWorldSize;
	// La taille de la grille en nodes.
	private int gridSizeX, gridSizeY;

	// Le rayon et le diamètre d'un node.
	public float nodeRadius;
	private float nodeDiameter;

	public bool drawNodes;

	// A partir d'un point, calcule le node correspondant.
	public Node NodeFromWorldPoint (Vector3 worldPosition) {
		float percentX = Mathf.Clamp01 ((worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x), percentY = Mathf.Clamp01 ((worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y);
		int x = Mathf.RoundToInt ((gridSizeX - 1) * percentX), y = Mathf.RoundToInt ((gridSizeY - 1) * percentY);

		return grid [x, y];
	}


	public List<Node> GetNeighbours (Node node) {
		List<Node> neighbours = new List<Node> ();

		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (x == 0 && y == 0)
					continue;

				int checkX = node.gridX + x, checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
					neighbours.Add (grid [checkX, checkY]);
			}
		}

		return neighbours;
	}

	void Awake () {
		nodeDiameter = nodeRadius * 2;

		gridSizeX = Mathf.RoundToInt (gridWorldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt (gridWorldSize.y / nodeDiameter);

		CreateGrid ();
	}

	// Créé la grille de node (pré-analyse de la carte).
	void CreateGrid () {
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

		grid = new Node[gridSizeX, gridSizeY];

		for (int x = 0; x < gridSizeX; x++) {
			for (int y = 0; y < gridSizeY; y++) {
				// La position du node.
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
				// Un élément non praticable (unwalkable) sur le node ?
				bool walkable = !(Physics.CheckSphere (worldPoint, nodeRadius, unwalkableMask));

				grid [x, y] = new Node (walkable, worldPoint, x, y);
			}
		}
	}
		

	public List<Node> path = new List<Node> ();
	void OnDrawGizmos () {
		// La taille de la grille (visualisable en éditeur).
		Gizmos.DrawWireCube (transform.position, new Vector3 (gridWorldSize.x, 1, gridWorldSize.y));

		// Les nodes: praticable en blanc, sinon en rouge (visualisable en playmode).
		if (grid != null && drawNodes) {
			foreach (Node n in grid) {
				Gizmos.color = (n.walkable) ? Color.white : Color.red;

				if (path != null && path.Contains (n)) {
					Gizmos.color = Color.black;
				}
				
				Gizmos.DrawCube (n.worldPosition, new Vector3 ((nodeDiameter - .1f), 0.2f, (nodeDiameter - .1f)));
			}
		}
	}
}