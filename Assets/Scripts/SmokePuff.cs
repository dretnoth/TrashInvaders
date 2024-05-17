using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokePuff : MonoBehaviour
{
    public SpawnControll spawnControll;
    float lifeTimer=0; 
    public float lifeInterval=1f, lifeIntervalMin=0.5f, lifeIntervalMax=2.5f;
    Vector3 myRotation = new Vector3 (0,0,0);
    public SpriteRenderer spriteRenderer;
    public Sprite[] mySprites;

    void Start()
    {
        if(spriteRenderer == null) 
            spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        Reset();
    }

    
    void Update()
    {
        lifeTimer += Time.deltaTime;
        if(lifeTimer > lifeInterval){
            Reset();
            Deactivate();
        }
    }

    void Reset(){
        lifeTimer = 0;
        lifeInterval = Random.Range(lifeIntervalMin, lifeIntervalMax);
        myRotation.z = Random.Range(0,360);
        this.transform.Rotate(myRotation);
        spriteRenderer.sprite = mySprites[
            (int)(Random.Range(0, mySprites.Length -1))
        ];
    }

    void Deactivate(){
        this.gameObject.SetActive(false);
    }
}
