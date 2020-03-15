using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [Header("Object References")]
	public Gun                      gun1;
	public Gun                      gun2;
    private Camera                  viewCamera;

	[Header("Audio")]
	public AudioClip				shootAudioClip;

    public float cooldownTime = 0.25f;
    private bool isActive = true;

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
        if (isActive && Input.GetMouseButtonDown(0))
        {
			gun1.Shoot();
			gun2.Shoot();
			AudioSource.PlayClipAtPoint(shootAudioClip, Camera.main.transform.position);
            isActive = false;
            StartCoroutine(WaitCooldown());
		}
	}

    IEnumerator WaitCooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        isActive = true;
    }
}
