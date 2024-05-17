using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float speedMove = 1f, optimalStandPosition=3f;
    public bool isMovingLeft = true, isDeath=false,
        isAproachingBatlefield=false, isFigthing= false, isFalling=false, 
        isEscaping=false, isBreakingApart=false;
    public int hitPointOriginal=100, hitPointCurrent=100, trashToDrop=50, dustDrop=300;

    public bool isUsingSingleBoxDropBattleTechnique;
    public bool isUsingTetrisTechnique;
    public bool isUsingDustingTechnique;
    public bool isDropingBombsTechnique;

    Vector3 pos, dustPos;

    public float dropInterval = 2, explodeInterval=4.5f;
    public float dustIntervalMin= 0.15f, dustIntervalMax = 0.75f;
    public float bombIntervalMin= 0.5f, bombIntervalMax = 5f;
    float dropTimer = 0, dustTimer=0, bombTimer=0;

    float percentualDamageRate=1,  flufTimer=0;

    public Transform trashSpawningPosition;
    public Color[] colorToChose;
    public Color myOwnColor;

    public GameController controll;
    public Rigidbody2D myRigidbody;
    public Collider2D myCollider;
    float myDeltaTime;


    [System.Serializable]
    public struct tetrisBlock
    {
        [SerializeField] public string name;
        [SerializeField] public Vector3[] positions;
    }
    public tetrisBlock[] aviableBlocks;
    int selectedTetrisBlock=0; float maxTetrisBlock=0;
   

    
    void Start()
    {
        if(controll == null) controll = GameController.controll;
        maxTetrisBlock = aviableBlocks.Length;
        bombTimer = Random.Range(bombIntervalMin, bombIntervalMax);
        hitPointOriginal = hitPointCurrent;
        percentualDamageRate = 1;
    }

    
    void Update()
    {
        pos = transform.position;        
        myDeltaTime = Time.deltaTime;

        if(isAproachingBatlefield){
            pos.y -= speedMove * myDeltaTime;
            if(pos.y <= optimalStandPosition){
                isAproachingBatlefield = false;
                controll.CommandToStartFight();
                isFigthing = true;
                if(Random.Range(0,100) > 50){
                    isMovingLeft = false;
                }
            }
        }

        if(isFigthing){
            if(isMovingLeft){
                pos.x -= speedMove * Time.deltaTime;
                if(pos.x < -5){isMovingLeft = false;}
            }else{
                pos.x += speedMove * myDeltaTime;
                if(pos.x > 5){isMovingLeft = true;}
            }

            if(isUsingSingleBoxDropBattleTechnique){
                dropTimer -= myDeltaTime;
                if(dropTimer <= 0){
                    dropTimer = dropInterval;
                    DropTrash();
                }    
            }         

            if(isUsingTetrisTechnique){
                dropTimer -= myDeltaTime;
                if(dropTimer <= 0){                    
                    DropTetrisBlock();
                }   
            }   

            if(isUsingDustingTechnique){
                dustTimer -= myDeltaTime;                
                if(dustTimer <= 0){
                    DropDust();
                }
            }



            if(isDropingBombsTechnique){
                bombTimer -= myDeltaTime;
                if(bombTimer <= 0){
                    controll.spawnControll.CommandToSpawnABomb(trashSpawningPosition.position);
                    bombTimer = Random.Range(bombIntervalMin, bombIntervalMax);
                }
            }

            
        }//is fighting

        


        if(isFalling){
            explodeInterval -= Time.deltaTime;
            if(explodeInterval <= 0){
                isFalling = false;
                CommandToBreak();
            }
        }

        if(isEscaping){
            pos.y += speedMove * Time.deltaTime;
            if(pos.y >= 8){
                controll.CommandBossEscaped(hitPointCurrent);
                isEscaping = false;
            }
        }

        flufTimer += Time.deltaTime;
        if(flufTimer > percentualDamageRate){
            flufTimer = 0;
            SpawnAPuff();
        }

        transform.position = pos;
    }

    void DropTrash(){
        int colorNumber = (int) Random.Range(0f, 10f);
        controll.spawnControll.CommandToSpawnATrashBox(trashSpawningPosition.position, colorToChose[colorNumber]);

        trashToDrop --;
        if(trashToDrop <= 0){
            isFigthing = false;
            isEscaping = true;
        }
    }


    void DropTetrisBlock(){
        List<Transform> boxes = new List<Transform>();
        int colorNumber = (int) Random.Range(0f, 10f);
        Vector3 pos = trashSpawningPosition.position;

        selectedTetrisBlock = (int)Random.Range(-0.4f, maxTetrisBlock -0.6f);
        if(selectedTetrisBlock < 0 || selectedTetrisBlock > maxTetrisBlock -1) selectedTetrisBlock =0;
        for(int i = 0; i < aviableBlocks[selectedTetrisBlock].positions.Length; i++) {
            pos = trashSpawningPosition.position + aviableBlocks[selectedTetrisBlock].positions[i];
            boxes.Add(controll.spawnControll.CommandToSpawnATrashBox(pos, colorToChose[colorNumber]));            
            trashToDrop --;
        }
        for(int i = 0; i < boxes.Count; i++) {
            boxes[i].SetParent(trashSpawningPosition);
        }

        float rotateValue = Random.Range(0f,1f);
        Quaternion angle = new Quaternion(0,0,0,1);
        if(rotateValue > 0.75f) angle = Quaternion.Euler(0,0,0);
        else if(rotateValue > 0.5f) angle = Quaternion.Euler(0,0,90);
        else if(rotateValue > 0.25f) angle = Quaternion.Euler(0,0,180);
        else  angle = Quaternion.Euler(0,0,270);

        trashSpawningPosition.rotation = angle;
        
        for(int i = 0; i < boxes.Count; i++) {
            boxes[i].SetParent(controll.spawnControll.folderForTrash);
        }
        trashSpawningPosition.rotation = new Quaternion(0,0,0,0);
        dropTimer = (dropInterval *(float)boxes.Count) + Random.Range(0f, 1f);
    }


    void DropDust(){
        dustPos = transform.position;
        dustPos.x += Random.Range(-1.5f, 1.5f);
        controll.spawnControll.CommandToSpawnATrashPacklet(
            dustPos, Quaternion.identity, Color.grey);
        dustTimer = Random.Range(dustIntervalMin, dustIntervalMax);
        dustDrop--;
        if(dustDrop <= 0){
            isFigthing = false;
            isEscaping = true;
        }
    }

    void SpawnAPuff(){
        Vector3 pos = transform.position;
        pos.x += Random.Range(-1.5f, 1.5f);
        pos.y += Random.Range(-1.5f, 1.5f);
        bool isblack = false;
        if(Random.Range(0,2f) > 1f){isblack = true;}
        if(isblack)
            controll.spawnControll.CommandToSpawnABlackPuff(pos);
        else controll.spawnControll.CommandToSpawnAWhitePuff(pos);  
    }


    public void GotHit(int damage, Vector3 hitPos){
        hitPointCurrent -= damage;
        if(isFigthing || isEscaping){
            if(hitPointCurrent <= 0){
                isFigthing = false;
                isFalling = true;
                isEscaping = false;
                isDeath = true;
                myRigidbody.bodyType = RigidbodyType2D.Dynamic;
                myCollider.isTrigger = false;
                controll.CommandBossWasShutDown();
            }
        }        

            if(damage > 1){
                controll.spawnControll.CommandToSpawnATrashPacklet(
                    hitPos, Quaternion.identity, myOwnColor);
                controll.spawnControll.CommandToSpawnAExplosion(hitPos);
            }

        percentualDamageRate = (float)hitPointCurrent / (float)hitPointOriginal;
        if(percentualDamageRate < 0.05f) percentualDamageRate = 0.05f;
        
    }


    public void CommandToBreak()
    {        
        Vector3 pos = transform.position;
        bool flip=false;
        int howMutchTrash = (int)Random.Range(15,30);
        if(isBreakingApart == false){
            isBreakingApart = true;
            for(int i=0; i<howMutchTrash; i++){
                if(flip){pos.x += 0.0f;}else{pos.y += 0.2f;}
                flip = !flip;
                controll.spawnControll.CommandToSpawnATrashPacklet(
                    pos, Quaternion.identity, myOwnColor);
            }
            controll.soundController.CommandPlayExplosion();
            controll.spawnControll.CommandToSpawnAGroundExplosion(transform.position);
            for(int i = 0; i < 10; i++) {
                pos = transform.position;
                pos.x += Random.Range(-2f, 2f); 
                pos.y += Random.Range(0.5f, 3f);
                controll.spawnControll.CommandToSpawnABlackPuff(pos);
            }
            for(int i = 0; i < 5; i++) {
                pos = transform.position;
                pos.x += Random.Range(-2f, 2f); 
                pos.y += Random.Range(0.5f, 3f);
                controll.spawnControll.CommandToSpawnAWhitePuff(pos);
            }
            Destroy(gameObject);
        }
    }

}
