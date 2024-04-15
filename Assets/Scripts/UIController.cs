using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.UI;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;


public class UIController : MonoBehaviour
{
    [Header("Panels")]
    public Transform mainMenuPanel;
    public Transform mainMenuControllButtonsPanel;
    public Transform levelDescribtionPanel;
    public Transform levelResultPanel;
    public Transform endWictoryPanel;
    public Transform creditsPanel;
    

    [Header("Images")]
    public Image levelDescriptionBossImage;
    public SpriteRenderer playModeBacgroundSpriteRenderer;

    [Header("Buttons")]
    public Button startGameButton;


    [Header("Texts")]
    public TMP_Text screenAnoucmentTextField;
    public TMP_Text testNumber;
    public TMP_Text levelDescriptionLevelNumber, levelDescriptionMainText, levelDescriptionSkillsText;
    public TMP_Text levelResultLevelNumberText, leverResultBossText, levelResultBoxText, 
        levelResultPacketSweepedText, levelResultPacketOnFieldText, levelResultBulletFiredText;
    public TMP_Text endWictoryText;


    [Header("Values")]
    public float screenAnoucmentInterval = 2f;
    float screenAnoucmentTimer = 0;
    public bool isScreenAnoucmentOn=false;

    [TextArea]
    public string endWictoryString;


    [Header("Relefernces")]
    public GameController control;
    public static UIController uIC;
    public SoundController soundController;

    [Header("The Steeings")]
    public Transform settingsPanel;
    public TMP_Text settingsSFXValueT, settingsMusicValueT;
    public Slider settingsSFXSlider, settingsMusicSlider;


    [Header("Hud Display")]
    public Transform hudDisplayPanel;
    public TMP_Text bossHPText;
    int bossHPValue=0;
    


   


    private void Awake() {
        uIC = this;
    }
    
    void Start()
    {
        if(control == null) control = GameController.controll;
        if(control != null){
            if(!control.isTestingModeOn){
               OrderActivateMainMenu(true);
            }else{OrderActivateMainMenu(false);}
        }
    }

    
    void Update()
    {
        if(isScreenAnoucmentOn){
            screenAnoucmentTimer += Time.deltaTime;
            if(screenAnoucmentTimer > screenAnoucmentInterval){
                screenAnoucmentTimer = 0;
                screenAnoucmentTextField.gameObject.SetActive(false);
                isScreenAnoucmentOn = false;
            }
        }

        if(control.isPlayModeOn){
            if(control.bossTransform != null){
                if(bossHPValue != control.bossController.hitPointCurrent){
                    bossHPValue = control.bossController.hitPointCurrent;
                    bossHPText.text = bossHPValue.ToString();
                }            
            }else{
                bossHPText.text = "x";
            }
        }

        testNumber.text = control.spawnControll.detectedSpawns.ToString();
    }

    public void ButtonStartTheGame(){
        OrderActivateMainMenu(false);
        control.CommandStarTheGame();
        //bacgroundPanel.gameObject.SetActive(true);
        playModeBacgroundSpriteRenderer.gameObject.SetActive(true);
        OrderToChangeBacgroundSprite();
    }

    public void ButtonEndApplication(){
        Application.Quit();
    }

    public void ButtonToOperateTheSettingsMenu(bool option){
        if(option){
            if(control.isPlayModeOn){
                OrderActivateMainMenu(true);
                soundController.musicMenuAS.Stop();
                }
            settingsPanel.gameObject.SetActive(true);
            control.isSettingsActive=true;
            settingsSFXSlider.value = (int)(soundController.sfxVolumeValue * 10);
            settingsMusicSlider.value = (int)(soundController.musicVolumeValue * 10);
            settingsSFXValueT.text = soundController.sfxVolumeValue * 100 +"%";
            settingsMusicValueT.text = soundController.musicVolumeValue * 100 +"%";
        }else{
            if(control.isPlayModeOn){
                OrderActivateMainMenu(false);
                soundController.musicInGameAS.Play();
                }
            settingsPanel.gameObject.SetActive(false);
            control.isSettingsActive=false;
        }
    }

    public void ButtonToOperateCredits(bool option){
        if(option){
            creditsPanel.gameObject.SetActive(true);
            control.isCreditsActive = true;
        }else{
            creditsPanel.gameObject.SetActive(false);
            control.isCreditsActive = false;
        }
    }


    public void ButtonTestTheBoss(){
        OrderActivateMainMenu(false);
        playModeBacgroundSpriteRenderer.gameObject.SetActive(true);
        OrderToChangeBacgroundSprite();
        control.OperationClearData();
        control.spawnControll.CommandFillThePoll();
        control.levelMax = control.myLevels.Length;
        
        control.levelCurrent = control.testValue;
        
        control.uIController.CommandToSetALevel();
        
    }



