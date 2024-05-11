using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
//using Microsoft.Unity.VisualStudio.Editor;
//using UnityEngine.UIElements;

public class NewsPaperController : MonoBehaviour
{
    public GameController control;
    
    [Header("Data")]
    public NewsArticle[] articles;

    [Header("Front Newspaper")]
    public Transform frontPageNewspaperPanel;
    public TMP_Text DateTymeStamp, articleLabel, mainArticle, secondArticle;
    public Image articleImage1, articleImage2,
        secondArticeImage;



    public void OrderActivateFronNewsPaper(bool option, int articleNumber){
        control.soundController.CommandPlayPaper();
        frontPageNewspaperPanel.gameObject.SetActive(option);
        control.isFrontNewspaperActive = option;
        if(option)
        {
            DateTymeStamp.text = System.DateTime.Today.ToString("d");
            articleLabel.text = articles[articleNumber].label;
            mainArticle.text = articles[articleNumber].mainMesage;
            secondArticle.text = articles[articleNumber].secondMesage;
            articleImage1.sprite = articles[articleNumber].articleSprite1;
            articleImage2.sprite = articles[articleNumber].articleSprite2;
            secondArticeImage.sprite = articles[articleNumber].secondArticleSprite;

        }
        else
        {control.uIController.OrderActivateMainMenu(true);}
            control.soundController.CommandPlayPaper();
    }

    
}
