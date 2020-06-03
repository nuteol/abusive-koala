using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip roundabout,backgroundMusic0, playerJumpSound, playerDeathSound, playerHitSound, playerHitSound2, playerDrawSound, playerDrawSound2, monsterDeathSound, glassCanonDeathSound, playerGetsHitSound, playerYouDied, bossMusic, backgroundMusic1;
    public static AudioSource audrioSrc;
    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic0 = Resources.Load<AudioClip>("BackgroundMusicTunnels");
        backgroundMusic1 = Resources.Load<AudioClip>("background-loop");
        roundabout = Resources.Load<AudioClip>("Roundabout");
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
        glassCanonDeathSound = Resources.Load<AudioClip>("GlassCanonDeath");

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
                audrioSrc.volume = 1f;
                audrioSrc.PlayOneShot(backgroundMusic0);
                break;
            case "backgroundMusic":
                audrioSrc.volume = 1f;
                audrioSrc.PlayOneShot(backgroundMusic1);
                break;

            case "playerHit":
                audrioSrc.volume = 1f;
                audrioSrc.PlayOneShot(playerHitSound);
                break;
            case "playerDraw":
                audrioSrc.volume = 1f;
                audrioSrc.PlayOneShot(playerDrawSound);
                break;
            case "maceHit":
                audrioSrc.volume = 1f;
                audrioSrc.PlayOneShot(playerHitSound2);
                break;
            case "maceDraw":
                audrioSrc.volume = 1f;
                audrioSrc.PlayOneShot(playerDrawSound2);
                break;
            case "playerJump":
                audrioSrc.volume = 1f;
                audrioSrc.PlayOneShot(playerJumpSound);
                break;
            case "playerDeath":
                audrioSrc.volume = 0.2f;
                audrioSrc.PlayOneShot(playerDeathSound);
                break;
            case "playerYouDied":
                audrioSrc.volume = 1f;
                audrioSrc.PlayOneShot(playerYouDied);
                break;
            case "playerGetHit":
                audrioSrc.volume = 1f;
                audrioSrc.PlayOneShot(playerGetsHitSound);
                break;

            case "monsterDeath":
                audrioSrc.volume = 1f;
                audrioSrc.PlayOneShot(monsterDeathSound);
                break;
            case "glassCanonDeath":
                audrioSrc.volume = 1f;
                audrioSrc.PlayOneShot(glassCanonDeathSound);
                    break;

            case "bossMusic":
                //stops all music and starts playing boss music
                audrioSrc.Stop();
                audrioSrc.loop = true;
                audrioSrc.clip = bossMusic;
                audrioSrc.Play();
                break;
            case "Roundabout":
                //stops all music and starts playing end music
                audrioSrc.Stop();
                audrioSrc.volume = 1f;
                audrioSrc.PlayOneShot(roundabout);
                break;

        }
    }
}
