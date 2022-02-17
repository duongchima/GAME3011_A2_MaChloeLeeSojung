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
        float randomAngle = Random.Range(0, 180);

        screwdriver_sweetSpot = randomAngle;
        bobbypin_sweetSpot = 0;
    }

    void Update()
    {
        if (!unlocked)
        {
            if (Input.GetKey(KeyCode.D))
            {
                Screwdriver.transform.Rotate(Vector3.forward, 0.1f);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                Screwdriver.transform.Rotate(-Vector3.forward, 0.1f);
            }

            BobbyPin.transform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(Input.mousePosition.x * 0.1f, 0, 180));

            if (Screwdriver.transform.rotation.eulerAngles.z > screwdriver_sweetSpot - 10 && Screwdriver.transform.rotation.eulerAngles.z < screwdriver_sweetSpot + 10)
            {
                unlocked = true;
            }
        }
        else
        {
            print("unlocked");
        }

    }
}
