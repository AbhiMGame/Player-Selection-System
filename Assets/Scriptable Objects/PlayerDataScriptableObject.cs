using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerDataSO",menuName ="Player Data SO", order =1)]
public class PlayerDataScriptableObject : ScriptableObject
{
    public Sprite playerImage;  
    public string playerName;  
    public int playerAttributes;  
    public string playerExperience;
}
