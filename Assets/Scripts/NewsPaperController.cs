using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NewsPaperController : MonoBehaviour
{
    public GameController control;
    
    [Header("Data")]
    public NewsArticle[] articles;

    [Header("Front Newspaper")]
    public Transform frontPageNewspaperPanel;
    public TMP_Text NewsPaperName, newspaperLabel, DateTymeStamp, 
        articleLabel, mainArticle, secondArticle;
    public Image articleImage1, articleImage2,
        secondArticeImage;
    public bool isTextFondCustomized = true;
    public TMP_FontAsset normalizedFont, newsReaderNormalFont, newsReaderItalicFont;



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

    public void ButtonChangeFontOnNewsPaper(){
        isTextFondCustomized = !isTextFondCustomized;
        if(isTextFondCustomized){
            NewsPaperName.font = newsReaderNormalFont;
            newspaperLabel.font = newsReaderItalicFont;
            DateTymeStamp.font = newsReaderItalicFont;
            articleLabel.font = newsReaderNormalFont;
            mainArticle.font = newsReaderNormalFont;
            secondArticle.font = newsReaderNormalFont;
        }else{
            NewsPaperName.font = normalizedFont;
            newspaperLabel.font = normalizedFont;
            DateTymeStamp.font = normalizedFont;
            articleLabel.font = normalizedFont;
            mainArticle.font = normalizedFont;
            secondArticle.font = normalizedFont;
        }
    }
    
}
