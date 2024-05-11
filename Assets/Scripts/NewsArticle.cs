using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Article", menuName = "Assets/News Article", order = 2)]
public class NewsArticle : ScriptableObject
{
    [TextArea] public string label, mainMesage, secondMesage;
    public Sprite articleSprite1, articleSprite2, secondArticleSprite;
}
