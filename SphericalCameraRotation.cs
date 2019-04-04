using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphericalCameraRotation : MonoBehaviour {
	[SerializeField] private float sensitivityX = 2f;
	[SerializeField] private float sensitivityY = 2f;

	private bool isDragging = false;

	private Vector3 theSpeed;
	private Vector3 avgSpeed;


	private void LateUpdate() {
		if(Input.GetMouseButtonDown(0)) {
			isDragging = true;
		}
		if (Input.GetMouseButton(0) && isDragging) {
			theSpeed = new Vector3(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"), 0.0F);
			avgSpeed = Vector3.Lerp(avgSpeed, theSpeed, Time.deltaTime * 5);
		} else {
			if (isDragging) {
				theSpeed = avgSpeed;
				isDragging = false;
			}
			theSpeed = Vector3.Lerp(theSpeed, Vector3.zero, Time.deltaTime);
		}

		transform.Rotate(Camera.main.transform.up * theSpeed.x * sensitivityX, Space.World);
		transform.Rotate(Camera.main.transform.right * theSpeed.y * sensitivityY, Space.World);

		Vector3 rotation = transform.localRotation.eulerAngles;

		float max = 70f;
		float min = 310f;

		rotation.z = Mathf.Clamp(rotation.z, 1, 3);
		transform.localRotation = Quaternion.Euler(rotation);
		if(rotation.x > max && rotation.x < 90) {
			rotation.x = max;
			transform.localRotation = Quaternion.Euler(rotation);
		}
		else if (380 - rotation.x > max && rotation.x < 360 && rotation.x > 300) {
			rotation.x = min;
			transform.localRotation = Quaternion.Euler(rotation);
		}
	}
}

