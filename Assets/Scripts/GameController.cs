using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int testValue=0;
    [Header("Bools")]
    public bool isTestingModeOn;
    public bool isPlayModeOn, isFightOn, isTrashTruckFellaOnField, isEndOfTheLevel, isWictory;
    public bool isBossDead;
    public bool isMainMenuActive, isSettingsActive, isCreditsActive,
         isLevelDescriptionActive, isLevelResultActive;


    [Header("Variables")]
    public int levelCurrent = 0; public int levelMax=0;
    public int trashBoxDroped = 0, trashBoxShotDown = 0;
    public int trashPacketDroped=0, trashPacketSweeped=0, trashPacketStillOnField=0;
    public int bulletsFired=0;
    
    
    
    public int bossesDestroyed=0, bossLastHp=0;
    public int trashBoxDropedTotal=0, trashBoxShotDownTotal=0, trashPacketDropedTotal=0,
        trashPacketSweepedTotal=0, bulletsFiredTotal=0;
    public int trashTruckSweeping=0;

    public bool trashFellaPeriodicalArriveBool; public float trashFellaInterval =120;
    float trashFellaTimer=0;


    

    
    [Header("Prefabs and Such")]
    public Sprite[] spriteBacgrounds;
    public LevelDescription[] myLevels;
    


    [Header("Relefernces")]
    public static GameController controll;
    public UIController uIController;
    public SpawnControll spawnControll;
    public SoundController soundController;
    public Camera myCamera;
    public Transform playerTransform, bossTransform;
    public BossController bossController;



    void Awake() {
        controll = this;
    }
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if(isEndOfTheLevel){
            if(trashTruckSweeping == 0){
                if(isTrashTruckFellaOnField){
                    isTrashTruckFellaOnField = false;
                    isPlayModeOn = false;
                    trashPacketStillOnField = trashPacketDroped - trashPacketSweeped;

                    trashBoxDropedTotal += trashBoxDroped;
                    trashBoxShotDownTotal += trashBoxShotDown;
                    trashPacketDropedTotal += trashPacketDroped;
                    trashPacketSweepedTotal += trashPacketSweeped;
                    bulletsFiredTotal += bulletsFired;

                    uIController.CommandToShowLevelResults();
                }
            }
        }

        if(isFightOn){
            trashFellaTimer +=Time.deltaTime;
            if(trashFellaTimer > trashFellaInterval){
                trashFellaTimer = 0;
                spawnControll.CommandSpawnATrashTruckFella();
            }
        }
    }

    public void CommandStarTheGame(){
        OperationClearData();
        spawnControll.CommandFillThePoll();
        levelMax = myLevels.Length;
        uIController.CommandToSetALevel();
    }


    public void CommandNextLevel(){
        levelCurrent++;
        if(levelCurrent < levelMax){
            trashBoxDroped=0;
            trashBoxShotDown=0;
            trashPacketDroped=0;
            trashPacketSweeped=0;
            bulletsFired = 0;
            //CommandStartALevel();
            uIController.CommandToSetALevel();
        }else{
            uIController.CommandEndGameWictory();
        }
    }


    public void CommandStartALevel(){
        isPlayModeOn = true;
        if(playerTransform == null){
            spawnControll.CommandSpawnNewPlayer();
        }
        spawnControll.CommandSpawnABossFromLevelDescription();
        soundController.CommandPlayInGameMusic();
        uIController.OrderToActivateHud(true);
    }

    public void CommandToStartFight(){
        isFightOn = true;
        uIController.CommandToShowAnoucment("Fight!");
        soundController.CommandPlayRooster();
    }

    public void CommandBossWasShutDown(){
        isFightOn = false;
        isEndOfTheLevel = true;
        isBossDead = true;
        bossesDestroyed++;
        uIController.CommandToShowAnoucment("Justice!");
        CommandRunASweepOperation();
        soundController.CommandPlayCheer();
    }

    public void CommandBossEscaped(int bossHpLeft){
        isFightOn = false;
        isEndOfTheLevel = true;
        isBossDead = false;
        bossLastHp = bossHpLeft;
        bossTransform.GetComponent<InstantDestroy>().DestroyMe();
        uIController.CommandToShowAnoucment("Dang It!");
        CommandRunASweepOperation();
        soundController.CommandPlayYouLost();
    }

    public void CommandRunASweepOperation(){
        isEndOfTheLevel = true;
        spawnControll.CommandSpawnATrashTruckFella();
    }


    public void OperationClearData(){
        levelCurrent = 0;
        trashBoxDroped=0;
        trashBoxShotDown=0;
        trashPacketDroped=0;
        trashPacketSweeped=0;
        trashPacketStillOnField=0;
        bulletsFired = 0;

        bossesDestroyed=0;
        trashBoxDropedTotal=0;
        trashBoxShotDownTotal=0;
        trashPacketDropedTotal=0;
        trashPacketSweepedTotal=0;
        bulletsFiredTotal=0;
    }

    public void OperationAddSweapedTrashPacked(int number){trashPacketSweeped += number;}

    public void OperationAddShutedDownTrashBox(){trashBoxShotDown++;}

    public void OperationAddSpawnedTrashBox(){trashBoxDroped++;}

    public void OperationAddSpawnedTrashPacklet(){trashPacketDroped++;}
   

}
