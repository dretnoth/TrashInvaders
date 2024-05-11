using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MagnumRevolverGraphicController : MonoBehaviour
{
    public GameController gameController;
    public Transform chamberSpriteTransform;
    public Transform[] bulletInChamberSpriteTransform;
    public bool isPlayerAlive;
    public int bulletsInChamber=6;
    PlayerController playerController = null;
    public Transform reloadingMesage; public TMP_Text reloadingMesageText;

    void Start()
    {
        if(gameController == null) gameController = GameController.controll;
    }

    
    void Update()
    {
        
    }


    public void ChangeOnBulletsInChamber(){
        if(gameController.isPlayModeOn){
            OperationReloadingMesage(true);
        }else{
            OperationReloadingMesage(false);
        }

        if(playerController == null){
            playerController = gameController.playerTransform.
                GetComponent<PlayerController>();
        }

        if(gameController.playerTransform != null){
            if(bulletsInChamber != playerController.heavyBulletsReserve){
                bulletsInChamber = playerController.heavyBulletsReserve;
                for(int i = 0; i < 6; i++) {
                    if(bulletsInChamber > i){
                       bulletInChamberSpriteTransform[i].gameObject.SetActive(true);
                   }else{
                       bulletInChamberSpriteTransform[i].gameObject.SetActive(false);
                    }
                }
            }
            if(isPlayerAlive == false){
                isPlayerAlive = true;
                OperationSetMagnumVisible(true);
            }
        }else{
            isPlayerAlive = false;
            OperationSetMagnumVisible(false);
        }

        if(gameController.isFightOn == false){
            reloadingMesageText.text = "Get ready!";
        }else{
            if(playerController != null){
                if(playerController.isStunned){
                    reloadingMesageText.text = "Stuned!";
                } else if(playerController.isReloadingHeavyBullet){
                    reloadingMesageText.text = "Reloading!";
                } else if(playerController.isRevolverSpining){
                    reloadingMesageText.text = "Wait!";
                } else if(bulletsInChamber == 6){
                    reloadingMesageText.text = "";
                }
                

                
            }else{
                if(gameController.isFightOn){
                    reloadingMesageText.text = "Hallo?";
                }
            }
        }
        
        
        

       

        

        

        

        //Debug.Log($"");
    }//ChangeOnBulletsInChamber


    /*
    //delete after testing
    public void OrderMagnumRevolver(){
            OperationReloadingMesage(true);
        if(gameController.isFightOn == false){
            reloadingMesageText.text = "Get ready!";
        }

        if(playerController == null) 
            playerController = gameController.playerTransform.GetComponent<PlayerController>();

        if(playerController.isReloadingHeavyBullet){
            reloadingMesageText.text = "Reloading!";
        }
        if(playerController.isRevolverSpining){
            reloadingMesageText.text = "Wait!";
        }
        if(!playerController.isReloadingHeavyBullet && !playerController.isRevolverSpining){
            OperationReloadingMesage(false);
        }
                
    }
    */

    void OperationReloadingMesage(bool option){
        if(option){
            if(!reloadingMesage.gameObject.activeInHierarchy)
                reloadingMesage.gameObject.SetActive(true);
        }else{
            if(reloadingMesage.gameObject.activeInHierarchy)
                reloadingMesage.gameObject.SetActive(false);
        }
    }


    void OperationSetMagnumVisible(bool option){
        if(option){
            chamberSpriteTransform.gameObject.SetActive(true);            
        }else{
            chamberSpriteTransform.gameObject.SetActive(false); 
        }
    }
}
