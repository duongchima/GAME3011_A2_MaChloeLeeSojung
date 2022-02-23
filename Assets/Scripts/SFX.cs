using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public static AudioClip unlockSound, bobbySound, screwSound, resetSound;
    static AudioSource src;
    // Start is called before the first frame update
    void Start()
    {
        unlockSound = Resources.Load<AudioClip>("LockpickingUnlocked");
        bobbySound = Resources.Load<AudioClip>("LockpickingStuck");
        screwSound = Resources.Load<AudioClip>("LockpickingTurn");
        resetSound = Resources.Load<AudioClip>("LockpickingReset");


        src = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "unlock":
                src.PlayOneShot(unlockSound);
                break;
            case "bobby":
                src.PlayOneShot(bobbySound);
                break;
            case "screwdriver":
                src.PlayOneShot(screwSound);
                break;
            case "reset":
                src.PlayOneShot(resetSound);
                break;
        }
    }
}
