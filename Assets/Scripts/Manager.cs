using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
//using Assets.Scripts.GameEvents;
using Assets.Scripts.GameEventProt;

public class Manager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TextTime;
    [SerializeField] private TextMeshProUGUI TextTimeEnd;
    [SerializeField] private TextMeshProUGUI TextLife;

    [SerializeField] private TextMeshProUGUI TextScore;
    [SerializeField] private GameObject panelWinOrLose;
    [SerializeField] private GameObject panelPause;
    [SerializeField] private TextMeshProUGUI TextResult;
    [SerializeField] private TextMeshProUGUI AnotherTime;
    [SerializeField] private GameObject buttonP;

    //
   // [SerializeField] private GameEvent gameEventOnWin;
    //[SerializeField] private GameEvent gameEventOnLose;
    //
    //semana 8 prototipos
    //[SerializeField] private GameIntEvent scoreUpdated;

    private int score = 0;
    private float time;
    private float timeSpeed = 2.0f;

    private void Update()
    {
        if(TextTime != null)
        {
            time += Time.deltaTime * timeSpeed;
            TextTime.text = "Tiempo " + time.ToString("F2");

        }
    }
    private void OnEnable()
    {


    }

    private void OnDisable()
    {
        GameEventsDep.OnLifeUpdated -= UpdateLifeUI;
        GameEventsDep.OnScoreUpdated -= UpdateScoreUI;
       // GameEventsDep.OnGameEnd -= EndGame;//
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        if (buttonP != null)
        {
            buttonP.SetActive(false);
            panelPause.SetActive(true);
        }
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        if (buttonP != null)
        {
            buttonP.SetActive(true);
            panelPause.SetActive(false);

        }
    }
    public void UpdateLifeUI(int newLife)
    {
        Debug.Log("Actualizando vida UI: " + newLife);
        TextLife.text = "Vida: " + newLife;

    }

    public void UpdateScoreUI(int newScore)
    {
        score += newScore;
        //scoreUpdated.Raise(score);                             
        TextScore.text = "Puntos: " + score;
    }

    public void EndGame(bool victory)
    {
        panelWinOrLose.SetActive(true);
        TextResult.text = victory ? "�GANASTE!" : "PERDISTE";
        Time.timeScale = 0f;
        TextTimeEnd.text = "Tiempo " + time.ToString("F2");
    }
    public void EndGame2()
    {
        panelWinOrLose.SetActive(true);
        TextResult.text = "PERDISTE";
        Time.timeScale = 0f;
    }
    public void WinGame()
    {
        panelWinOrLose.SetActive(true);
        TextResult.text = "Ganaste";
        Time.timeScale = 0f;
    }
}