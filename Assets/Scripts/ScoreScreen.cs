using TMPro;
using UnityEngine;

public class ScoreScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public void UpdateScore(int totalScore)
    {
        scoreText.text = "Puntos: " + totalScore;
        Debug.Log(scoreText.text);
    }
}