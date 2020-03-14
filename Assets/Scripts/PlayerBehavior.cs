using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [Header("Settings")]
    public float                    damageTakenTime;
    public Material                 damageTakenMaterial;

    public void IndicateDamageTaken()
    {
        var indicateDamageCR = StartCoroutine(CRIndicateDamageTaken());
    }

    private IEnumerator CRIndicateDamageTaken()
    {
        Material originalMaterial = GetComponent<MeshRenderer>().material;

        GetComponent<MeshRenderer>().material = damageTakenMaterial;

        yield return new WaitForSeconds(damageTakenTime);

        GetComponent<MeshRenderer>().material = originalMaterial;
    }
}
