using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRowView : MonoBehaviour
{
    public Image playerImage;
    public Text playerName;
    public Text playerAttributes;
    public Text playerExperience;

    public void UpdatePlayerRow(PlayerDataScriptableObject playerData)
    {
        playerImage.sprite = playerData.playerImage;
        playerName.text = playerData.playerName;
        playerAttributes.text = playerData.playerAttributes.ToString();
        playerExperience.text = playerData.playerExperience;
    }
}
