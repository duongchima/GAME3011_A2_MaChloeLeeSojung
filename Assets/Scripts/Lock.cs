using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public bool unlocked = false;

    public GameObject Screwdriver;
    public GameObject BobbyPin;

    void Update()
    {
       if (Input.GetKey(KeyCode.D))
       {
            Screwdriver.transform.Rotate(Vector3.forward);
       }
       else if (Input.GetKey(KeyCode.A))
       {
            Screwdriver.transform.Rotate(-Vector3.forward);
       }

        BobbyPin.transform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(Input.mousePosition.x * 0.1f, 0, 180));

    }
}
