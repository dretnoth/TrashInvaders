using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyCar : MonoBehaviour
{
    public GameController gameController;
    public Transform targetDetected, targetBoss;
    public bool isAriving=true, isFigthing, isDeparting, 
        isDrivingLeft, isDrivingRight=true, isHavingTarget, 
        isStunned, isStunProtected;
    public int ammoReserve = 50;
    public float ammoInterval = 0.25f, speed = 2.2f;
    float ammoTimer=0;
    public float stunedInterval=4f, stunProtectionInterval=3f;
    float stunTimer=0;
    public SpriteRenderer mySpriteRenderer;
    Vector3 pos, targetPosition;
    public AudioSource audioGunShot;

    void Start()
    {
        if(gameController == null) gameController = GameController.controll;
        audioGunShot.volume = gameController.soundController.sfxVolumeValue;        
    }

    
    void Update()
    {
        pos = transform.position;
        if(!isStunned){
        if(isDrivingLeft){
            pos.x -= speed * Time.deltaTime;
            if(isAriving){
                if(pos.x < 6){
                    isAriving = false;
                    isFigthing = true;
                }
            }
            if(isFigthing){
                if(pos.x < -6){
                    OperationFlipX(true);
                }
            }
            if(isDeparting){
                if(pos.x < -9){
                    Destroy(gameObject);
                } 
            }
        }

        if(isDrivingRight){
            pos.x += speed * Time.deltaTime;
            if(isFigthing){
                if(pos.x > 6){
                    OperationFlipX(false);
                }
            }
            if(isDeparting){
                if(pos.x > 9){
                    Destroy(gameObject);
                } 
            }
        }

        transform.position = pos;

        if(isFigthing && gameController.isFightOn){
            ammoTimer -= Time.deltaTime;
            if(ammoTimer < 0){
                ammoTimer = ammoInterval;
                OperationFireBullet();
            }
        }    
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
    }


    void OperationFireBullet(){
        isHavingTarget = false;
        targetDetected = null;

        RaycastHit2D[] cans = Physics2D.RaycastAll(transform.position, Vector2.up, 10f);
        bool isTargetSet = false;
        if(cans != null){
            for(int i = 0; i < cans.Length; i++)
            if(!isTargetSet) {
                if(cans[i].transform.gameObject.activeInHierarchy == true){
                    if(cans[i].transform.tag == "Can"){
                        isTargetSet = true;
                        targetDetected = cans[i].transform;
                    }
                }
            }
        }

        if(targetBoss == null){
            if(gameController.bossTransform != null){
                if(gameController.bossTransform.gameObject.activeInHierarchy == true){
                    targetBoss = gameController.bossTransform;
                }else{
                    targetBoss = null;
                }
            }
        }else{
            if(gameController.bossTransform.gameObject.activeInHierarchy == false){
                targetBoss = null;
            }
        }

        if(targetDetected != null || targetBoss != null){
           if(targetDetected != null)
                {targetPosition = targetBoss.position;} 
            else if(targetBoss != null)
                {targetPosition = targetBoss.position;}
            isHavingTarget = true;
        }

        if(isHavingTarget){
            Vector3 direction = targetPosition - transform.position;
            float zAngle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg - 90;
            Quaternion    rotationToTarget = Quaternion.Euler (0, 0, zAngle);  
            gameController.spawnControll.CommandToSpawnABullet(transform.position, rotationToTarget);
            ammoReserve--;
            audioGunShot.Play();

            if(ammoReserve <= 0){
                isFigthing = false;
                isDeparting = true;
                gameController.spawnControll.CommandToSpanwAMissle(
                    false, false, true, transform.position
                );
            }
        }
    }


    void OperationFlipX(bool toRight){
        if(toRight){
            isDrivingLeft = false;
            isDrivingRight = true;
            mySpriteRenderer.flipX = true;
        }else{
            isDrivingLeft = true;
            isDrivingRight = false;
            mySpriteRenderer.flipX = false;
        }
    }

    public void CommandGotHitByABomb(){
        if(!isStunned && !isStunProtected){
            isStunned = true;
            mySpriteRenderer.color = Color.red;  
        }   
    }

}
