using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [Header("Object References")]
	public Gun                      gun1;
	public Gun                      gun2;
    private Camera                  viewCamera;

	void Start()
	{
		viewCamera = Camera.main;
	}

	void Update()
	{
        if (GameControl.GAME_STATE != GameControl.eGameState.level) return;

		Vector3 mousePos = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, viewCamera.transform.position.y));
		transform.LookAt(mousePos + Vector3.up * transform.position.y);

        // Weapon
        if (Input.GetMouseButton(0))
        {
			gun1.Shoot();
			gun2.Shoot();
		}
	}
}
