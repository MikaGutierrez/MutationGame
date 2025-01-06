using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : Audio
{
    public GameObject HintButton;
    public GameObject Hint1;
    public GameObject Hint2;
    public GameObject Record;
    public GameObject CreditPaper;

    public Text TextTimerTimer;
    public Text TextRoomTimer;
    public Text TextGenesTimer;

    public Text TextTimerRoom;
    public Text TextRoomRoom;
    public Text TextGenesRoom;

    public Text TextTimerGenes;
    public Text TextRoomGenes;
    public Text TextGenesGenes;

    // Start is called before the first frame update

    public void Start()
    {
        CreditPaper.SetActive(false);
        Record.SetActive(false);
        Hint1.SetActive(false); Hint2.SetActive(false);
    }

    public void Update()
    {
        TextTimerTimer.text = CharacterMovement.TimerTimer + "";
        TextRoomTimer.text = CharacterMovement.RoomTimer + "";
        TextGenesTimer.text = CharacterMovement.GenesTimer + "";

        TextTimerRoom.text = CharacterMovement.TimerRoom + "";
        TextRoomRoom.text = CharacterMovement.RoomRoom + "";
        TextGenesRoom.text = CharacterMovement.GenesRoom + "";

        TextTimerGenes.text = CharacterMovement.TimerGenes + "";
        TextRoomGenes.text = CharacterMovement.RoomGenes + "";
        TextGenesGenes.text = CharacterMovement.GenesGenes + "";
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
        PlaySounds(audioClips[0], p1: 0.8f, p2: 1.2f);
        HintButton.SetActive(false);
        Hint2.SetActive(false);
        Hint1.SetActive(true);
    }
    public void Hint1ArrowClick()
    {
        PlaySounds(audioClips[0], p1: 0.8f, p2: 1.2f);
        Hint2.SetActive(true);
        Hint1.SetActive(false);
    }
    public void Hint2ArrowClick()
    {
        PlaySounds(audioClips[0], p1: 0.8f, p2: 1.2f);
        Hint1.SetActive(true);
        Hint2.SetActive(false);
    }
    public void HintClickAnywere()
    {
        PlaySounds(audioClips[0], p1: 0.8f, p2: 1.2f);
        Hint1.SetActive(false);
        Hint2.SetActive(false);
        HintButton.SetActive(true);
    }
    public void CreditsClick()
    {
        PlaySounds(audioClips[0], p1: 0.8f, p2: 1.2f);
        CreditPaper.SetActive(true);
    }
    public void CreditsClick2()
    {
        PlaySounds(audioClips[0], p1: 0.8f, p2: 1.2f);
        CreditPaper.SetActive(false);
    }
    public void RecordClick()
    {
        PlaySounds(audioClips[0], p1: 0.8f, p2: 1.2f);
        Record.SetActive(true);
    }
    public void RecordClouse()
    {
        PlaySounds(audioClips[0], p1: 0.8f, p2: 1.2f);
        Record.SetActive(false);
    }
}