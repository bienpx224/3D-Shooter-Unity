using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _highScoreText;
    [SerializeField] TMP_Text _multiplierText;
    [SerializeField] float _scoreMultiplierExpiration = 1f;
    [SerializeField] FloatScoreText _floatingTextPrefab;
    [SerializeField] Canvas _floatingScoreCanvas;
    [SerializeField] float _floatSpeed = 0.2f;
    int _score;
    int _highScore;
    int _killMultiplier;
    public const string HIGH_SCORE = "HIGH_SCORE";
    // Start is called before the first frame update
    void Start()
    {
        Zombie.Died += ZombieDied;
        _highScore = PlayerPrefs.GetInt(HIGH_SCORE);
        _highScoreText.SetText("High Score: " + _highScore);
        _multiplierText.color = Color.white;
        _multiplierText.fontSize = 41;
    }

    private void ZombieDied(Zombie zombie)
    {
        UpdateMultiplierText();
        _score += _killMultiplier;
        if (_score > _highScore)
        {
            _highScore = _score;
            _highScoreText.SetText("High Score: " + _highScore);
            PlayerPrefs.SetInt(HIGH_SCORE, _highScore);
        }
        _scoreText.SetText(_score.ToString());

        var floatingText = Instantiate(
            _floatingTextPrefab,
            zombie.transform.position,
            _floatingScoreCanvas.transform.rotation,
            _floatingScoreCanvas.transform
        );

        floatingText.SetScoreValue(_killMultiplier);
    }
    void OnDestroy(){
        Zombie.Died -= ZombieDied;
    }

    void UpdateMultiplierText()
    {
        if (Time.time <= _scoreMultiplierExpiration)
        {
            _killMultiplier++;
        }
        else
        {
            _killMultiplier = 1;
        }

        _scoreMultiplierExpiration = Time.time + 1f;
        _multiplierText.SetText("x " + _killMultiplier.ToString());

        if (_killMultiplier < 2)
        {
            _multiplierText.color = Color.white;
            _multiplierText.fontSize = 41;
        }
        else if (_killMultiplier < 5)
        {
            _multiplierText.color = Color.green;
            _multiplierText.fontSize = 61;
        }
        else if (_killMultiplier < 10)
        {
            _multiplierText.color = Color.yellow;
            _multiplierText.fontSize = 81;
        }
        else
        {
            _multiplierText.color = Color.red;
            _multiplierText.fontSize = 101;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
