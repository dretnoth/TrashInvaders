using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PoluttionMystControll : MonoBehaviour
{
    public Transform polutionMistPanel;
    public TMP_Text polutionInfoText;
    public Image polutionMistImage;
    public Color polutionMistCollor;
    float timer =0, interval=0.25f;
    public float maxValue =0.75f, currentValue=0, trashRatio=0;
    public int packetsOnField=0, pecketsOneHundretPercentValue=1000,
        polutionInfoLastValue=100, polutionInfoCurrentValue=0;
    public GameController gameController;

    public float mistmoveSpeed=0.1f, mistMoveRange=2f;
    public bool isMovingLeft, isMovingRight;
    Vector3 positionOfMistPanel, originalPositionOfMistPanel;

    
    void Start()
    {
        if(gameController == null) gameController = GameController.controll;
        polutionMistCollor = Color.white;
        ChangeOnPolutionDensity();
        CommandActivatePolutionPanel(false);
        float testValue = Random.Range(0,100);
        if(testValue > 50){
            isMovingRight = true;
        }else{
            isMovingLeft = true;
        }
        originalPositionOfMistPanel = polutionMistPanel.position;
    }

    
    void Update()
    {
        if(gameController.isPlayModeOn){
            timer += Time.deltaTime;
            if(timer > interval){
                timer = 0;
                ChangeOnPolutionDensity();
            }

            positionOfMistPanel = polutionMistPanel.position;
            if(isMovingLeft){
                positionOfMistPanel.x -= mistmoveSpeed * Time.deltaTime;
                if(positionOfMistPanel.x < (originalPositionOfMistPanel.x - mistMoveRange)){
                    isMovingLeft = false;
                    isMovingRight = true;
                }
            }
            if(isMovingRight){
                positionOfMistPanel.x += mistmoveSpeed * Time.deltaTime;
                if(positionOfMistPanel.x > (originalPositionOfMistPanel.x + mistMoveRange)){
                    isMovingLeft = true;
                    isMovingRight = false;
                }
            }
            polutionMistPanel.position = positionOfMistPanel;
        }
    }

    public void CommandActivatePolutionPanel(bool option){
        polutionMistPanel.gameObject.SetActive(option);
    }

    public void ChangeOnPolutionDensity(){
        packetsOnField = 
            gameController.trashPacketDroped 
            + gameController.trashPacketDropedTotal
            - gameController.trashPacketSweeped
            - gameController.trashPacketSweepedTotal;
        
        trashRatio = (float)packetsOnField / pecketsOneHundretPercentValue;
        currentValue = trashRatio;
        if(currentValue < 0) currentValue =0;
        if(currentValue > maxValue) currentValue =maxValue;

        polutionMistCollor.a = currentValue;
        polutionMistImage.color = polutionMistCollor;

        polutionInfoCurrentValue = (int)(trashRatio*100f);

        if(polutionInfoLastValue != polutionInfoCurrentValue){
            polutionInfoLastValue = polutionInfoCurrentValue;
        }
        polutionInfoText.text = polutionInfoCurrentValue.ToString() +"%";
    }   
}
