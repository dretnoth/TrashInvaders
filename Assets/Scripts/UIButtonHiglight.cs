using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonHiglight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameController controller;
    public SoundController soundController;


    void Start()
    {
        if(controller == null){ controller  = GameController.controll;}
        if(soundController == null)
            {soundController = controller.soundController;}
    }
    
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        soundController.CommandPlayButtonHighlight();        
    }

    
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        
    }

}