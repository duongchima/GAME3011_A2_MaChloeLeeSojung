using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public bool hasPlayed = false;
    public bool unlocked = false;
    public Quaternion bobbyPinStartRot;
    public Quaternion screwdriverStartingRot;
    public Vector3 bobbyPinStartPos;
    public Vector3 screwdriverStartingPos;
    [Header("Equipment")]
    public GameObject Screwdriver;
    public GameObject BobbyPin;

    [Header("Equipment Speed")]
    public float screwdriverRotSpeed = 0.1f;
    public float bobbypinRotSpeed = 0.1f;
    public float screwdriverMoveSpeed = 2000.0f;
    public float bobbyPinMoveSpeed = 500.0f;

    [Header("Sweet Spots")]
    public float screwdriverSweetSpotRot;
    public float bobbyPinSweetSpotRot;
    public float screwdriverSweetSpotPos;
    public float bobbyPinSweetSpotPos;

    public Dictionary<string, bool> Locks = new Dictionary<string, bool>(3);

    private void Start()
    {
        Locks.Add("LockBase", false);
        Locks.Add("Screwdriver", false);
        Locks.Add("BobbyPin", false);
        bobbyPinStartRot = BobbyPin.transform.rotation;
        screwdriverStartingRot = Screwdriver.transform.rotation;
        bobbyPinStartPos = BobbyPin.transform.position;
        screwdriverStartingPos = Screwdriver.transform.position;
    }
   
      
    void Update()
    {
        if (!Locks["LockBase"])
        {
            float increasedChance = GameManager.Instance.threshold + (int)GameManager.Instance.playerSkill;

            if (Screwdriver.transform.rotation.eulerAngles.z > screwdriverSweetSpotRot - (increasedChance) &&
                Screwdriver.transform.rotation.eulerAngles.z < screwdriverSweetSpotRot + (increasedChance) &&
                Screwdriver.transform.position.y > screwdriverSweetSpotPos - (increasedChance) &&
                Screwdriver.transform.position.y < screwdriverSweetSpotPos + (increasedChance))
            {
                Locks["Screwdriver"] = true;
            }
            else
            {
                AllowScrewdriverMovement();
            }
            if (Screwdriver.transform.rotation.eulerAngles.z > screwdriverSweetSpotRot - (increasedChance) &&
                Screwdriver.transform.rotation.eulerAngles.z < screwdriverSweetSpotRot + (increasedChance))
            {
                if (!hasPlayed)
                {
                    SFX.PlaySound("unlock");
                    Debug.Log("Found screwdriver rotation sweet spot!");
                    hasPlayed = true;
                }
            }
            else if (Screwdriver.transform.position.y > screwdriverSweetSpotPos - (increasedChance) &&
                Screwdriver.transform.position.y < screwdriverSweetSpotPos + (increasedChance))
            {
                if (!hasPlayed)
                {
                    SFX.PlaySound("unlock");
                    Debug.Log("Found screwdriver rotation sweet spot!");
                    hasPlayed = true;
                }
            }
            if (BobbyPin.transform.rotation.eulerAngles.z > bobbyPinSweetSpotRot - (increasedChance) && 
                BobbyPin.transform.rotation.eulerAngles.z < bobbyPinSweetSpotRot + (increasedChance) &&
                BobbyPin.transform.position.y > bobbyPinSweetSpotPos - (increasedChance) &&
                BobbyPin.transform.position.y < bobbyPinSweetSpotPos + (increasedChance))
            {
                Locks["BobbyPin"] = true;
            }
            else
            {
                AllowBobbyPinMovement();
            }
             if (BobbyPin.transform.rotation.eulerAngles.z > bobbyPinSweetSpotRot - (increasedChance) &&
                BobbyPin.transform.rotation.eulerAngles.z < bobbyPinSweetSpotRot + (increasedChance))
            {
                if (!hasPlayed)
                {
                    SFX.PlaySound("unlock");
                    Debug.Log("Found screwdriver rotation sweet spot!");
                    hasPlayed = true;
                }
            }
            else if (BobbyPin.transform.position.y > bobbyPinSweetSpotPos - (increasedChance) &&
                BobbyPin.transform.position.y < bobbyPinSweetSpotPos + (increasedChance))
            {
                if (!hasPlayed)
                {
                    SFX.PlaySound("unlock");
                    Debug.Log("Found screwdriver rotation sweet spot!");
                    hasPlayed = true;
                }
            }
            if (Locks["BobbyPin"] && Locks["Screwdriver"]) Locks["LockBase"] = true;
        }
        else
        {
            print("unlocked");
            SFX.PlaySound("unlock");
            unlocked = true;
        }
    }
    private void AllowScrewdriverMovement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Screwdriver.transform.Rotate(Vector3.forward, screwdriverRotSpeed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Screwdriver.transform.Rotate(-Vector3.forward, screwdriverRotSpeed);
        }
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            if (Screwdriver.transform.position.y <= 375) {
                Screwdriver.transform.position += new Vector3(0, 1, 0) * Time.deltaTime * screwdriverMoveSpeed;
                LeanTween.rotate(Screwdriver, new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), Screwdriver.transform.rotation.eulerAngles.z), 1f)
                        .setLoopPingPong(1)
                        .setEaseInOutBack();
            }
        }
        if(Input.GetAxisRaw("Vertical") < 0)
        {
            if (Screwdriver.transform.position.y >= 345)
            {
                Screwdriver.transform.position += new Vector3(0, -1, 0) * Time.deltaTime * screwdriverMoveSpeed;
                LeanTween.rotate(Screwdriver, new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), Screwdriver.transform.rotation.eulerAngles.z), 1f)
                        .setLoopPingPong(1)
                        .setEaseInOutBack();
            }
        }
    }
    private void AllowBobbyPinMovement()
    {
        BobbyPin.transform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(Input.mousePosition.x * bobbypinRotSpeed, 0, 180));
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (BobbyPin.transform.position.y >= 345)
            {
                BobbyPin.transform.position += new Vector3(0, -1, 0) * Time.deltaTime * bobbyPinMoveSpeed;

                LeanTween.rotate(BobbyPin, new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), BobbyPin.transform.rotation.eulerAngles.z), 1f)
                         .setLoopPingPong(1)
                         .setEaseInOutBack();
            }
        }
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (BobbyPin.transform.position.y <= 375)
            {
                BobbyPin.transform.position += new Vector3(0, 1, 0) * Time.deltaTime * bobbyPinMoveSpeed;

                LeanTween.rotate(BobbyPin, new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), BobbyPin.transform.rotation.eulerAngles.z), 1f)
                         .setLoopPingPong(1)
                         .setEaseInOutBack();
            }
        }
    }
}
