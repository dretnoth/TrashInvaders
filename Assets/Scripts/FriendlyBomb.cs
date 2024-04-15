using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyBomb : MonoBehaviour
{
    public GameController gameController;
    public float explosionRange=2f;
    int trashCollected=0;

    void Start()
    {
        if(gameController == null) gameController = GameController.controll;
    }

    void OnTriggerEnter2D(Collider2D other){
        bool iamDone=false;
        Vector3 pos;
        if(other.tag == "Ground"){
            iamDone = true;     
            RaycastHit2D[] hit = Physics2D.CircleCastAll(
                transform.position, explosionRange, new Vector2 (-1,1));
            if(hit != null){
                for(int i = 0; i < hit.Length; i++) {
                    if(hit[i].transform.gameObject.activeInHierarchy == true)
                        if(hit[i].transform.tag == "trash")
                            if(hit[i].transform.GetComponent<Box>().isPacket){
                                hit[i].transform.GetComponent<Box>().Deactivate();
                                trashCollected++;
                            }
                }
            }
        }
        

        if(iamDone){
            pos = transform.position;
            pos.y = -3.5f;
            gameController.spawnControll.CommandToSpawnAGroundExplosion(pos);
            gameController.OperationAddSweapedTrashPacked(trashCollected);
            Destroy(gameObject);
        }
    }

}
