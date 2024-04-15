using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControll : MonoBehaviour
{
    public float speedOfBullet = 5f;
    public float lifeInterval = 3f;
    float lifeTimer = 3f;
    public int damagePower = 1, damagePowerOriginal=1;

    private void Start() {
        lifeTimer = lifeInterval;
    }
    
    void Update()
    {
        transform.Translate(Vector3.up * speedOfBullet * Time.deltaTime);
        
        lifeTimer -= Time.deltaTime;
        if(lifeTimer < 0){}
    }

    public void Deactivate(){
        lifeTimer = lifeInterval;
        damagePower = damagePowerOriginal;
        this.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other){
        bool isBulletDone = false;
        if(other.tag == "Boss"){
            other.GetComponent<BossController>().GotHit(damagePower, transform.position);
            isBulletDone = true;
        }

        if(other.tag == "Box"){
            Box otherBox = other.GetComponent<Box>();
            if(otherBox.isBox){
                otherBox.GotHip(damagePower);
                isBulletDone = true;
            }
        }


        if(isBulletDone){Deactivate();}
    }
}
