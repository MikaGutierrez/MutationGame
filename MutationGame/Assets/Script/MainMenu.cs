using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject HintButton;
    public GameObject Hint1;
    public GameObject Hint2;
    public GameObject CreditPaper;
    // Start is called before the first frame update

    public void Start()
    {
        CreditPaper.SetActive(false);
        Hint1.SetActive(false); Hint2.SetActive(false);
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
    public void HintButtonClick()
    {
        HintButton.SetActive(false);
        Hint2.SetActive(false);
        Hint1.SetActive(true);
    }
    public void Hint1ArrowClick()
    {
        Hint2.SetActive(true);
        Hint1.SetActive(false);
    }
    public void Hint2ArrowClick()
    {
        Hint1.SetActive(true);
        Hint2.SetActive(false);
    }
    public void HintClickAnywere()
    {
        Hint1.SetActive(false);
        Hint2.SetActive(false);
        HintButton.SetActive(true);
    }
    public void CreditsClick()
    {
        CreditPaper.SetActive(true);
    }
    public void CreditsClick2()
    {
        CreditPaper.SetActive(false);
    }
}
