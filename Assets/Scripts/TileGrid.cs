using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//http://answers.unity3d.com/questions/474418/make-grid-and-snap-to-it-in-unity-editor-2d-game.html

[ExecuteInEditMode]
public class TileGrid : MonoBehaviour {
	public float snapValue;
	public float depth = 0;    

	void Update() {
		float snapInverse = 1/snapValue;

		float x, y, z;

		// if snapValue = .5, x = 1.45 -> snapInverse = 2 -> x*2 => 2.90 -> round 2.90 => 3 -> 3/2 => 1.5
		// so 1.45 to nearest .5 is 1.5
		x = Mathf.Round(transform.localPosition.x * snapInverse)/snapInverse;
		y = Mathf.Round(transform.localPosition.y * snapInverse)/snapInverse;   
		z = depth;  // depth from camera

		transform.localPosition = new Vector3(x, y, z);
	}
}