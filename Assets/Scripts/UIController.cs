using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        levelResultPacketSweepedText, levelResultPacketOnFieldText, levelResultPolutionRate, 
        levelResultBulletFiredText, levelResultBulletHeavyFiredText, levelResultMissleFiredText;
    public TMP_Text endWictoryText;
    public TMP_Text buttonMmSwitchAcText;


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
    public NewsPaperController newsPaperController;
    public PoluttionMystControll poluttionMystControll;

    [Header("The Steeings")]
    public Transform settingsPanel;
    public TMP_Text settingsSFXValueT, settingsMusicValueT;
    public Slider settingsSFXSlider, settingsMusicSlider;


    [Header("Hud Display")]
    public Transform hudDisplayPanel;
    public TMP_Text bossHPText; int bossHPValue=0; 
    public Image bossHPBarImage; float bossHpPercentage=1f;
    public Image trashColectedImage;
    public TMP_Text trashColectedText; string trashColectedMesage="";
    float trashColectedFillValue=0;


    [Header("Android Controll")]
    public Transform hudAnroidControllPanel;
    public TMP_Text hudAndroidSettingsValue;


    


    


   


    private void Awake() {
        uIC = this;
    }
    
    void Start()
    {
        if(control == null) control = GameController.controll;
        if(control != null){
            if(!control.isTestingModeOn){
                newsPaperController.OrderActivateFronNewsPaper(true, 0);
            }else{
                //curently no orders
                }
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
                    bossHpPercentage = ( (float)bossHPValue 
                    / (float)control.bossController.hitPointOriginal );
                    bossHPBarImage.fillAmount = bossHpPercentage;
                }            
            }else{
                bossHPText.text = "x";
                bossHPBarImage.fillAmount = 0;
            }
        }

        //on final release remove the test number
        //testNumber.text = control.spawnControll.detectedSpawns.ToString();
    }





    public void ButtonStartTheGame(){
        OrderActivateMainMenu(false);
        control.CommandStarTheGame();
        soundController.CommandPlayButtonPress();
        playModeBacgroundSpriteRenderer.gameObject.SetActive(true);
        OrderToChangeBacgroundSprite();
    }

    public void ButtonEndApplication(){
        Application.Quit();
        soundController.CommandPlayButtonPress();
        #if UNITY_PLAYER
        #endif
        Debug.Log($"Dearing to close app");
        //uncertain of effectivity on PC, web, or android
        //Test it then decide
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        #if UNITY_ADROID
            AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call<bool>("moveTaskToBack", true);            
        #endif
    }

    public void ButtonToOperateTheSettingsMenu(bool option){
        soundController.CommandPlayButtonPress();
        //this is only theory for now
        //EventSystem.current.SetSelectedGameObject(null);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject (null);
        
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
        control.CommandPausingTheGame(option);
    }

    public void ButtonToOperateCredits(bool option){
        soundController.CommandPlayButtonPress();
        if(option){
            creditsPanel.gameObject.SetActive(true);
            control.isCreditsActive = true;
        }else{
            creditsPanel.gameObject.SetActive(false);
            control.isCreditsActive = false;
        }
    }


    public void ButtonTestTheBoss(){
        soundController.CommandPlayButtonPress();
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
        soundController.CommandPlayButtonPress();
        levelDescribtionPanel.gameObject.SetActive(false);
        control.isLevelDescriptionActive = false;
        control.CommandStartALevel();
    }

    public void ButtonToNextWorkDay(){
        soundController.CommandPlayButtonPress();
        levelResultPanel.gameObject.SetActive(false);
        control.isLevelResultActive = false;
        control.isEndOfTheLevel = false;
        control.CommandNextLevel();
    }


    


    public void ButtonOkayOnTheEndGamePanel(){
        soundController.CommandPlayButtonPress();
        endWictoryPanel.gameObject.SetActive(false);
        control.isWictory = false;
        soundController.CommandPlayAMenuMusic();
        ButtonToOperateCredits(true);
        newsPaperController.OrderActivateFronNewsPaper(true, 1);
    }



    public void ButtonFrontNewsPaperContinue(){
        newsPaperController.OrderActivateFronNewsPaper(false, 0);
    }


    




    


    public void OrderActivateMainMenu(bool option){
        mainMenuPanel.gameObject.SetActive(option);
        control.isMainMenuActive = option;
        if(option){            
            soundController.CommandPlayAMenuMusic();
        }
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
            control.magnumRevolver.ChangeOnBulletsInChamber();
        }else{
            hudDisplayPanel.gameObject.SetActive(false);
        }
    }

   

    public void OrderForMobileControllActivation(){
        bool thisValue = !control.isAndroidControlActive;
        
        control.isAndroidControlActive = thisValue;
        hudAnroidControllPanel.gameObject.SetActive(thisValue);
        hudAndroidSettingsValue.text = thisValue.ToString();
        buttonMmSwitchAcText.text = "Androind Controll: "+ thisValue;
        
        Debug.Log($"activating mobile controll "+ thisValue);
    }






    public void CommandUpdateCollectedTrashInformations(){
        if(control.playerController != null){
            trashColectedMesage = control.playerController.collectedTrashCurrent 
                +"/"+ control.playerController.collectedTrashCapacity;
            trashColectedFillValue = control.playerController.collectedTrashCurrent 
                / control.playerController.collectedTrashCapacity;
        }else{
            trashColectedMesage = "0";
            trashColectedFillValue = 0;
        }
        trashColectedText.text = trashColectedMesage;
        trashColectedImage.fillAmount = trashColectedFillValue;
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
        string myString ="";
        OrderToActivateHud(false);
        poluttionMystControll.CommandActivatePolutionPanel(false);
        levelResultPanel.gameObject.SetActive(true);
        control.isLevelResultActive = true;
        levelResultLevelNumberText.text = "Allient Trash Atack, Day: "+ (control.levelCurrent +1) +" results:";
        if(control.isBossDead){
            myString = "Alien Boss was Eliminated!";
        }else{
            myString = "Alien Boss Escaped! His Last HP was: "+control.bossLastHp;
        }  
        myString += " ["+ control.bossesDestroyed +"]";
        leverResultBossText.text = myString;      
        levelResultBoxText.text = "Trash Boxes Destroyed/Drop: "+ control.trashBoxShotDown 
            +"/"+ control.trashBoxDroped;
        levelResultPacketSweepedText.text = "Trash Sweaped: " +control.trashPacketSweeped  +" ["
            +control.trashPacketSweepedTotal +"]";
        levelResultPacketOnFieldText.text = "Trash still on the field: " +control.trashPacketStillOnField;
        levelResultPolutionRate.text = "Polution rate is: " +
        ((int)((float)control.trashPacketStillOnField 
            / (float)poluttionMystControll.pecketsOneHundretPercentValue * 100f)
            )
        + "%";
        levelResultBulletFiredText.text = "Basic auto Ammunition Fired: " +control.bulletsFired +" ["
            +control.bulletsFiredTotal +"]";
        levelResultBulletHeavyFiredText.text = "Magnum Ammo Fired: " +control.bulletsHeavyFirred +" ["
            +control.bulletsHeavyFiredTotal +"]";
        levelResultMissleFiredText.text = "Missle Ammo Fired: " +control.missleFired +" ["
            +control.missleFiredTotal +"]";   
        
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
