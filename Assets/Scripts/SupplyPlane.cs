using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyPlane : MonoBehaviour
{
    public GameController gameController;
    public float speedMove = 3f, rangeToDropTheSupply=3f;
    float distanceToDropArea=0;
    public bool isSupplyDropped=false, isFlyingRight, isFlyingLeft;
    Vector3 cordinationsToDropTheSupply, myPosition;
    public SpriteRenderer spriteRenderer;
    public AudioSource audioSource;
    
    
    
    void Start()
    {
        gameController = GameController.controll;
        cordinationsToDropTheSupply = transform.position;
        cordinationsToDropTheSupply.x = Random.Range(-rangeToDropTheSupply, rangeToDropTheSupply);
        //probably unable to find
        //if(spriteRenderer == null) spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        audioSource.volume = gameController.soundController.sfxVolumeValue;
    }

    
    void Update()
    {
        myPosition = transform.position;
        if(!isSupplyDropped){
            distanceToDropArea = Vector3.Distance(myPosition, cordinationsToDropTheSupply);
            if(distanceToDropArea < 0.5f){
                CommandDropTheSupply();
            }    
        }
        if(isFlyingRight){
            myPosition.x += speedMove * Time.deltaTime;
            if(myPosition.x > 8.5f){
                DestroyMe();
            }
        }  
        if(isFlyingLeft){
            myPosition.x -= speedMove * Time.deltaTime;
            if(myPosition.x < -8.5f){
                DestroyMe();
            }
        }    
        transform.position = myPosition;  
    }

    void CommandDropTheSupply(){
        isSupplyDropped = true;
        gameController.spawnControll.CommandSpawnASupplyCrate(transform.position);
    }

    public void CommandSetFlyDirectionToRight(){
        Vector3 pos = transform.position;
        bool option = false;        
        if(Random.Range(0,100)>50){option = true;}
        
        if(option){
            isFlyingRight = true;
            pos.x = -9;
        }else{
            isFlyingLeft = true;
            pos.x = 9;
            spriteRenderer.flipX = true;
        }
        transform.position = pos;
    }

    public void DestroyMe(){Destroy(gameObject);}

}
