using System.Collections;
using System.Collections.Generic;
//using Unity.Mathematics;
using UnityEngine;

public class Box : MonoBehaviour
{
    public bool isBox, isPacket, isOnGround;
    bool isBoxDone = false, isGoingToBreak = false, isBroken = false;
    public int hitPointCurrent=2, hitPointMax=2;
    public Color myColor;
    public SpriteRenderer mySpriteRenderer;
    public SpawnControll spawnControll;
    float packletTimer = 3f;
    
    public void SetMe(Color newColor){
        myColor = newColor;
        mySpriteRenderer.color = myColor;
    }

    public void Start(){
        if(spawnControll == null){
            spawnControll = SpawnControll.spawnControll;
        }
    }

    void Update() {
        if(isGoingToBreak){
            if(!isBroken){
                isBroken = true;
                CommandForBoxToBreak(false, true); 
            }
        }

        if(isPacket)
            if(!isOnGround){
                packletTimer -= Time.deltaTime;
                if(packletTimer < 0) isOnGround = true;
            }
    }


    
    private void OnCollisionEnter2D(Collision2D other) 
    {       
        if(isBox && !isBoxDone ){
            if(other.gameObject.tag == "Ground"){
                isBoxDone = true;
            }
            if(other.gameObject.tag == "Trash"){
                if(other.gameObject.GetComponent<Box>().isOnGround == true)
                    isBoxDone = true;
            }
            if(other.gameObject.tag == "Boss"){
                if(other.gameObject.GetComponent<BossController>().isDeath){
                    isBoxDone = true;
                }
            }
            
            if(isBoxDone ){
                isGoingToBreak = true;             
            }    
        }

        if(isPacket && !isOnGround){
            if(other.gameObject.tag == "Ground") isOnGround = true;
            if(other.gameObject.tag == "Trash")
                if(other.gameObject.GetComponent<Box>().isOnGround == true)
                    isOnGround = true;
            if(other.gameObject.tag == "Supply")
                if(other.gameObject.GetComponent<SupplyCrate>().isOnGround == true)
                    isOnGround = true;
        }
    

    }


    public void CommandGotHit(int damage){
        hitPointCurrent -= damage;
            if(hitPointCurrent <= 0){
                CommandForBoxToBreak(true, false);
            }
    }


    public void CommandForBoxToBreak(bool isHitedByWeapon, bool isTouchedGround)
    {
        isBroken = true;        
        if(isHitedByWeapon){
            
                int numberOfPackets = (int) Random.Range(2, 5);
                Quaternion angle = transform.rotation;
                for(int i = 0; i < numberOfPackets; i++) {
                    angle = Quaternion.Euler(0,0, Random.Range(-45, 45f)) ;
                    spawnControll.CommandToSpawnATrashPacklet(transform.position, angle, myColor);
                }
            spawnControll.CommandToSpawnAExplosion(transform.position);
            spawnControll.controller.OperationAddShutedDownTrashBox();
        }

        if(isTouchedGround){
            Vector3 pos = transform.position;
            for(int i = 0; i < 9; i++) {
                if(i==1) pos = new Vector3(transform.position.x -0.2f, transform.position.y, 0);
                if(i==2) pos = new Vector3(transform.position.x -0.2f, transform.position.y -0.2f, 0);
                if(i==3) pos = new Vector3(transform.position.x -0.2f, transform.position.y +0.2f, 0);
                
                if(i==4) pos = new Vector3(transform.position.x , transform.position.y -0.2f, 0);
                if(i==5) pos = new Vector3(transform.position.x , transform.position.y +0.2f, 0);

                if(i==6) pos = new Vector3(transform.position.x +0.2f, transform.position.y -0.2f, 0);
                if(i==7) pos = new Vector3(transform.position.x +0.2f, transform.position.y , 0);
                if(i==8) pos = new Vector3(transform.position.x +0.2f, transform.position.y +0.2f, 0);
                
                spawnControll.CommandToSpawnATrashPacklet(pos, Quaternion.identity, myColor);
            }            
        }        
        Deactivate();
    }


    public void Deactivate(){
        isBoxDone = false;
        isBroken = false;
        isGoingToBreak = false;
        hitPointCurrent = hitPointMax;
        isOnGround = false;
        packletTimer = 3;
        this.gameObject.SetActive(false);
    }
    
}
