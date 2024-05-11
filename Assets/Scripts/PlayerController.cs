using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
//using Unity.VisualScripting;
//using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
//using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    [Header("Values")]
    public float speedMove = 5;
    public bool isMovingLeft= true, isStunned, isStunProtected;

    public float stunedInterval=4f, stunProtectionInterval=3f;
    float stunTimer=0;
    Vector3 pos = new Vector3 (0,0,0);
    float inputHorizontalValue=0;

    public float BulletFireIntervalNormal= 0.5f; float BulletFireTimer = 0;
    public float HeavyBulletFireIntervalNormal= 1f; float HeavyBulletFireTimer = 0;
    public int heavyBulletsReserve=0;
    public bool isReloadingHeavyBullet, isRevolverSpining;
    
    [Header("Relefernces")]
    public GameController controll;
    public SpriteRenderer mySpriteRenderer;
    public AudioSource bulletAS, heavyBulletAS, heavyBulletReloadingAS, revolverSpinAS;
    public AudioSource pickUpAS;

    private void Awake() {
        
    }

    private void Start() {
        if(controll == null) controll = GameController.controll;
        CommandChecekAudioSettings();
    }

    
    
    void Update()
    {
        if(controll.isPlayModeOn){
            pos = transform.position;
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
                    controll.magnumRevolver.ChangeOnBulletsInChamber();
                }

                if(isStunProtected)
                if(stunTimer > stunProtectionInterval){
                    stunTimer = 0;
                    isStunProtected = false;
                    mySpriteRenderer.color = Color.white;
                }
            }
        }//play mode on

        if(controll.isFightOn && !isStunned){
            BulletFireTimer -= Time.deltaTime;
            HeavyBulletFireTimer -= Time.deltaTime;

            
            if(BulletFireTimer < 0){
                BulletFireTimer = BulletFireIntervalNormal;                
                controll.spawnControll.CommandToSpawnABullet(transform.position, Quaternion.identity);
                bulletAS.Play();
            }
            
            if(Input.GetButton ("Fire1"))
                CommandFireSequence();

            if(isReloadingHeavyBullet){
                if(HeavyBulletFireTimer < 0){
                    HeavyBulletFireTimer = HeavyBulletFireIntervalNormal;
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
                    controll.magnumRevolver.ChangeOnBulletsInChamber();
                }
            }
        }


        

    }


    public void CommandFireSequence(){
        if(!isReloadingHeavyBullet && !isStunned){                
            if(HeavyBulletFireTimer < 0){
                HeavyBulletFireTimer = HeavyBulletFireIntervalNormal;
                controll.spawnControll.CommandToSpawnAHeavyBullet(transform.position, Quaternion.identity);
                heavyBulletAS.Play();
                heavyBulletsReserve --;  
                controll.magnumRevolver.ChangeOnBulletsInChamber();
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
        bulletAS.volume = controll.soundController.sfxVolumeValue;
        heavyBulletAS.volume = controll.soundController.sfxVolumeValue;
        heavyBulletReloadingAS.volume = controll.soundController.sfxVolumeValue;
        revolverSpinAS.volume = controll.soundController.sfxVolumeValue;
        pickUpAS.volume = controll.soundController.sfxVolumeValue;
    }


    public void OperationHorisontalMovement(){
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

    public void GotHitByABomb(){
        if(!isStunned && !isStunProtected){
            isStunned = true;
            mySpriteRenderer.color = Color.red;  
            controll.magnumRevolver.ChangeOnBulletsInChamber();
        }        
    }
}
