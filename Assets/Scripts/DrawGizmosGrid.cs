using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class DrawGizmosGrid : MonoBehaviour {

	public Vector2 gridSize;
	public float cellSize;
	

    void OnDrawGizmos()
    {
		Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, gridSize.y, 1));

		Vector3 worldBottomLeft = transform.position - Vector3.right * gridSize.x / 2 - Vector3.up * gridSize.y / 2;

		for (int x = 0; x < gridSize.x; x++)
        {
			for (int y = 0; y < gridSize.y; y++)
            {
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * cellSize*2 + cellSize) + Vector3.up * (y * cellSize * 2 + cellSize);
				Gizmos.DrawWireCube(worldPoint, new Vector3(cellSize * 2, cellSize * 2, 1));
            }
        }
    }
}
