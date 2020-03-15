using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [Header("Settings")]
    public float                    damageTakenTime;
    public Material                 damageTakenMaterial;
    public GameObject               healthBar;

    [Header("Audio")]
    public AudioClip				damageTakenAudioClip;

    public void IndicateDamageTaken()
    {
        var indicateDamageCR = StartCoroutine(CRIndicateDamageTaken());
        StartCoroutine(HealthBarIndicateDamageTaken());
    }

    private IEnumerator CRIndicateDamageTaken()
    {
        Material originalMaterial = GetComponent<MeshRenderer>().material;

        GetComponent<MeshRenderer>().material = damageTakenMaterial;

        AudioSource.PlayClipAtPoint(damageTakenAudioClip, Camera.main.transform.position);

        yield return new WaitForSeconds(damageTakenTime);

        GetComponent<MeshRenderer>().material = originalMaterial;
    }

    private IEnumerator HealthBarIndicateDamageTaken()
    {
        RectTransform rt = healthBar.GetComponent<RectTransform>();
        for (int i = 0; i < 20; i++)
        {
            rt.localScale = new Vector3(rt.localScale.x * 1.02f, rt.localScale.y * 1.04f, rt.localScale.z);
            if (i < 10)
            {
                rt.Rotate(new Vector3(0, 0, 5));
            } else
            {
                rt.Rotate(new Vector3(0, 0, -5));
            }
            yield return null;
        }
        for (int i = 0; i < 20; i++)
        {
            rt.localScale = new Vector3(rt.localScale.x / 1.02f, rt.localScale.y / 1.04f, rt.localScale.z);
            if (i < 10)
            {
                rt.Rotate(new Vector3(0, 0, 2));
            }
            else
            {
                rt.Rotate(new Vector3(0, 0, -2));
            }
            yield return null;
        }
    }
}
