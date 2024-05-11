using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public SpawnControll spawnControll;
    PlayerController myplayer = null;
    TrashTruckFella myTrashTruckFella = null;

    private void Start() {
        if(spawnControll == null) spawnControll = SpawnControll.spawnControll;
    }
    void OnTriggerEnter2D(Collider2D other){
        bool iamDone=false;
        Vector3 pos;
        if(other.tag == "Ground"){
            iamDone = true;            
        }
        if(other.tag == "Player"){
            iamDone = true;
            myplayer =  other.GetComponent<PlayerController>();
            if(myplayer != null) myplayer.GotHitByABomb();
            myTrashTruckFella = other.GetComponent<TrashTruckFella>();
            if(myTrashTruckFella != null) myTrashTruckFella.GotHitByABomb();
        }
        

        if(iamDone){
            pos = transform.position;
            pos.y = spawnControll.controller.groundSurfaceY + 0.9f;
            spawnControll.CommandToSpawnAGroundExplosion(pos);
            myplayer = null;
            myTrashTruckFella = null;
            this.gameObject.SetActive(false);
        }
    }
}
