using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRowUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image playerImage;
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI playerAttributes;
    [SerializeField] private TextMeshProUGUI playerExperienceText;

    public void SetupRow(PlayerDataScriptableObject playerData)
    {
        if (playerImage != null)
        {
            playerImage.sprite = playerData.playerImage;
        }

        if (playerNameText != null)
        {
            playerNameText.text = playerData.playerName;
        }

        if (playerAttributes != null)
        {
            playerAttributes.text = playerData.playerAttributes.ToString();
        }

        if (playerExperienceText != null)
        {
            playerExperienceText.text = playerData.playerExperience;
        }
    }
}
