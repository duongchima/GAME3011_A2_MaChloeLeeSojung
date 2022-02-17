using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public bool unlocked = false;

    public GameObject Screwdriver;
    public GameObject BobbyPin;

    public float screwdriver_sweetSpot = 0;
    public float bobbypin_sweetSpot = 0;

    private void Start()
    {
        screwdriver_sweetSpot = Random.Range(0, 180);
        bobbypin_sweetSpot = Random.Range(0, 180);
    }

    void Update()
    {
        if (!unlocked)
        {
            if (Screwdriver.transform.rotation.eulerAngles.z > screwdriver_sweetSpot - GameManager.Instance.threshold &&
                Screwdriver.transform.rotation.eulerAngles.z < screwdriver_sweetSpot + GameManager.Instance.threshold)
            {
                //unlocked = true;
            }
            else
            {
                if (Input.GetKey(KeyCode.D))
                {
                    Screwdriver.transform.Rotate(Vector3.forward, 0.1f);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    Screwdriver.transform.Rotate(-Vector3.forward, 0.1f);
                }
            }

            if (BobbyPin.transform.rotation.eulerAngles.z > bobbypin_sweetSpot - GameManager.Instance.threshold && 
                BobbyPin.transform.rotation.eulerAngles.z < bobbypin_sweetSpot + GameManager.Instance.threshold)
            {

            }
            else
            {
                BobbyPin.transform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(Input.mousePosition.x * 0.1f, 0, 180));
            }
        }
        else
        {
            print("unlocked");
        }

    }
}
