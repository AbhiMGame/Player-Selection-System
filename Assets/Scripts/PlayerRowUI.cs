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

    private PlayerDataScriptableObject playerData;
    private bool isSelected = false;

    public PlayerDataScriptableObject PlayerData => playerData;


    public void SetupRow(PlayerDataScriptableObject playerData)
    {
        this.playerData = playerData;  // Ensure the field is set

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

    public void SetSelected(bool selected)
    {
        isSelected = selected;
        // Update the visual appearance of the row based on selection state
        GetComponent<Image>().color = selected ? Color.gray : Color.red;
    }
}
