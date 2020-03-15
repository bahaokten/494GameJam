using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy2Behavior : MonoBehaviour
{
    [Header("Prefab References")]
    public GameObject                       enemyDeathEffect;
    public GameObject                       enemyCanvas;
    public GameObject                       beam;

    [Header("Settings")]
    public float                            speed = 1;

    [Header("Data")]
    public int                              enemyID = 0;

    [Header("Audio")]
    public AudioClip				        destroyedAudioClip;

    private State                           currentState = State.countdown;
    private Vector3                         speedVector;

    enum State
    {
        countdown,
        shoot,
        inactive
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(Vector3.zero, Vector3.up);
        transform.Rotate(new Vector3(0,-90,0));
        speedVector = speed * (new Vector3(-transform.position.x, 0, -transform.position.z)).normalized;
        StartCoroutine(DoCountdownToShoot());
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == State.shoot)
        {
            transform.position += speedVector;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentState != State.shoot)
        {
            return;
        }
        if (other.transform.tag == "Player")
        {
            currentState = State.inactive;
            SpawnDeathEffect();
            GameControl.Instance.DeductHealth(1f);

            if (null != other.GetComponent<PlayerBehavior>())
            {
                other.GetComponent<PlayerBehavior>().IndicateDamageTaken();
            }
            
            Destroy(gameObject);
        }
    }

    void SpawnDeathEffect()
    {
        GameObject deathEffect = Instantiate(enemyDeathEffect);
        deathEffect.transform.position = transform.position;
    }

    IEnumerator DoCountdownToShoot()
    {
        yield return new WaitForSeconds(1);
        enemyCanvas.GetComponentInChildren<Text>().text = "2";
        yield return new WaitForSeconds(1);
        enemyCanvas.GetComponentInChildren<Text>().text = "1";
        yield return new WaitForSeconds(1);
        enemyCanvas.GetComponentInChildren<Text>().enabled = false;
        enemyCanvas.GetComponentInChildren<Image>().enabled = false;
        currentState = State.shoot;
        beam.SetActive(true);
        StartCoroutine(DoCountDownToDespawn());
    }

    IEnumerator DoCountDownToDespawn()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
