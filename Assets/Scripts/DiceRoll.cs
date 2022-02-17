using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DiceRoll : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI diceText;
    [SerializeField]
    private Image dice;
    public int diceNum;
    public void RollDifficulty()
    {
        diceNum = Random.Range(0, 20);
        UpdateDifficulty();
        diceText.text = diceNum.ToString();
    }

    void UpdateDifficulty()
    {
        if (diceNum >= 0 && diceNum <= 6)
        {
            GameManager.Instance.UpdateDifficulty(GameManager.Difficulties.Hard);
        }
        else if (diceNum >= 7 && diceNum <= 13)
        {
            GameManager.Instance.UpdateDifficulty(GameManager.Difficulties.Medium);
        }
        else if (diceNum >= 14 && diceNum <= 20)
        {
            GameManager.Instance.UpdateDifficulty(GameManager.Difficulties.Easy);
        }
    }
    
}
