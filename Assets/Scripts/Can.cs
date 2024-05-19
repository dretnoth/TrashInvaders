using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can : MonoBehaviour
{
    public GameController gameControler;
    public Transform[] versionsOfCans;
    public Collider2D myColider;
    int lastVersionOfCan=0;
    public int hitPointCurrent =5, hitPointMax=5;
    public bool isBroken;
    public Color[] myColor;
    void Start()
    {
        gameControler = GameController.controll;
        OperationChoseVersionOfCan();
    }

    

    void OperationChoseVersionOfCan(){
        versionsOfCans[lastVersionOfCan].gameObject.SetActive(false);
        int chosenOne = (int)Random.Range(0f, (float)versionsOfCans.Length -1f);
        if(chosenOne < 0) chosenOne = 0;
        if(chosenOne > versionsOfCans.Length -1) chosenOne = versionsOfCans.Length -1;
        versionsOfCans[chosenOne].gameObject.SetActive(true);
        myColider = versionsOfCans[chosenOne].GetComponent<Collider2D>();
        lastVersionOfCan = chosenOne;
    }

    public void CommandGotHit(int damage){
        hitPointCurrent -= damage;
        gameControler.spawnControll.CommandToSpawnAFlashPuff(transform.position);
            if(hitPointCurrent <= 0){
                CommandForCanToBreak();
            }
    }

    public void CommandForCanToBreak()
    {
        if(!isBroken){
            isBroken = true;    
            
            int numberOfPackets = (int) Random.Range(3f, 9f);
            Quaternion angle = transform.rotation;
            Vector3 pos = transform.position;

            for(int i = 0; i < numberOfPackets; i++) {
                if(i==1) pos = new Vector3(transform.position.x -0.2f, transform.position.y, 0);
                if(i==2) pos = new Vector3(transform.position.x -0.2f, transform.position.y -0.2f, 0);
                if(i==3) pos = new Vector3(transform.position.x -0.2f, transform.position.y +0.2f, 0);
                
                if(i==4) pos = new Vector3(transform.position.x , transform.position.y -0.2f, 0);
                if(i==5) pos = new Vector3(transform.position.x , transform.position.y +0.2f, 0);

                if(i==6) pos = new Vector3(transform.position.x +0.2f, transform.position.y -0.2f, 0);
                if(i==7) pos = new Vector3(transform.position.x +0.2f, transform.position.y , 0);
                if(i==8) pos = new Vector3(transform.position.x +0.2f, transform.position.y +0.2f, 0);
                
                gameControler.spawnControll.CommandToSpawnATrashPacklet(
                    pos, transform.rotation, myColor[lastVersionOfCan]);
                }
            gameControler.spawnControll.CommandToSpawnAExplosion(transform.position);
            gameControler.spawnControll.controller.OperationAddShutedDownTrashBox();
        }

        gameControler.OperationAddShutedDownTrashBox();
        Deactivate();
    }


    public void Deactivate(){
        isBroken = false;
        hitPointCurrent = hitPointMax;
        OperationChoseVersionOfCan();
        this.gameObject.SetActive(false);
    }

}
