using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestControll : MonoBehaviour
{
    public bool isTestingOwerdrive;
    public bool isMissleTestFromTruck;
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
            gameController.spawnControll.CommandToSpanwAMissle(true, false);
        }
    }
}
