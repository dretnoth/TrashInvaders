using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class AndroidControllerButtons : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameController controller;
    bool isPressed = false;
    public bool isLeftDirection=false, isRightDirection=false,
        isHeavyBulletFire=false, isForTrashCollecting=false;
    PlayerController playerController;
    
    void Start()
    {
        if(controller == null){ controller  = GameController.controll;}
    }

    
    void Update()
    {
        if (isPressed){
            if(playerController == null) {
                playerController = controller.playerTransform.GetComponent<PlayerController>();
            }
            if(playerController != null){
                if(isLeftDirection)
                playerController.ButtonInputHorisontalMovement(false);
                
                if(isRightDirection)
                playerController.ButtonInputHorisontalMovement(true);
                
                if(isHeavyBulletFire)
                playerController.CommandFireSequence();
                
                if(isForTrashCollecting)
                playerController.CommandColectingNearlyTrash();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData){
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData){
        isPressed = false;
    }
}
