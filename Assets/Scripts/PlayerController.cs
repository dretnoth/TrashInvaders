using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Values")]
    public float speedMove = 5;
    public bool isMovingLeft= true, isStunned, isStunProtected, isBussy, 
        isCollectingTrash, isBussyWhitATrash, isDumpingTrash,
        isLeavingToDumpATrash, isReturningFromDumping,
        isFastFireOn;

    public float stunedInterval=4f, stunProtectionInterval=3f;
    float stunTimer=0;
    Vector3 pos = new Vector3 (0,0,0);
    float inputHorizontalValue=0;

    public float BulletFireIntervalNormal= 0.5f; float BulletFireTimer = 0;
    public float HeavyBulletFireIntervalNormal= 1f; float HeavyBulletFireTimer = 0;
    public int heavyBulletsReserve=0, fastFireAmooReserve=0, fastFireAmmoRate=50;
    public bool isReloadingHeavyBullet, isRevolverSpining;
    public float collectingTrashRange=1.5f, colectingTrashInterval=0.5f,
        dumpingTrashinterval=6f;
    float trashCollectingtimer=0, trashDumpingTimer=0;
    public int collectedTrashCurrent=0, collectedTrashCapacity=30;
    
    [Header("Relefernces")]
    public GameController gameController;
    public SpriteRenderer mySpriteRenderer;
    public AudioSource bulletAS, heavyBulletAS, heavyBulletReloadingAS, revolverSpinAS;
    public AudioSource pickUpAS, dumpingTrashAS, startEngineAS, counterBellAS;

    private void Awake() {
        
    }

    private void Start() {
        if(gameController == null) gameController = GameController.controll;
        CommandChecekAudioSettings();
    }

    
    
    void Update()
    {
        pos = transform.position;
        if(gameController.isPlayModeOn){
            inputHorizontalValue = Input.GetAxis("Horizontal");

            if(inputHorizontalValue != 0 && !isStunned) {
                OperationHorisontalMovement();
            }

            if(isStunned || isStunProtected){
                stunTimer += Time.deltaTime;

                if(isStunned)
                if(stunTimer > stunedInterval){
                    stunTimer = 0;
                    isStunned = false;
                    isStunProtected = true;
                    mySpriteRenderer.color = Color.blue;
                    gameController.magnumRevolver.ChangeOnBulletsInChamber();
                }

                if(isStunProtected)
                if(stunTimer > stunProtectionInterval){
                    stunTimer = 0;
                    isStunProtected = false;
                    mySpriteRenderer.color = Color.white;
                }
            }
        }//play mode on

        if(gameController.isFightOn && !isStunned){
            BulletFireTimer -= Time.deltaTime;
            HeavyBulletFireTimer -= Time.deltaTime;
            trashCollectingtimer -= Time.deltaTime;

            
            if(BulletFireTimer < 0 && !isBussy){
                if(!isFastFireOn){ BulletFireTimer = BulletFireIntervalNormal;                
                }else{
                    BulletFireTimer = BulletFireIntervalNormal * 0.5f;
                    OperationFastFireAmmoDrop();
                    }
                gameController.spawnControll.CommandToSpawnABullet(transform.position, Quaternion.identity);
                bulletAS.Play();
            }
            
            if(Input.GetButton ("Fire1"))
                CommandFireSequence();

            if(Input.GetButton ("Fire3"))
                CommandColectingNearlyTrash();

            if(isReloadingHeavyBullet){
                if(HeavyBulletFireTimer < 0){
                    if(!isFastFireOn)
                        HeavyBulletFireTimer = HeavyBulletFireIntervalNormal;
                    else
                        HeavyBulletFireTimer = HeavyBulletFireIntervalNormal *0.5f;
                    heavyBulletsReserve ++; 
                    if(heavyBulletsReserve > 6){
                       isReloadingHeavyBullet = false; 
                       heavyBulletsReserve = 6;
                       HeavyBulletFireTimer = 3f;
                       isRevolverSpining = true;
                       revolverSpinAS.Play();
                    }else{
                        heavyBulletReloadingAS.Play();
                    }
                    if(isRevolverSpining && heavyBulletsReserve == 6){
                        isRevolverSpining = false;
                    }
                    gameController.magnumRevolver.ChangeOnBulletsInChamber();
                }
            }
        }


        if(isBussy){
            if(isBussyWhitATrash){
                if(isLeavingToDumpATrash){
                    pos.x -= speedMove * Time.deltaTime;
                    transform.position = pos;
                    if(pos.x < -8.5f){
                        isLeavingToDumpATrash = false;
                        isDumpingTrash = true;
                        dumpingTrashAS.Play();
                    }
                }

                if(isDumpingTrash){
                    trashDumpingTimer += Time.deltaTime;
                    if(trashDumpingTimer > dumpingTrashinterval){
                        trashDumpingTimer = 0;
                        isDumpingTrash = false;
                        isReturningFromDumping = true;
                        isMovingLeft = false;
                        mySpriteRenderer.flipX = true;
                        gameController.OperationAddSweapedTrashPacked(collectedTrashCurrent);
                        OperationDropCoolectedTrash();
                        startEngineAS.Play();
                    }
                }

                if(isReturningFromDumping){
                    pos.x += speedMove * Time.deltaTime;
                    transform.position = pos;
                    if(pos.x >= -0){
                        isReturningFromDumping = false;
                        isBussy = false;
                        isBussyWhitATrash = false;
                        gameController.soundController.audioTruckHonk.Play();
                    }
                }
            }//isBussyWhitATrash
        }//bussy

    }


    public void CommandFireSequence(){
        if(!isReloadingHeavyBullet && !isStunned && !isBussy){                
            if(HeavyBulletFireTimer < 0){
                if(!isFastFireOn){HeavyBulletFireTimer = HeavyBulletFireIntervalNormal;}
                else{
                    HeavyBulletFireTimer = HeavyBulletFireIntervalNormal *0.5f;
                    OperationFastFireAmmoDrop();
                }
                
                gameController.spawnControll.CommandToSpawnAHeavyBullet(transform.position, Quaternion.identity);
                heavyBulletAS.Play();
                heavyBulletsReserve --;  
                gameController.magnumRevolver.ChangeOnBulletsInChamber();
                if(heavyBulletsReserve <= 0) {
                    isReloadingHeavyBullet = true;;
                }
            }
        }
    }


    public void ButtonInputHorisontalMovement(bool leftOfRigt){
        if(leftOfRigt == false){
            inputHorizontalValue = -1;
        }else{
            inputHorizontalValue = 1;
        }
        if(inputHorizontalValue != 0 && !isStunned) {
            OperationHorisontalMovement();
        }
    }

    public void CommandChecekAudioSettings(){
        bulletAS.volume = gameController.soundController.sfxVolumeValue;
        heavyBulletAS.volume = gameController.soundController.sfxVolumeValue;
        heavyBulletReloadingAS.volume = gameController.soundController.sfxVolumeValue;
        revolverSpinAS.volume = gameController.soundController.sfxVolumeValue;
        pickUpAS.volume = gameController.soundController.sfxVolumeValue;
        dumpingTrashAS.volume = gameController.soundController.sfxVolumeValue;
        startEngineAS.volume = gameController.soundController.sfxVolumeValue;
        counterBellAS.volume = gameController.soundController.sfxVolumeValue;
    }


    public void OperationHorisontalMovement(){
        if(!isBussy){
            pos.x += inputHorizontalValue * speedMove * Time.deltaTime;

                if(inputHorizontalValue < 0) if(isMovingLeft == false) {
                    mySpriteRenderer.flipX = false;
                    isMovingLeft = true;
                }
                if(inputHorizontalValue > 0) if(isMovingLeft == true) {
                    mySpriteRenderer.flipX = true;
                    isMovingLeft = false;
                }
            
                if(pos.x < -6) pos.x = -6;
                if(pos.x > 6) pos.x = 6;
                transform.position = pos;
        }
    }

    public void CommandGotHitByABomb(){
        if(!isStunned && !isStunProtected){
            isStunned = true;
            mySpriteRenderer.color = Color.red;  
            gameController.magnumRevolver.ChangeOnBulletsInChamber();
        }        
    }


    public void CommandColectingNearlyTrash(){
        if(!isStunned && !isBussy){
            if(trashCollectingtimer < 0){
                trashCollectingtimer = colectingTrashInterval;
                Vector3 myPosition =transform.position;
                myPosition.y -=0.4f;
                RaycastHit2D[] hit = Physics2D.RaycastAll(myPosition, Vector2.one);
                bool isDone = false;
                if(hit != null){
                    for(int i = 0; i < hit.Length; i++) {
                        if(!isDone)
                        if(hit[i].transform.gameObject.activeInHierarchy == true)
                        if(hit[i].transform.gameObject.tag == "Trash"){
                            isDone = true;
                            hit[i].transform.GetComponent<Box>().Deactivate();                            
                            OperationpickedUpTrashPackelt();
                        }
                    }
                }
            }
        }
    }//trash

    void OperationpickedUpTrashPackelt(){
        collectedTrashCurrent++;
        pickUpAS.Play();
        gameController.uIController.CommandUpdateCollectedTrashInformations();
        if(collectedTrashCurrent >= collectedTrashCapacity){
            isBussy = true;
            isBussyWhitATrash = true;
            isLeavingToDumpATrash = true;
            if(heavyBulletsReserve < 6) isReloadingHeavyBullet = true;
            if(!isMovingLeft){
                mySpriteRenderer.flipX = false;
                isMovingLeft = true;                
            }
            gameController.soundController.CommandPlayTruckHonk();
        }
    }


    void OperationFastFireAmmoDrop(){
        fastFireAmooReserve--;
        if(fastFireAmooReserve <= 0){
            isFastFireOn = false;
        }
    }

    public void OperationDropCoolectedTrash(){
        collectedTrashCurrent = 0;
        gameController.uIController.CommandUpdateCollectedTrashInformations();
    }


    public void CommandSupplyBoxObtained(){
        counterBellAS.Play();
        int choseOne = (int)Random.Range(-0.4f, 2.4f);
        if(choseOne < 0) choseOne = 0;
        if(choseOne > 2) choseOne = 2; //curently one

        if(choseOne == 0){
            gameController.spawnControll.CommandToSpanwAMissle(
                true, false, false, Vector3.zero);
        }else if(choseOne == 1){
            gameController.spawnControll.CommandSpawnAArmyCar();
        }else if(choseOne == 2){
            isFastFireOn = true;
            fastFireAmooReserve += fastFireAmmoRate;
        }
    }


    public void CommandForceMagnumReload(){
        if(heavyBulletsReserve < 6){isReloadingHeavyBullet = true;}
    }
}
