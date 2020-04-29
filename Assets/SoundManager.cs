using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip backgroundMusic1, playerJumpSound, playerHitSound, playerDeathSound, playerDrawSound, monsterDeathSound, playerGetsHitSound, playerYouDied;
    static AudioSource audrioSrc;
    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic1 = Resources.Load<AudioClip>("BackgroundMusicTunnels"); 

        playerHitSound = Resources.Load<AudioClip>("swordHit");
        playerDrawSound = Resources.Load<AudioClip>("swordDraw");
        playerJumpSound = Resources.Load<AudioClip>("playerJump");
        playerDeathSound = Resources.Load<AudioClip>("maleDeath");
        playerGetsHitSound = Resources.Load<AudioClip>("robloxDeath");
        playerYouDied = Resources.Load<AudioClip>("youDied");

        monsterDeathSound = Resources.Load<AudioClip>("cutterDeath");

        audrioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "1stLevelBackground":
                audrioSrc.PlayOneShot(backgroundMusic1);
                break;

            case "playerHit":
                audrioSrc.PlayOneShot(playerHitSound);
                break;
            case "playerDraw":
                audrioSrc.PlayOneShot(playerDrawSound);
                break;
            case "playerJump":
                audrioSrc.PlayOneShot(playerJumpSound);
                break;
            case "playerDeath":
                audrioSrc.PlayOneShot(playerDeathSound);
                break;
            case "playerYouDied":
                audrioSrc.PlayOneShot(playerYouDied);
                break;
            case "playerGetHit":
                audrioSrc.PlayOneShot(playerGetsHitSound);
                break;

            case "monsterDeath":
                audrioSrc.PlayOneShot(monsterDeathSound);
                break;

        }
    }
}
