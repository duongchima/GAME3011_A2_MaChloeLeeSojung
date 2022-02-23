using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
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
    public float movementSpeed = 2000.0f;

    [Header("Sweet Spots")]
    public float screwdriverSweetSpotRot = 0;
    public float bobbyPinSweetSpotRot = 0;
    public float screwdriverSweetSpotPos = 0;
    public float bobbyPinSweetSpotPos = 0;

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

            }
            else if (Screwdriver.transform.position.y > screwdriverSweetSpotPos - (increasedChance) &&
                Screwdriver.transform.position.y < screwdriverSweetSpotPos + (increasedChance))
            {

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

            }
            else if (BobbyPin.transform.position.y > bobbyPinSweetSpotPos - (increasedChance) &&
                BobbyPin.transform.position.y < bobbyPinSweetSpotPos + (increasedChance))
            {

            }
            if (Locks["BobbyPin"] && Locks["Screwdriver"]) Locks["LockBase"] = true;
        }
        else
        {
            print("unlocked");
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
            Screwdriver.transform.position += new Vector3(0, 1, 0) * Time.deltaTime * movementSpeed;
        }
        if(Input.GetAxisRaw("Vertical") < 0)
        {
            Screwdriver.transform.position += new Vector3(0, -1, 0) * Time.deltaTime * movementSpeed;
        }
    }
    private void AllowBobbyPinMovement()
    {
        BobbyPin.transform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(Input.mousePosition.x * bobbypinRotSpeed, 0, 180));
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            BobbyPin.transform.position += new Vector3(0, -1, 0) * Time.deltaTime * movementSpeed;
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            BobbyPin.transform.position += new Vector3(0, 1, 0) * Time.deltaTime * movementSpeed;
        }
    }
}
