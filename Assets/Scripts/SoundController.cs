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
        audioSExplosion;
    public AudioClip[] musicAudioClips;
    public int musicInPlay;


    private void Awake() {
        if(soundController == null) soundController = this;
    }

    private void Start() {
        CommandAudioVolumeCheckUp();
    }

    public void CommandPlayAMenuMusic(){
        if(musicInGameAS.isPlaying) musicInGameAS.Stop();
        musicMenuAS.Play();
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
        audioSYouLost.Play();
        musicInGameAS.Stop();
        }

    public void CommandPlayExplosion(){audioSExplosion.Play();}


    public void CommandAudioVolumeCheckUp(){
        musicMenuAS.volume = musicVolumeValue;
        musicInGameAS.volume = musicVolumeValue;
        
        audioSRooster.volume = sfxVolumeValue;
        audioSCheer.volume = sfxVolumeValue;
        audioSExplosion.volume = sfxVolumeValue;
        
        audioSYouWin.volume = sfxVolumeValue;
        audioSYouLost.volume = sfxVolumeValue;

        if(gameController.playerTransform != null){
            gameController.playerTransform.GetComponent<PlayerController>()
                .CommandChecekAudioSettings();
        }
    }

    

}
