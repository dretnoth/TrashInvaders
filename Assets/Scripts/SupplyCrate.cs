using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyCrate : MonoBehaviour
{
    public Transform parachute;
    GameController gameController;
    public bool isOnGround, isParachuteDeployed;
    float parachuteTimer=0.5f;
    public Rigidbody2D myRigidbody2D=null;
    Vector3 pos;

    void Start()
    {
        gameController = GameController.controll;
        myRigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        if(!isParachuteDeployed){
            parachuteTimer -= Time.deltaTime;
            if(parachuteTimer < 0){
                OperationDeployParachute(true);
            }
        }

        if(isOnGround){
            pos = transform.position;
            if(pos.x > 5.5f){
                pos.x -= Time.deltaTime;
                transform.position = pos;
            }
            if(pos.x < -5.5f){
                pos.x += Time.deltaTime;
                transform.position = pos;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(!isOnGround){
            if(other.gameObject.tag == "Ground"){isOnGround = true;}
            if(other.gameObject.tag == "Trash"){
                if(other.gameObject.GetComponent<Box>().isOnGround == true)
                    isOnGround = true;
            }

            if(isOnGround){OperationDeployParachute(false);}
        }
        if(other.gameObject.tag == "Player")
        {
            other.transform.GetComponent<PlayerController>().CommandSupplyBoxObtained();
            gameController.spawnControll.CommandToSpawnAWhitePuff(transform.position);
            Destroy(gameObject);
        }
    }

    void OperationDeployParachute(bool option){
        if(option){
            isParachuteDeployed = true;
            myRigidbody2D.mass = 0.1f;
            myRigidbody2D.gravityScale = 0.1f;
        }else{}
            parachute.gameObject.SetActive(option);
    }


    /*
    void OnTriggerEnter2D(Collider2D other){        
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().CommandSupplyBoxObtained();
            gameController.spawnControll.CommandToSpawnAWhitePuff(transform.position);
            Destroy(gameObject);
        }
    }
    */
}
