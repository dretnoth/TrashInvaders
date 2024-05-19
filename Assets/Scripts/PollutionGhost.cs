using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollutionGhost : MonoBehaviour
{
    public SpriteRenderer mySpriteRenderer;
    public float xSpeed=1, ySpeed=1, xDirection=1, yDirection=1;
    public float size=0.5f;
    Vector3 pos = new Vector3(0,0,0);
    //public bool isFacingLeft = false;
    
    void Start()
    {
        size = Random.Range(0.15f, 0.45f);
        mySpriteRenderer.transform.localScale = new Vector3(size, size, 1);
        pos.x = Random.Range(-6f, 6f);
        pos.y = Random.Range(-3f, 3f);
        if(Random.Range(0,10) < 5) {
            xDirection = -1;
            pos.x += xSpeed * Time.deltaTime * xDirection;
        }
        if(Random.Range(0,10) < 5) yDirection = -1;        
    }

    void OperationChangeSpeed(){
        xSpeed = Random.Range(0.25f, 0.75f);
        ySpeed = Random.Range(0.25f, 0.75f);
    }
    
    void Update()
    {
        pos = transform.position;
        pos.x += xSpeed * Time.deltaTime * xDirection;
        pos.y += ySpeed * Time.deltaTime * yDirection;
        if(pos.x > 7) {
            xDirection = -1;
            mySpriteRenderer.flipX = true;
            pos.x = 6.95f;
            OperationChangeSpeed();
        }
        if(pos.x < -7){
            xDirection = 1;
            mySpriteRenderer.flipX = false;
            pos.x = -6.95f;
            OperationChangeSpeed();
        }
        if(pos.y > 6) yDirection = -1;
        if(pos.y < -4) yDirection = 1;
        transform.position = pos;
    }
}
