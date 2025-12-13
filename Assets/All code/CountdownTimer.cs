using UnityEngine;
using UnityEngine.UI;   // for UI Text

public class CountdownTimer : MonoBehaviour
{
    public Text timerText;      // assign your TimerText here
    public float timeRemaining = 60f;   // start at 60 seconds
    private bool timerRunning = true;

    void Update()
    {
        if (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerRunning = false;
                UpdateTimerDisplay(timeRemaining);
                EndGame();
            }
        }
    }

    void UpdateTimerDisplay(float time)
    {
        int seconds = Mathf.CeilToInt(time);
        timerText.text = seconds.ToString();
    }

    void EndGame()
    {
        Debug.Log("Time’s up! Game Over");
        // you can show a GameOver panel or reload the scene here later
        // SceneManager.LoadScene("GameOverScene");
    }
}
