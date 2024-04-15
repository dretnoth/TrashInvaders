using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedLifeSpawn : MonoBehaviour
{
    public float lifeSpawnInterval = 0.5f;
    float timer=0;
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > lifeSpawnInterval){
            timer = 0;
            this.gameObject.SetActive(false);
        }
    }
}
