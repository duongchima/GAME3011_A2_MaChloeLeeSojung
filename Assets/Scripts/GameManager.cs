using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static Difficulties Difficulty;

    public float timer;
    public TextMeshProUGUI timerText;

    public float threshold;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        Countdown();
    }

    public void Countdown()
    {
        if (timer > 0)
        {
            timerText.text = $"Timer  -  {Mathf.FloorToInt(timer)} seconds";
            timer -= Time.deltaTime;
        }
    }

    public enum Difficulties
    {
        Easy,
        Medium,
        Hard
    };

    public void UpdateDifficulty(Difficulties newDifficulty)
    {
        Difficulty = newDifficulty;
        switch (newDifficulty)
        {
            case Difficulties.Easy:
                timer = 15;
                threshold = 10;
                Debug.Log("Difficulty set to easy");
                break;
            case Difficulties.Medium:
                timer = 30;
                threshold = 5;
                Debug.Log("Difficulty set to medium");
                break;
            case Difficulties.Hard:
                timer = 60;
                threshold = 1;
                Debug.Log("Difficulty set to hard");
                break;
        }
    }
}
