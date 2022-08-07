using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _bestScoreText;
    private int _score;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        _scoreText.text = "Score: " + _score;
        _bestScoreText.text = "Best: " + PlayerPrefs.GetInt("Best");
    }
    public void AddScore(int score)
    {
        _score += score;
        _scoreText.text = "Score: " + _score;
        if (_score > PlayerPrefs.GetInt("Best"))
        {
            PlayerPrefs.SetInt("Best", _score);
            _bestScoreText.text = "Best: " + PlayerPrefs.GetInt("Best");
        }
    }
    public void GameOver()
    {
        SceneManager.LoadScene(0);
    }
}
