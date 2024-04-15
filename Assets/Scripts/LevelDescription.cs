using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Assets/Level Description", order = 1)]
public class LevelDescription : ScriptableObject
{
    [TextArea]
    public string descriptionMain, descriptionSkills;
    public GameObject PrefabBoss;
    public Sprite spriteBoss;
}
