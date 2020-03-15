using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthPowerupBehaviour : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip				        destroyedAudioClip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Bullet")
        {
            AudioSource.PlayClipAtPoint(destroyedAudioClip, Camera.main.transform.position);
            GameControl.Instance.AddHealth(1f);
            gameObject.SetActive(false);
        }


    }

}