using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

	static public FollowCamera S;
	public float easing = 0.05f;
	public Vector2 minXY;


	public bool ____________;

	// poi is the bomb. Object to follow
	public GameObject poi;
	public float cameraZposition;

	void Awake() {
		S = this;
		cameraZposition = this.transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		if (poi == null)
			return;

		Vector3 destination = poi.transform.position;

		destination.x = Mathf.Max (minXY.x, destination.x);
		destination.y = Mathf.Max (minXY.y, destination.y);

		destination = Vector3.Lerp (transform.position, destination, easing);
		destination.z = cameraZposition;
		transform.position = destination;

		this.GetComponent<Camera>().orthographicSize = destination.y + 10;
	}
}
