using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public SpawnControll spawnControll;

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
            other.GetComponent<PlayerController>().GotHitByABomb();
        }

        if(iamDone){
            pos = transform.position;
            pos.y = -3.5f;
            spawnControll.CommandToSpawnAGroundExplosion(pos);
            this.gameObject.SetActive(false);
        }
    }
}
