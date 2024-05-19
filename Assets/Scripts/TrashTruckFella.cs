using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashTruckFella : MonoBehaviour
{
    public SpriteRenderer mySpriteRenderer;
    public GameController gameController;
    public float speedMove = 3f;
    public int trashCollected=0;

    public bool isMovingLeft=false, isOnDuty, isStunned, isStunProtected;
    public float stunedInterval=4f, stunProtectionInterval=3f;
    float stunTimer=0;
    Vector3 pos;
    public Sprite[] mySprites;
    public AudioSource collectingAS;

    void Start()
    {
        if(gameController == null) gameController= GameController.controll;
        if(mySprites.Length > 0){
            int chosenOne = (int) Random.Range (-0.4f, mySprites.Length -1);
            if(mySprites[chosenOne] != null){
                mySpriteRenderer.sprite = mySprites[chosenOne];
            }
        }
    }

    
    void Update()
    {
        if(isOnDuty){
            pos = transform.position;
            if(isMovingLeft){
                pos.x -= speedMove * Time.deltaTime;
                if(pos.x < - 8){
                    ChangeDirection();
                    EndOfDuty();
                }

            }else{
                pos.x += speedMove * Time.deltaTime;
                if(pos.x > 8){                    
                    ChangeDirection();
                    EndOfDuty();
                }
            }
            transform.position = pos;
        }

        if(isStunned || isStunProtected){
                stunTimer += Time.deltaTime;

                if(isStunned)
                if(stunTimer > stunedInterval){
                    stunTimer = 0;
                    isStunned = false;
                    isStunProtected = true;
                    mySpriteRenderer.color = Color.blue;
                }

                if(isStunProtected)
                if(stunTimer > stunProtectionInterval){
                    stunTimer = 0;
                    isStunProtected = false;
                    mySpriteRenderer.color = Color.white;
                }
            }
    }

    public void ChangeDirection(){
        if(!isMovingLeft){
            isMovingLeft = true;
            mySpriteRenderer.flipX = true;
        }else{
            isMovingLeft = false;
            mySpriteRenderer.flipX = false;
        }
    }

    public void EndOfDuty(){
        isOnDuty = false;
        gameController.OperationAddSweapedTrashPacked(trashCollected);
        trashCollected = 0;
        gameController.trashTruckSweeping -= 1;
        gameController.soundController.CommandPlayRecicleBin();
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Trash")
        if(other.GetComponent<Box>().isPacket)
        {
            trashCollected++;
            other.GetComponent<Box>().Deactivate();
            collectingAS.Play();
        }
    }

    public void GotHitByABomb(){
        if(!isStunned && !isStunProtected){
            isStunned = true;
            mySpriteRenderer.color = Color.red;  
        }        
    }
}
