using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PantallaDerrota : MonoBehaviour
{
    public Text scoreText;

    public void Setup(int score)
    {
        gameObject.SetActive(true);
        scoreText.text = "HAS SIDO DERROTADO" + "\n" + "PUNTUACION FINAL" + "\n" + score.ToString("D7");
        Time.timeScale = 0;

    }

    public void Reiniciar()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Salir()
    {
        Application.Quit();
    }
}
