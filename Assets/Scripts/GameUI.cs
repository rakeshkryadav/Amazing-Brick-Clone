using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    // User interface elements.
    [SerializeField] private Button soundButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text totalScoreText;
    [SerializeField] private TMP_Text bestScoreText;

    // User interface panel.
    [SerializeField] private GameObject tapPanel;
    [SerializeField] private GameObject scorePanel;

    // User interface components.
    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite soundOff;
    [SerializeField] private Sprite musicOn;
    [SerializeField] private Sprite musicOff;

    public static bool isGameOver = false;
    public static int gameScore;
    public static int bestScore;

    // logic variables.
    private bool sound = true;
    private bool music = true;

    private void Start(){
        tapPanel.SetActive(false);
        scorePanel.SetActive(false);
        Application.targetFrameRate = 30;

        // Check game over.
        if(isGameOver){
            scorePanel.SetActive(true);
            isGameOver = false;
        }

        Score();
    }

    public void GameScore(){
        // increment the score.
        gameScore++;
        scoreText.text = gameScore.ToString();
    }
    public void PlaySound(){
        if(sound){
            sound = false;
            soundButton.GetComponentInChildren<Image>().sprite = soundOff;
        }
        else{
            sound = true;
            soundButton.GetComponentInChildren<Image>().sprite = soundOn;
        }
    }

    public void PlayMusic(){
        if(music){
            music = false;
            musicButton.GetComponentInChildren<Image>().sprite = musicOff;
        }
        else{
            music = true;
            musicButton.GetComponentInChildren<Image>().sprite = musicOn;
        }
    }

    public void PlayGame(){
        tapPanel.SetActive(true);
        gameScore = 0;
    }

    public void Home(){
        scorePanel.SetActive(false);
    }

    private void Score(){
        if(gameScore > bestScore){
            bestScore = gameScore;
        }
        totalScoreText.text = gameScore.ToString();
        bestScoreText.text = bestScore.ToString();
    }
}
