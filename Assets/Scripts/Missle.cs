using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    public GameController gameController;
    public int damagePower = 10;
    public float speed=3, speedRotation=70;
    public Transform target;
    Vector3 direction; float zAngle; Quaternion desiredRot;

    void Start()
    {
        if(gameController == null) gameController = GameController.controll;
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
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Boss"){
            other.GetComponent<BossController>().GotHit(damagePower, transform.position);
            Destroy(gameObject);
        }
    }
}
