using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    public GameController gameController;
    public int damagePower = 10;
    public float speed=3, speedRotation=70;
    public float securityTimer = 8f;
    float timerSpawnPuff=0; public float intervalSpawnPuff=0.25f;
    public Transform target;
    Vector3 direction; float zAngle; Quaternion desiredRot;
    public AudioSource missleLaunchAS, rocketEngineAS;
    public Transform ParticleSystemTransform;

    void Start()
    {
        if(gameController == null) gameController = GameController.controll;
        missleLaunchAS.volume = gameController.soundController.sfxVolumeValue;
        rocketEngineAS.volume = gameController.soundController.sfxVolumeValue *0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null){
            if(gameController.bossTransform != null)
                target = gameController.bossTransform ;
        }

        if(target != null){
            direction = target.position - transform.position;
                zAngle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg - 90;
                desiredRot = Quaternion.Euler (0, 0, zAngle); 
			    transform.rotation = Quaternion.RotateTowards (transform.rotation, desiredRot, speedRotation * Time.deltaTime);
        }
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        timerSpawnPuff += Time.deltaTime;
        if(timerSpawnPuff > intervalSpawnPuff){
            timerSpawnPuff = 0;
            gameController.spawnControll.CommandToSpawnABlackPuff(ParticleSystemTransform.position);
        }

        securityTimer -= Time.deltaTime;
        if(securityTimer < 0) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Boss"){
            other.GetComponent<BossController>().GotHit(damagePower, transform.position);
            gameController.soundController.CommandPlayExplosion();
            Destroy(gameObject);
        }
    }

    public void GetTarget(){
        if(gameController == null) {
            //Debug.Log($"Game controller on missle was null");
            gameController = GameController.controll;  
        }
        if(gameController.bossTransform != null){
            target = gameController.bossTransform;
            //Debug.Log($"Misle target input succefull.");
        }
        
    }
}
