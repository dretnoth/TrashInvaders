using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControll : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject prefabPlayerTrashTruck;
    public GameObject prefabBullet, prefabHeavyBullet;
    public GameObject prefabBomb;
    public GameObject prefabTrashBlock, prefabTrashPacket;
    public GameObject[] prefabBoss;
    public GameObject prefabTrashTruckFella;
    public GameObject prefabExplosion, prefabGroundExplosion;



    [Header("Pools")]
    public List<Transform> listOfBullets = new List<Transform>();
    public List<Transform> listOfHeavyBullets = new List<Transform>();
    public List<Transform> listOfBombs = new List<Transform>();
    public List<Transform> listOfTrashBoxes = new List<Transform>();
    public List<Transform> listOfTrashPackets = new List<Transform>();
    public List<Transform> listOfExplosions = new List<Transform>();
    public List<Transform> listOfGroundExplosions = new List<Transform>();


    [Header("References")]
    public static SpawnControll spawnControll;
    public GameController controller;
    public Transform folderForBullets;
    public Transform folderForTrash;
    public Transform folderForVisuals;

    [Header("Value")]
    public int detectedSpawns = 0;


    private void Awake() {
        spawnControll = this;
    }

    public void CommandToSpawnABullet(Vector3 pos, Quaternion angle){
        bool isFounded = false;
        int lenghtOfList = listOfBullets.Count;
        Transform bulletTransform = null;
        
        for(int i = 0; i < lenghtOfList; i++) {
            if(!isFounded){
                if(listOfBullets[i].gameObject.activeInHierarchy == false){
                    bulletTransform = listOfBullets[i];
                    isFounded = true;
                }
            }
        }

        if(!isFounded){
            GameObject go = (GameObject)Instantiate(prefabBullet, Vector3.zero, Quaternion.identity);
            bulletTransform = go.transform;
            listOfBullets.Add(bulletTransform);            
            go.transform.SetParent(folderForBullets);
        }

        bulletTransform.gameObject.SetActive(true);
        bulletTransform.transform.position = pos;
        bulletTransform.transform.rotation = angle;

        controller.bulletsFired++;
    }

    public void CommandToSpawnAHeavyBullet(Vector3 pos, Quaternion angle){
        bool isFounded = false;
        int lenghtOfList = listOfHeavyBullets.Count;
        Transform bulletTransform = null;
        
        for(int i = 0; i < lenghtOfList; i++) {
            if(!isFounded){
                if(listOfHeavyBullets[i].gameObject.activeInHierarchy == false){
                    bulletTransform = listOfHeavyBullets[i];
                    isFounded = true;
                }
            }
        }

        if(!isFounded){
            GameObject go = (GameObject)Instantiate(prefabHeavyBullet, Vector3.zero, Quaternion.identity);
            bulletTransform = go.transform;
            listOfHeavyBullets.Add(bulletTransform);            
            go.transform.SetParent(folderForBullets);
        }

        bulletTransform.gameObject.SetActive(true);
        bulletTransform.transform.position = pos;
        bulletTransform.transform.rotation = angle;

        controller.bulletsFired++;
    }


    public void CommandToSpawnABomb(Vector3 pos){
        bool isFounded = false;
        int lenghtOfList = listOfBombs.Count;
        Transform myTransform = null;
        
        for(int i = 0; i < lenghtOfList; i++) {
            if(!isFounded){
                if(listOfBombs[i].gameObject.activeInHierarchy == false){
                    myTransform = listOfBombs[i];
                    isFounded = true;
                }
            }
        }

        if(!isFounded){
            GameObject go = (GameObject)Instantiate(prefabBomb, Vector3.zero, Quaternion.identity);
            myTransform = go.transform;
            listOfBombs.Add(myTransform);            
            go.transform.SetParent(folderForBullets);
        }

        myTransform.gameObject.SetActive(true);
        myTransform.transform.position = pos;
    }


    public Transform CommandToSpawnATrashBox(Vector3 pos, Color color){
        bool isFounded = false;
        int lenghtOfList = listOfTrashBoxes.Count;
        Transform boxTransform = null;
        
        for(int i = 0; i < lenghtOfList; i++) {
            if(!isFounded){
                if(listOfTrashBoxes[i].gameObject.activeInHierarchy == false){
                    boxTransform = listOfTrashBoxes[i];
                    isFounded = true;
                }
            }
        }

        if(!isFounded){
            GameObject go = (GameObject)Instantiate(prefabTrashPacket, Vector3.zero, Quaternion.identity);
            boxTransform = go.transform;
            listOfTrashBoxes.Add(boxTransform);            
            go.transform.SetParent(folderForTrash);
        }

        boxTransform.gameObject.SetActive(true);
        boxTransform.transform.position = pos;
        boxTransform.GetComponent<Box>().SetMe(color);

        controller.OperationAddSpawnedTrashBox();
        return boxTransform;
    }


    public void CommandToSpawnATrashPacklet(Vector3 pos, Quaternion angle, Color color){
        bool isFounded = false;
        int lenghtOfList = listOfTrashPackets.Count;
        Transform packetTransform = null;
        detectedSpawns++;
        
        for(int i = 0; i < lenghtOfList; i++) {
            if(!isFounded){
                if(listOfTrashPackets[i].gameObject.activeInHierarchy == false){
                    packetTransform = listOfTrashPackets[i];
                    isFounded = true;
                }
            }
        }

        if(!isFounded){
            GameObject go = (GameObject)Instantiate(prefabTrashPacket, Vector3.zero, Quaternion.identity);
            packetTransform = go.transform;
            listOfTrashPackets.Add(packetTransform);            
            go.transform.SetParent(folderForTrash);
        }

        packetTransform.gameObject.SetActive(true);
        packetTransform.transform.position = pos;
        packetTransform.transform.rotation = angle;
        packetTransform.GetComponent<Box>().SetMe(color);

        controller.OperationAddSpawnedTrashPacklet();
    }


     public void CommandToSpawnAExplosion(Vector3 pos){
        bool isFounded = false;
        int lenghtOfList = listOfExplosions.Count;
        Transform myTransform = null;
        detectedSpawns++;
        
        for(int i = 0; i < lenghtOfList; i++) {
            if(!isFounded){
                if(listOfExplosions[i].gameObject.activeInHierarchy == false){
                    myTransform = listOfExplosions[i];
                    isFounded = true;
                }
            }
        }

        if(!isFounded){
            GameObject go = (GameObject)Instantiate(prefabExplosion, Vector3.zero, Quaternion.identity);
            myTransform = go.transform;
            listOfExplosions.Add(myTransform);            
            go.transform.SetParent(folderForVisuals);
        }

        myTransform.gameObject.SetActive(true);
        myTransform.transform.position = pos;
        myTransform.transform.rotation = Quaternion.Euler(0,0,Random.Range(0,360));
     }


     public void CommandToSpawnAGroundExplosion(Vector3 pos){
        bool isFounded = false;
        int lenghtOfList = listOfGroundExplosions.Count;
        Transform myTransform = null;
        detectedSpawns++;
        
        for(int i = 0; i < lenghtOfList; i++) {
            if(!isFounded){
                if(listOfGroundExplosions[i].gameObject.activeInHierarchy == false){
                    myTransform = listOfGroundExplosions[i];
                    isFounded = true;
                }
            }
        }

        if(!isFounded){
            GameObject go = (GameObject)Instantiate(prefabGroundExplosion, Vector3.zero, Quaternion.identity);
            myTransform = go.transform;
            listOfGroundExplosions.Add(myTransform);            
            go.transform.SetParent(folderForVisuals);
        }

        myTransform.gameObject.SetActive(true);
        myTransform.transform.position = pos;        
     }


   


    public void CommandSpawnNewPlayer(){
        GameObject player = (GameObject) Instantiate(prefabPlayerTrashTruck, new Vector3(0,-4,0), Quaternion.identity);
        controller.playerTransform = player.transform;
    }


    //obsoled?
    public void CommandSpawnNewBoss(int option){
        GameObject boss = (GameObject) Instantiate(prefabBoss[option], new Vector3(0,8,0), Quaternion.identity);
        controller.bossTransform = boss.transform;
        boss.GetComponent<BossController>().isAproachingBatlefield = true;
        controller.bossController = boss.GetComponent<BossController>();
    }

    public void CommandSpawnABossFromLevelDescription(){
        GameObject boss = (GameObject) Instantiate(
            controller.myLevels[controller.levelCurrent].PrefabBoss,
             new Vector3(0,8,0), Quaternion.identity);
        controller.bossTransform = boss.transform;
        controller.bossController = boss.GetComponent<BossController>();
        controller.bossController.isAproachingBatlefield = true;
    }


    public void CommandSpawnATrashTruckFella(){
        bool isOnRight = false;
        if(Random.Range(0,100) > 50) isOnRight = true;
        Vector3 pos = new Vector3 (-9, -4f, 0);
        GameObject go = (GameObject)Instantiate(prefabTrashTruckFella, pos, Quaternion.identity);
        if(isOnRight){
            pos.x = 9;
            go.transform.position = pos;
            go.GetComponent<TrashTruckFella>().ChangeDirection(); 
        }
        controller.trashTruckSweeping +=1;
        controller.isTrashTruckFellaOnField = true;
    }


   



    public void CommandFillThePoll(){
        GameObject go = null;
        Transform bulletTransform = null;
        
        for(int i = 0; i < 100; i++) {
            go = (GameObject)Instantiate(prefabBullet, Vector3.zero, Quaternion.identity);
            bulletTransform = go.transform;
            listOfBullets.Add(bulletTransform);
            go.transform.SetParent(folderForBullets);
            go.SetActive(false);
        }

        for(int i = 0; i < 10; i++) {
            go = (GameObject)Instantiate(prefabHeavyBullet, Vector3.zero, Quaternion.identity);
            bulletTransform = go.transform;
            listOfHeavyBullets.Add(bulletTransform);
            go.transform.SetParent(folderForBullets);
            go.SetActive(false);
        }

        for(int i = 0; i < 10; i++) {
            go = (GameObject)Instantiate(prefabBomb, Vector3.zero, Quaternion.identity);
            bulletTransform = go.transform;
            listOfBombs.Add(bulletTransform);
            go.transform.SetParent(folderForBullets);
            go.SetActive(false);
        }
        
        for(int i = 0; i < 100; i++) {
            go = (GameObject)Instantiate(prefabTrashBlock, Vector3.zero, Quaternion.identity);
            listOfTrashBoxes.Add(go.transform);
            go.transform.SetParent(folderForTrash);
            go.SetActive(false);
        }

        for(int i = 0; i < 300; i++) {
            go = (GameObject)Instantiate(prefabTrashPacket, Vector3.zero, Quaternion.identity);
            listOfTrashPackets.Add(go.transform);
            go.transform.SetParent(folderForTrash);
            go.SetActive(false);
        }

        for(int i = 0; i < 50; i++) {
            go = (GameObject)Instantiate(prefabExplosion, Vector3.zero, Quaternion.identity);
            listOfExplosions.Add(go.transform);
            go.transform.SetParent(folderForVisuals);
            go.SetActive(false);
        }

        for(int i = 0; i < 10; i++) {
            go = (GameObject)Instantiate(prefabGroundExplosion, Vector3.zero, Quaternion.identity);
            listOfGroundExplosions.Add(go.transform);
            go.transform.SetParent(folderForVisuals);
            go.SetActive(false);
        }

    }//fill the pool
    
    
    void Update()
    {
        
    }
}
