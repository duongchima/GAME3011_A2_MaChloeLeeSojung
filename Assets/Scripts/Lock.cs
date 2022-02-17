using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    [Header("Equipment")]
    public GameObject Screwdriver;
    public GameObject BobbyPin;

    [Header("Equipment Speed")]
    public float screwdriverRotSpeed = 0.1f;
    public float bobbypinRotSpeed = 0.1f;

    [Header("Sweet Spots")]
    public float screwdriverSweetSpot = 0;
    public float bobbyPinSweetSpot = 0;

    private Dictionary<string, bool> Locks = new Dictionary<string, bool>(3);

    private void Start()
    {
        screwdriverSweetSpot = Random.Range(0, 180);
        bobbyPinSweetSpot = Random.Range(0, 180);

        Locks.Add("LockBase", false);
        Locks.Add("Screwdriver", false);
        Locks.Add("BobbyPin", false);
    }

    void Update()
    {
        if (!Locks["LockBase"])
        {
            float increasedChance = GameManager.Instance.threshold + (int)GameManager.Instance.playerSkill;

            if (Screwdriver.transform.rotation.eulerAngles.z > screwdriverSweetSpot - (increasedChance) &&
                Screwdriver.transform.rotation.eulerAngles.z < screwdriverSweetSpot + (increasedChance))
            {
                Locks["Screwdriver"] = true;
            }
            else
            {
                if (Input.GetKey(KeyCode.D))
                {
                    Screwdriver.transform.Rotate(Vector3.forward, screwdriverRotSpeed);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    Screwdriver.transform.Rotate(-Vector3.forward, screwdriverRotSpeed);
                }
            }

            if (BobbyPin.transform.rotation.eulerAngles.z > bobbyPinSweetSpot - (increasedChance) && 
                BobbyPin.transform.rotation.eulerAngles.z < bobbyPinSweetSpot + (increasedChance))
            {
                Locks["BobbyPin"] = true;
            }
            else
            {
                BobbyPin.transform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(Input.mousePosition.x * bobbypinRotSpeed, 0, 180));
            }

            if (Locks["BobbyPin"] && Locks["Screwdriver"]) Locks["LockBase"] = true;
        }
        else
        {
            print("unlocked");
        }

    }
}
