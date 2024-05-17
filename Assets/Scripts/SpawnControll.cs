using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControll : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject prefabPlayerTrashTruck;
    public GameObject prefabBullet, prefabHeavyBullet;
    public GameObject prefabBomb;
    public GameObject prefabMissle;
    public GameObject prefabTrashBlock, prefabTrashPacket;
    public GameObject[] prefabBoss;
    public GameObject prefabTrashTruckFella, prefabArmyCar;
    public GameObject prefabSupplyPlane, prefabSupplyBox;
    public GameObject prefabExplosion, prefabGroundExplosion;
    public GameObject prefabBlackPuff, prefabWhitePuff;



    [Header("Pools")]
    public List<Transform> listOfBullets = new List<Transform>();
    public List<Transform> listOfHeavyBullets = new List<Transform>();
    public List<Transform> listOfBombs = new List<Transform>();
    public List<Transform> listOfTrashBoxes = new List<Transform>();
    public List<Transform> listOfTrashPackets = new List<Transform>();
    public List<Transform> listOfExplosions = new List<Transform>();
    public List<Transform> listOfGroundExplosions = new List<Transform>();
    public List<Transform> listOfBlackPuffs = new List<Transform>();
    public List<Transform> listOfWhitePuffs = new List<Transform>();


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

        bulletTransform.transform.rotation = angle;
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

        controller.bulletsHeavyFirred++;
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
            GameObject go = (GameObject)Instantiate(prefabTrashBlock, Vector3.zero, Quaternion.identity);
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
        controller.soundController.CommandPLayPoopSound();
     }


    public void CommandToSpawnAGroundExplosion(Vector3 pos){
        bool isFounded = false;
        int lenghtOfList = listOfGroundExplosions.Count;
        Transform myTransform = null;
        
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
        controller.soundController.CommandPlayExplosion();      
    }


    public void CommandToSpawnABlackPuff(Vector3 pos){
        bool isFounded = false;
        int lenghtOfList = listOfBlackPuffs.Count;
        Transform myTransform = null;
        
        for(int i = 0; i < lenghtOfList; i++) {
            if(!isFounded){
                if(listOfBlackPuffs[i].gameObject.activeInHierarchy == false){
                    myTransform = listOfBlackPuffs[i];
                    isFounded = true;
                }
            }
        }

        if(!isFounded){
            GameObject go = (GameObject)Instantiate(prefabBlackPuff, Vector3.zero, Quaternion.identity);
            myTransform = go.transform;
            listOfBlackPuffs.Add(myTransform);            
            go.transform.SetParent(folderForVisuals);
        }

        myTransform.gameObject.SetActive(true);
        myTransform.transform.position = pos;    
    }


    public void CommandToSpawnAWhitePuff(Vector3 pos){
        bool isFounded = false;
        int lenghtOfList = listOfWhitePuffs.Count;
        Transform myTransform = null;
        
        for(int i = 0; i < lenghtOfList; i++) {
            if(!isFounded){
                if(listOfWhitePuffs[i].gameObject.activeInHierarchy == false){
                    myTransform = listOfWhitePuffs[i];
                    isFounded = true;
                }
            }
        }

        if(!isFounded){
            GameObject go = (GameObject)Instantiate(prefabWhitePuff, Vector3.zero, Quaternion.identity);
            myTransform = go.transform;
            listOfWhitePuffs.Add(myTransform);            
            go.transform.SetParent(folderForVisuals);
        }

        myTransform.gameObject.SetActive(true);
        myTransform.transform.position = pos;    
    }


   


    public void CommandSpawnNewPlayer(){
        GameObject player = (GameObject) Instantiate(prefabPlayerTrashTruck, 
            new Vector3(0,controller.groundSurfaceY,0), Quaternion.identity);
        controller.playerTransform = player.transform;
        if(controller.playerController != null)
            controller.playerController = player.GetComponent<PlayerController>();
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
        Vector3 pos = new Vector3 (-9, controller.groundSurfaceY, 0);
        GameObject go = (GameObject)Instantiate(prefabTrashTruckFella, pos, Quaternion.identity);
        if(isOnRight){
            pos.x = 9;
            go.transform.position = pos;
            go.GetComponent<TrashTruckFella>().ChangeDirection(); 
        }
        controller.trashTruckSweeping +=1;
        controller.isTrashTruckFellaOnField = true;
        controller.soundController.CommandPlayTruckHonk();
    }


    public void CommandSpawnASupplyPlane(){
        if(prefabSupplyPlane != null){
            Vector3 pos = new Vector3 (-9, 5, 0);
            GameObject go = (GameObject)Instantiate(prefabSupplyPlane, pos, Quaternion.identity);
            go.GetComponent<SupplyPlane>().CommandSetFlyDirectionToRight();
        }
    }

    public void CommandSpawnASupplyCrate(Vector3 pos){
        GameObject go = (GameObject)Instantiate(prefabSupplyBox, pos, Quaternion.identity);
    }

    public void CommandSpawnAArmyCar(){
        Vector3 pos = new Vector3 (9, controller.groundSurfaceY, 0);
        GameObject go = (GameObject)Instantiate(prefabArmyCar, pos, Quaternion.identity);
        
    }


    public void CommandToSpanwAMissle(bool isTruck, bool isJetFighter, bool isCar, Vector3 nexpos){
        GameObject go = null;
        Vector3 pos= Vector3.zero;
        Quaternion quaternionRotation = Quaternion.identity;
        if(isTruck){
            pos = controller.playerTransform.position;
        }
        if(isJetFighter){}
        if(isCar){
            pos = nexpos;
        }
        go = (GameObject)Instantiate(prefabMissle, pos, quaternionRotation);
        go.GetComponent<Missle>().GetTarget();
        
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

        for(int i = 0; i < 400; i++) {
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

        for(int i = 0; i < 50; i++) {
            go = (GameObject)Instantiate(prefabBlackPuff, Vector3.zero, Quaternion.identity);
            listOfBlackPuffs.Add(go.transform);
            go.transform.SetParent(folderForVisuals);
            go.SetActive(false);
        }

        for(int i = 0; i < 50; i++) {
            go = (GameObject)Instantiate(prefabWhitePuff, Vector3.zero, Quaternion.identity);
            listOfWhitePuffs.Add(go.transform);
            go.transform.SetParent(folderForVisuals);
            go.SetActive(false);
        }

    }//fill the pool
    
    
    void Update()
    {
        
    }
}
