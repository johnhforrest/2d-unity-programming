using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int playUntil;

    public Text player1ScoreText;
    public Text player2ScoreText;
    public Text playingUntilText;

    private int player1Score;
    private int player2Score;

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Snap the main camera to the size of the sprite
        Camera.main.orthographicSize = spriteRenderer.size.x * Screen.height / Screen.width * 0.5f;

        playingUntilText.text = string.Format("Playing until: {0}", playUntil);
    }

    public void IncreasePlayerScore(int playerNumber)
    {
        if (playerNumber == 1)
        {
            player1Score++;
            player1ScoreText.text = player1Score.ToString();
        }
        else
        {
            player2Score++;
            player2ScoreText.text = player2Score.ToString();
        }

        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        if (player1Score >= playUntil)
        {
            EndGame();
        }
        else if (player2Score >= playUntil)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        player1Score = 0;
        player1ScoreText.text = player1Score.ToString();

        player2Score = 0;
        player2ScoreText.text = player2Score.ToString();
    }
}
