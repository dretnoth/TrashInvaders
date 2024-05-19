using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public float sfxVolumeValue = 0.5f, musicVolumeValue = 1f;
    public static SoundController soundController;
    public GameController gameController;
    public AudioSource musicMenuAS, musicInGameAS;
    public AudioSource audioSRooster, audioSCheer, audioSYouLost, audioSYouWin, 
        audioSExplosion, audioSPaper, audioTruckHonk, audioRecicleBin,
        audioReadyFight;
    public AudioSource audioButtonHiglight, audioButtonPress, audioButtonSwithc;
    public AudioSource[] poopAudioClips;
    public AudioClip[] musicAudioClips, uiButonHighlightACs, uiButtonPressACs,
        uiButtonSwitchACs;
    public int musicInPlay;


    private void Awake() {
        if(soundController == null) soundController = this;
    }

    private void Start() {
        CommandAudioVolumeCheckUp();
    }

    public void CommandPlayAMenuMusic(){
        if(musicInGameAS.isPlaying) musicInGameAS.Stop();
        if(!musicMenuAS.isPlaying) musicMenuAS.Play();
    }

    public void CommandPlayInGameMusic(){
        if(musicMenuAS.isPlaying) musicMenuAS.Stop();
        musicInPlay++;
        if(musicInPlay >= musicAudioClips.Length){musicInPlay = 0;}
        musicInGameAS.clip = musicAudioClips[musicInPlay];
        musicInGameAS.Play();
    }


    public void CommandPlayRooster(){audioSRooster.Play();}

    public void CommandPlayCheer(){audioSCheer.Play();}

    public void CommandPlayYouLost(){audioSYouLost.Play();}

    public void CommandPlayYouWin(){
        audioSYouWin.Play();
        musicInGameAS.Stop();
        }

    public void CommandPlayExplosion(){audioSExplosion.Play();}


    public void CommandPlayPaper(){audioSPaper.Play();}


    public void CommandPlayTruckHonk(){audioTruckHonk.Play();}

    public void CommandPlayRecicleBin(){audioRecicleBin.Play();}

    public void CommandPlayReadyFight(){audioReadyFight.Play();}


    public void CommandPlayButtonHighlight(){
        audioButtonHiglight.clip = uiButonHighlightACs[
            (int)Random.Range(0, uiButonHighlightACs.Length-1)
        ];
        audioButtonHiglight.Play();
    }

    public void CommandPlayButtonPress(){
        audioButtonPress.clip = uiButtonPressACs[
            (int)Random.Range(0, uiButtonPressACs.Length-1)
        ];
        audioButtonPress.Play();
    }

    public void CommandPlayButtonSwitch(){
        audioButtonSwithc.clip = uiButtonSwitchACs[
            (int)Random.Range(0, uiButtonSwitchACs.Length-1)
        ];
        audioButtonSwithc.Play();
    }

    public void CommandPLayPoopSound(){
        int chosenOne = (int)Random.Range(0,poopAudioClips.Length-1);
        poopAudioClips[chosenOne].Play();
    }


    public void CommandAudioVolumeCheckUp(){
        musicMenuAS.volume = musicVolumeValue;
        musicInGameAS.volume = musicVolumeValue;
        
        audioSRooster.volume = sfxVolumeValue;
        audioSCheer.volume = sfxVolumeValue;
        audioSExplosion.volume = sfxVolumeValue;
        audioSPaper.volume = sfxVolumeValue;
        
        audioSYouWin.volume = sfxVolumeValue;
        audioSYouLost.volume = sfxVolumeValue;

        audioButtonHiglight.volume = sfxVolumeValue;
        audioButtonPress.volume = sfxVolumeValue;
        audioButtonSwithc.volume = sfxVolumeValue;

        for (int i = 0; i < poopAudioClips.Length; i++)
        {
            poopAudioClips[i].volume = sfxVolumeValue;

        }

        if(gameController.playerTransform != null){
            gameController.playerTransform.GetComponent<PlayerController>()
                .CommandChecekAudioSettings();
        }
    }

    

}
