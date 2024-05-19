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
    public string mesage="";

    void Start()
    {
        if(gameController == null) gameController = GameController.controll;
        OperationReloadingMesage(true);
    }

    
    void Update()
    {
        
    }


    public void ChangeOnBulletsInChamber(){
        
        //bulletsPart

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


        //mesage part
        mesage = "";
        if(playerController != null){
            if(playerController.isFastFireOn) mesage = "Fast Fire Mod!";
            if(playerController.isReloadingHeavyBullet) mesage = "Reloading!";
            if(playerController.isRevolverSpining) mesage = "Spinning Barrel!";
            if(playerController.isStunned) mesage = "Stuned!";
        }
        if(gameController.isFightOn) mesage = "Figt is not on!";
        reloadingMesageText.text = mesage;

        
    }//ChangeOnBulletsInChamber



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
