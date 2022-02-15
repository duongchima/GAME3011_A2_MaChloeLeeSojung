using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static Difficulties Difficulty;
    void Start()
    {
       
    }
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
                Debug.Log("Difficulty set to easy");
                break;
            case Difficulties.Medium:
                Debug.Log("Difficulty set to medium");
                break;
            case Difficulties.Hard:
                Debug.Log("Difficulty set to hard");
                break;
        }
    }
}
