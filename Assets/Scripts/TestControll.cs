using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestControll : MonoBehaviour
{
    public bool isTestingOwerdrive;
    public bool isMissleTestFromTruck, isSupplyPlane, isArmyCar, 
        isFastFire, isTrashCan;
    public GameController gameController;
    
    void Start()
    {
        if(gameController == null) 
        gameController = GameController.controll;
        if(isTestingOwerdrive){
            gameController.isTestingModeOn = true;
            Debug.Log($"Test script is on");
        }        
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)){
            Debug.Log($"Doing test!:");
            Test();
        }
    }


    public void Test(){
        if(isMissleTestFromTruck){
            gameController.spawnControll.CommandToSpanwAMissle(
                true, false, false, Vector3.zero);
        }
        if(isSupplyPlane){
            gameController.spawnControll.CommandSpawnASupplyPlane();
        }
        if(isArmyCar){
            gameController.spawnControll.CommandSpawnAArmyCar();
        }
        if(isFastFire){
            gameController.playerController.fastFireAmooReserve +=50;
            gameController.playerController.isFastFireOn = true;
        }
        if(isTrashCan){
            gameController.spawnControll.CommandToSpawnATrashCan(
                gameController.bossTransform.position
            );
        }
    }
}
