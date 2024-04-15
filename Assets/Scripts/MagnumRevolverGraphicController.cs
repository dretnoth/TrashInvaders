using System.Collections;
using System.Collections.Generic;
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
    public Transform reloadingMesage;

    void Start()
    {
        if(gameController == null) gameController = GameController.controll;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameController.isPlayModeOn){
            if(gameController.playerTransform != null){
                if(isPlayerAlive == false){
                    isPlayerAlive = true;
                    OperationSetMagnumVisible(true);
                }
                if(playerController == null) 
                    playerController = gameController.playerTransform.
                        GetComponent<PlayerController>();
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
                if(playerController.isReloadingHeavyBullet){
                    OperationReloadingMesage(true);
                }else{
                    OperationReloadingMesage(false);
                }

            }else{
                if(isPlayerAlive == true){
                    isPlayerAlive = false;
                    OperationSetMagnumVisible(false);
                    OperationReloadingMesage(false);
                }
            }
        }
    }

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
