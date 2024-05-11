using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameController gameController;
    public bool isSpacePressed, isEnterPressed, isEscapePressed;

    void Start()
    {
        
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            if(!isSpacePressed) KeyToContinueIsDown();
            isSpacePressed = true;
        }
        if(Input.GetKeyUp(KeyCode.Space)) { isSpacePressed = false;}
        
        if(Input.GetKeyDown(KeyCode.Return)) {
            if(!isSpacePressed) KeyToContinueIsDown();
            isEnterPressed = true;
        }
        if(Input.GetKeyUp(KeyCode.Return)) {isEnterPressed = false;}
        
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(!isEscapePressed) KeyToBackIsDown();
            isEscapePressed = true;
        }
        if(Input.GetKeyUp(KeyCode.Escape)) {isEscapePressed = false;}
    }

    void KeyToContinueIsDown(){
        bool isDone =false;
        if(!isDone) if(gameController.isFrontNewspaperActive){
            isDone =true;
            gameController.uIController.ButtonFrontNewsPaperContinue();
        }

        if(!isDone) if(gameController.isLevelDescriptionActive){
            isDone =true;
            gameController.uIController.ButtonLaunchLevel();
        }

        if(!isDone) if(gameController.isLevelResultActive){
            isDone =true;
            gameController.uIController.ButtonToNextWorkDay();
        }
    }
    void KeyToBackIsDown(){}
}
