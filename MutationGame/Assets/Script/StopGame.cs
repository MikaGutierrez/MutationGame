using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StopGame : MonoBehaviour
{
    private bool IsGameStopped;
    public GameObject StopPanel;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        StopPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape") && Player.GetComponent<CharacterMovement>().IsDead == false)
        {
            if (IsGameStopped == false)
            {
                StopPanel.SetActive(true);
                IsGameStopped = true;
                Time.timeScale = 0f;
            }
            else if (IsGameStopped == true)
            {
                StopPanel.SetActive(false);
                Time.timeScale = 1f;
                IsGameStopped = false;
            }
        }
    }
    public void ContinueClick()
    {
            StopPanel.SetActive(false);
            Time.timeScale = 1f;
            IsGameStopped = false;
    }
    public void ExitClick()
    {
        Application.Quit();
        Time.timeScale = 1f;
    }

    public void StartClick()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1f;
    }
    public void BackClick()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
}
