using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [Header("User Interface")]
    public Image                    healthBarFill;

    private void Start()
    {
        healthBarFill.fillAmount = 1f;
    }

    private void OnEnable()
    {
        GameControl.HEALTH_CHANGE_DELEGATE += UpdateHealth;
    }

    private void OnDisable()
    {
        GameControl.HEALTH_CHANGE_DELEGATE -= UpdateHealth;
    }

    public void StartGame()
    {
        GameControl.GAME_STATE = GameControl.eGameState.level;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateHealth()
    {
        healthBarFill.fillAmount = GameControl.Instance.health / GameControl.Instance.initialHealth;
    }

}
