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

    public GameObject Lock, resetButton, victory, startButton;
    [SerializeField]
    private Lock _lock;
    [SerializeField]
    private DiceRoll diceRoll;
    public PlayerSkillLevel playerSkill;

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
    void Start()
    {

    }
    private void Update()
    {
        Countdown();
        if (_lock.unlocked)
        {
            victory.SetActive(true);
            timer = 0;
        }
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
    }

    public enum PlayerSkillLevel
    {
        Beginner = 1,
        Intermediate = 5,
        Advanced = 10
    }

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
    public void DisplayLock()
    {
        Lock.SetActive(true);
        resetButton.SetActive(true);
        startButton.SetActive(false);
        GenerateSweetSpot();
    }
    public void GenerateSweetSpot()
    {
        _lock.screwdriverSweetSpotRot = Random.Range(0, 180);
        _lock.bobbyPinSweetSpotRot = Random.Range(0, 180);
        _lock.bobbyPinSweetSpotPos = Random.Range(-25, 40);
        _lock.screwdriverSweetSpotPos = Random.Range(-10, 60);
    }
    public void Reset()
    {
        _lock.BobbyPin.transform.rotation = _lock.bobbyPinStartRot;
        _lock.Screwdriver.transform.rotation = _lock.screwdriverStartingRot;
        GenerateSweetSpot();
        if (!_lock.unlocked)
        {
            if (Difficulty == Difficulties.Easy)
            {
                timer = 15;
            }
            else if (Difficulty == Difficulties.Medium)
            {
                timer = 30;
            }
            else if (Difficulty == Difficulties.Hard)
            {
                timer = 60;
            }
        }
        else
        {
            _lock.Locks["LockBase"] = false;
            _lock.Locks["BobbyPin"] = false;
            _lock.Locks["Screwdriver"] = false;
            _lock.unlocked = false;
            diceRoll.RollDifficulty();
        }
    }
}
