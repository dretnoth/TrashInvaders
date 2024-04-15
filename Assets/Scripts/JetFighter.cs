using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class JetFighter : MonoBehaviour
{
    public GameController gameController;
    public float speedMove =3f;
    public float fireInterval = 0.75f;
    float fireTimer = 0;
    Vector3 pos;
    bool isMovingLeft = true;
    public GameObject prefabMissle, prefabBomb;
    public SpriteRenderer spriteRenderer;
    
    void Start()
    {
        if(gameController == null) gameController = GameController.controll;
    }

    
    void Update()
    {
        pos = transform.position;
            if(isMovingLeft){
                pos.x -= speedMove * Time.deltaTime;
                if(pos.x < - 10){
                    EndOfDuty();
                }

            }else{
                pos.x += speedMove * Time.deltaTime;
                if(pos.x > 10){  
                    EndOfDuty();
                }
            }
        transform.position = pos;

        fireTimer += Time.deltaTime;
        if(fireTimer > fireInterval){
            fireTimer = 0;
        }
    }


    void Fire(){
        Quaternion angle;
        if(isMovingLeft){
            angle = Quaternion.Euler(0,0,270);
        }else{angle = Quaternion.Euler(0,0,90);}
        Instantiate(prefabMissle, transform.position, angle);
        Instantiate(prefabBomb, transform.position, Quaternion.identity);
    }

    public void ChangeDirection(){
        if(!isMovingLeft){
            isMovingLeft = true;
            spriteRenderer.flipX = false;
        }else{
            isMovingLeft = false;
            spriteRenderer.flipX = true;
        }
    }


    void EndOfDuty(){
        Destroy(gameObject);
    }
}
