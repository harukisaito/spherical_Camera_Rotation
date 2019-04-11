using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

	[SerializeField] private Transform target;
	[SerializeField] private float zoomSensitivity = 0.003f;

	private Vector3 zoomAmount;
	private float distanceToTarget;
	private float zoom = 1f;

	private void Awake() {
		transform.SetParent(target);
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z * target.parent.localScale.z);
		distanceToTarget = transform.localPosition.z;
		SetZoom(zoom);
	}

	private void LateUpdate() {
		if(target.localScale.x + 1 < Mathf.Abs(zoomAmount.z) && target.localScale.y + 1 < Mathf.Abs(zoomAmount.z) && target.localScale.z + 1 < Mathf.Abs(zoomAmount.z)) {
			if(Input.mouseScrollDelta.y < 0) {
				zoom -= zoomSensitivity * Mathf.Abs(transform.localPosition.z);
				SetZoom(zoom);
			}
		}
		if(Input.mouseScrollDelta.y > 0) {
			zoom += zoomSensitivity * Mathf.Abs(transform.localPosition.z);
			SetZoom(zoom);
		}
		transform.LookAt(target);
	}

	private void SetZoom(float zoomLevel) {
		zoomAmount = new Vector3(
			transform.localPosition.x,
			transform.localPosition.y,
			distanceToTarget * zoomLevel);
		transform.localPosition = zoomAmount;
	}
}
