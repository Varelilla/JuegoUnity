using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Vida : MonoBehaviour
{
    public Image[] hearts;
    public TextMeshProUGUI scoreText;
    public int score = 0;

    public void actualizarVida(int currentLives)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < currentLives;
        }
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = score.ToString("D7");
    }
}
