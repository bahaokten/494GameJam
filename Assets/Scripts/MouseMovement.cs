using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
	Camera viewCamera;

	void Start()
	{
		viewCamera = Camera.main;
	}

	void Update()
	{
		Vector3 mousePos = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, viewCamera.transform.position.y));
		transform.LookAt(mousePos + Vector3.up * transform.position.y);
	}
}