    public void ButtonLaunchLevel(){
        levelDescribtionPanel.gameObject.SetActive(false);
        control.isLevelDescriptionActive = false;
        control.CommandStartALevel();
    }

    public void ButtonToNextWorkDay(){
        levelResultPanel.gameObject.SetActive(false);
        control.isLevelResultActive = false;
        control.isEndOfTheLevel = false;
        control.CommandNextLevel();
    }


    


    public void ButtonOkayOnTheEndGamePanel(){
        endWictoryPanel.gameObject.SetActive(false);
        control.isWictory = false;
        OrderActivateMainMenu(true);
        ButtonToOperateCredits(true);
    }




    public void OrderActivateMainMenu(bool option){
        if(option){
            mainMenuPanel.gameObject.SetActive(true);
            control.isMainMenuActive = true;
            soundController.CommandPlayAMenuMusic();
            }
        else{mainMenuPanel.gameObject.SetActive(false);
            control.isMainMenuActive = false;}
    }

    public void OrderToChangeBacgroundSprite(){
        int chosenOne=0;
        Sprite chosenSprite = null;
        bool isChosed = false;

        for(int i = 0; i < 10; i++) {
            if(!isChosed){
                chosenOne = (int)Random.Range(-0.4f, (float)control.spriteBacgrounds.Length -0.6f);
                if(chosenOne < 0) chosenOne = 0;
                if(chosenOne > control.spriteBacgrounds.Length -1) chosenOne = control.spriteBacgrounds.Length -1;
                chosenSprite = control.spriteBacgrounds[chosenOne];

                if(playModeBacgroundSpriteRenderer.sprite != chosenSprite){
                    playModeBacgroundSpriteRenderer.sprite = chosenSprite;
                    isChosed = true;
                }
            }
        }

        if(!isChosed) playModeBacgroundSpriteRenderer.sprite = control.spriteBacgrounds[0];
                
    }

    public void OrderToActivateHud(bool option){
        if(option){
            hudDisplayPanel.gameObject.SetActive(true);
        }else{
            hudDisplayPanel.gameObject.SetActive(false);
        }
    }



    public void CommandToShowAnoucment(string theString){
        screenAnoucmentTextField.text = theString;
        screenAnoucmentTextField.gameObject.SetActive(true);
        isScreenAnoucmentOn = true;
    }

    public void CommandToSetALevel(){
        levelDescribtionPanel.gameObject.SetActive(true);
        control.isLevelDescriptionActive = true;
        levelDescriptionLevelNumber.text = "Allient Trash Atack, Day: "+ (control.levelCurrent +1);
        levelDescriptionMainText.text = control.myLevels[control.levelCurrent].descriptionMain;
        levelDescriptionSkillsText.text = control.myLevels[control.levelCurrent].descriptionSkills;
        levelDescriptionBossImage.sprite = control.myLevels[control.levelCurrent].spriteBoss;
    }

    public void CommandToShowLevelResults(){
        OrderToActivateHud(false);
        levelResultPanel.gameObject.SetActive(true);
        control.isLevelResultActive = true;
        levelResultLevelNumberText.text = "Allient Trash Atack, Day: "+ (control.levelCurrent +1) +" results:";
        if(control.isBossDead){
            leverResultBossText.text = "Alien Boss was Eliminated!";
        }else{
            leverResultBossText.text = "Alien Boss Escaped! His Last HP was: "+control.bossLastHp;
        }
        levelResultBoxText.text = "Trash Boxes Destroyed/Drop: "+ control.trashBoxShotDown 
            +"/"+ control.trashBoxDroped;
        levelResultPacketSweepedText.text = "Trash Sweaped: " +control.trashPacketSweeped  +" ("
            +control.trashPacketSweepedTotal +")";
        levelResultPacketOnFieldText.text = "Trash still on the field: " +control.trashPacketStillOnField;
        levelResultBulletFiredText.text = "Ammunition Fired: " +control.bulletsFired +" ("
            +control.bulletsFiredTotal +")";
            
        
    }


    

    public void CommandEndGameWictory(){
        endWictoryPanel.gameObject.SetActive(true);
        control.isWictory = true;
        endWictoryText.text = endWictoryString + control.trashPacketStillOnField;
        soundController.CommandPlayYouWin();
    }

    public void OperationValueFromSlidderSFX(){
        float recievedValue = (float)(settingsSFXSlider.value);
        soundController.sfxVolumeValue = recievedValue *0.1f;
        settingsSFXValueT.text = recievedValue * 10 +"%";
        soundController.CommandAudioVolumeCheckUp();
    }

    public void OperationValueFromSlidderMusic(){
        float recievedValue = (float)(settingsMusicSlider.value);
        soundController.musicVolumeValue = recievedValue *0.1f;
        settingsMusicValueT.text = recievedValue * 10 +"%";
        soundController.CommandAudioVolumeCheckUp();
    }
}
