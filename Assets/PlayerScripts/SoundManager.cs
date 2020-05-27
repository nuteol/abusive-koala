using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip backgroundMusic0, playerJumpSound, playerDeathSound, playerHitSound, playerHitSound2, playerDrawSound, playerDrawSound2, monsterDeathSound, playerGetsHitSound, playerYouDied, bossMusic, backgroundMusic1;
    public static AudioSource audrioSrc;
    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic0 = Resources.Load<AudioClip>("BackgroundMusicTunnels");
        backgroundMusic1 = Resources.Load<AudioClip>("background-loop");

        playerHitSound = Resources.Load<AudioClip>("swordHit");
        playerDrawSound = Resources.Load<AudioClip>("swordDraw");
        playerHitSound2 = Resources.Load<AudioClip>("maceSwing");
        playerDrawSound2 = Resources.Load<AudioClip>("maceDraw");
        playerJumpSound = Resources.Load<AudioClip>("playerJump");
        playerDeathSound = Resources.Load<AudioClip>("maleDeath");
        //playerGetsHitSound = Resources.Load<AudioClip>("robloxDeath");
        playerGetsHitSound = Resources.Load<AudioClip>("playerGetHit");
        playerYouDied = Resources.Load<AudioClip>("youDied");

        monsterDeathSound = Resources.Load<AudioClip>("cutterDeath");

        bossMusic = Resources.Load<AudioClip>("bossfightloop");

        audrioSrc = GetComponent<AudioSource>();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PlayMainMenu()
    {

        audrioSrc.Stop();
        audrioSrc.loop = true;
        audrioSrc.clip = backgroundMusic0;
        audrioSrc.volume = 0.4f;
        audrioSrc.Play();

    }
    public static void StopMainMenu()
    {
        audrioSrc.Stop();
    }
    public static void PlayBackground()
    {

        audrioSrc.Stop();
        audrioSrc.loop = true;
        audrioSrc.clip = backgroundMusic1;
        audrioSrc.volume = 0.4f;
        audrioSrc.Play();

    }
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "1stLevelBackground":
                audrioSrc.PlayOneShot(backgroundMusic0);
                break;
            case "backgroundMusic":
                audrioSrc.PlayOneShot(backgroundMusic1);
                break;

            case "playerHit":
                audrioSrc.PlayOneShot(playerHitSound);
                break;
            case "playerDraw":
                audrioSrc.PlayOneShot(playerDrawSound);
                break;
            case "maceHit":
                audrioSrc.PlayOneShot(playerHitSound2);
                break;
            case "maceDraw":
                audrioSrc.PlayOneShot(playerDrawSound2);
                break;
            case "playerJump":
                audrioSrc.PlayOneShot(playerJumpSound);
                audrioSrc.volume = 0.1f;
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

            case "bossMusic":
                //stops all music and starts playing boss music
                audrioSrc.Stop();
                audrioSrc.loop = true;
                audrioSrc.clip = bossMusic;
                audrioSrc.volume = 0.2f;
                audrioSrc.Play();
                break;

        }
    }
}
