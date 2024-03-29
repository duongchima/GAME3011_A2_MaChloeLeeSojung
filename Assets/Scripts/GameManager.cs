﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static Difficulties Difficulty;

    public float timer;
    public TextMeshProUGUI timerText;
    bool selectedSkillLevel;
    bool gameStarted;
    public float threshold;
    public GameObject Lock, resetButton, victory, gameOver, startButton, advanced, intermediate, beginner;
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
        gameStarted = false;
        selectedSkillLevel = false;
    }
    private void Update()
    {
        Countdown();
        if (_lock.unlocked)
        {
            victory.SetActive(true);
            Lock.SetActive(false);
            resetButton.SetActive(false);
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
        if(timer <= 0)
        {
            if (!_lock.unlocked && gameStarted)
            {
                gameOver.SetActive(true);
                Lock.SetActive(false);
                resetButton.SetActive(false);
            }
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
        Beginner = 3,
        Intermediate = 4,
        Advanced = 5
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
        if (selectedSkillLevel)
        {
            Lock.SetActive(true);
            resetButton.SetActive(true);
            startButton.SetActive(false);
            beginner.SetActive(false);
            intermediate.SetActive(false);
            advanced.SetActive(false);
            gameStarted = true;
            GenerateSweetSpot();
        }
        else
        {
            Debug.Log("Must pick a skill Level!");
        }
    }
    public void GenerateSweetSpot()
    {
        _lock.screwdriverSweetSpotRot = Random.Range(0, 180);
        _lock.bobbyPinSweetSpotRot = Random.Range(0, 180);
        _lock.bobbyPinSweetSpotPos = Random.Range(-12, 38);
        _lock.screwdriverSweetSpotPos = Random.Range(-12, 38);
    }
    public void Reset()
    {
        _lock.BobbyPin.transform.rotation = _lock.bobbyPinStartRot;
        _lock.Screwdriver.transform.rotation = _lock.screwdriverStartingRot;
        _lock.BobbyPin.transform.position = _lock.bobbyPinStartPos;
        _lock.Screwdriver.transform.position = _lock.screwdriverStartingPos;
        GenerateSweetSpot();
        resetButton.SetActive(true);
        Lock.SetActive(true);
        gameOver.SetActive(false);
        victory.SetActive(false);
        _lock.bobbyPinRotFound = false;
        _lock.bobbyPinPosFound = false;
        _lock.screwDriverRotFound = false;
        _lock.screwDriverPosFound = false;
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
    public void AdvancedSelected()
    {
        playerSkill = PlayerSkillLevel.Advanced;
        selectedSkillLevel = true;
    }
    public void IntermediateSelected()
    {
        playerSkill = PlayerSkillLevel.Intermediate;
        selectedSkillLevel = true;
    }
    public void BeginnerSelected()
    {
        playerSkill = PlayerSkillLevel.Beginner;
        selectedSkillLevel = true;
    }
}
