using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [Header("Settings")]
    public float                    damageTakenTime;
    public Material                 damageTakenMaterial;

    [Header("Audio")]
    public AudioClip				damageTakenAudioClip;

    public void IndicateDamageTaken()
    {
        var indicateDamageCR = StartCoroutine(CRIndicateDamageTaken());
    }

    private IEnumerator CRIndicateDamageTaken()
    {
        Material originalMaterial = GetComponent<MeshRenderer>().material;

        GetComponent<MeshRenderer>().material = damageTakenMaterial;

        AudioSource.PlayClipAtPoint(damageTakenAudioClip, Camera.main.transform.position);

        yield return new WaitForSeconds(damageTakenTime);

        GetComponent<MeshRenderer>().material = originalMaterial;
    }
}
