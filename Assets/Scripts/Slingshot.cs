using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {

	public GameObject prefabBomb;
	public float velocityMultiplier;

	// User can edit the fields above
	public bool _______________;
	// User should not edit the fields below

	public GameObject launchPoint;
	public Vector3 launchPosition;
	public GameObject bomb;
	public bool aimingMode;

	void Update() {
		if (!aimingMode)
			return;

		// Get mouse position and 
		Vector3 mousePosition2D = Input.mousePosition;
		mousePosition2D.z = -Camera.main.transform.position.z;
		Vector3 mousePosition3D = Camera.main.ScreenToWorldPoint (mousePosition2D);

		// Distance between launchposition and mouseposition
		Vector3 mouseDelta = mousePosition3D - launchPosition;

		float maxMagnitude = this.GetComponent<SphereCollider> ().radius;

		if (mouseDelta.magnitude > maxMagnitude) 
		{
			mouseDelta.Normalize ();
			mouseDelta = mouseDelta * maxMagnitude;
		}

		Vector3 bombPosition = launchPosition + mouseDelta;
		bomb.transform.position = bombPosition;
		if (Input.GetMouseButtonUp(0)) 
		{
			aimingMode = false;
			bomb.GetComponent<Rigidbody> ().isKinematic = false;
			bomb.GetComponent<Rigidbody> ().velocity = -mouseDelta * velocityMultiplier;

			FollowCamera.S.poi = bomb;

			bomb = null;
		}
	}

	void OnMouseDown() {
		aimingMode = true;
		bomb = Instantiate (prefabBomb) as GameObject;
		bomb.transform.position = launchPosition;
		bomb.GetComponent<Rigidbody>().isKinematic = true;
	}

	void Awake() {
		Transform launchPointTransform = transform.Find ("LaunchPoint");
		launchPoint = launchPointTransform.gameObject;
		launchPoint.SetActive (false);
		launchPosition = launchPointTransform.position;

	}

	void OnMouseEnter() {
		launchPoint.SetActive (true);
	}

	void OnMouseExit() {
		launchPoint.SetActive (false);
	}


}
