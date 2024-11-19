using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class PlayerSlot
{
    public Image playerImage;
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI attributesText;
    public TextMeshProUGUI experienceText;
}

public class PlayerSlotManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private PlayerSlot[] playerSlots = new PlayerSlot[5];
    [SerializeField] private Button saveButton;
    [SerializeField] private Button modifyButton; 

    [Header("Screens")]
    [SerializeField] private GameObject playerSelectionScreenPanel;  
    [SerializeField] private GameObject playerScreenPanel;          

    [Header("References")]
    [SerializeField] private PlayerListManager playerListManager;

    private void Start()
    {
        
        if (playerSelectionScreenPanel != null)
            playerSelectionScreenPanel.SetActive(true);
        if (playerScreenPanel != null)
            playerScreenPanel.SetActive(false);

       
        if (saveButton != null)
        {
            saveButton.onClick.RemoveAllListeners();  
            saveButton.onClick.AddListener(OnSaveButtonClicked);
        }
        else
        {
            Debug.LogError("Save button reference is missing!");
        }

       
        if (modifyButton != null)
        {
            modifyButton.onClick.RemoveAllListeners();  
            modifyButton.onClick.AddListener(OnModifyButtonClicked);
        }
        else
        {
            Debug.LogError("Modify button reference is missing!");
        }
    }

    public void OnSaveButtonClicked()
    {
        Debug.Log("Save button clicked");  

        List<GameObject> selectedPlayers = playerListManager.GetSelectedPlayers();
        Debug.Log($"Selected players count: {selectedPlayers.Count}");

       
        if (selectedPlayers.Count != 5)
        {
            Debug.LogWarning($"Please select exactly 5 players! Currently selected: {selectedPlayers.Count}");
            return;
        }

       
        for (int i = 0; i < selectedPlayers.Count; i++)
        {
            if (i < playerSlots.Length && playerSlots[i] != null)
            {
                GameObject playerRow = selectedPlayers[i];
                PlayerRowUI rowUI = playerRow.GetComponent<PlayerRowUI>();

                if (rowUI != null && rowUI.PlayerData != null)
                {
                    UpdatePlayerSlot(playerSlots[i], rowUI.PlayerData);
                    Debug.Log($"Updated player slot {i} with player: {rowUI.PlayerData.playerName}");  
                }
                else
                {
                    Debug.LogError($"Missing PlayerRowUI or PlayerData on selected player {i}");
                }
            }
            else
            {
                Debug.LogError($"Player slot {i} is not properly set up in the inspector");
            }
        }

        SwitchToPlayerScreen();
    }

    public void OnModifyButtonClicked()
    {
        Debug.Log("Modify button clicked");  

        playerListManager.ClearSelectedPlayers();  

        
        for (int i = 0; i < playerSlots.Length; i++)
        {
            if (playerSlots[i] != null)
            {
                ClearPlayerSlot(playerSlots[i]);
                Debug.Log($"Cleared player slot {i}");  
            }
        }

        
        SwitchToPlayerSelectionScreen();
    }

    private void UpdatePlayerSlot(PlayerSlot slot, PlayerDataScriptableObject playerData)
    {
        if (slot.playerImage != null && playerData.playerImage != null)
        {
            slot.playerImage.sprite = playerData.playerImage;
        }

        if (slot.playerNameText != null)
        {
            slot.playerNameText.text = playerData.playerName;
        }

        if (slot.attributesText != null)
        {
            slot.attributesText.text = playerData.playerAttributes.ToString();
        }

        if (slot.experienceText != null)
        {
            slot.experienceText.text = playerData.playerExperience;
        }
    }

    private void ClearPlayerSlot(PlayerSlot slot)
    {
        if (slot.playerImage != null)
        {
            slot.playerImage.sprite = null;
        }

        if (slot.playerNameText != null)
        {
            slot.playerNameText.text = string.Empty;
        }

        if (slot.attributesText != null)
        {
            slot.attributesText.text = string.Empty;
        }

        if (slot.experienceText != null)
        {
            slot.experienceText.text = string.Empty;
        }
    }

    private void SwitchToPlayerScreen()
    {
     

        if (playerSelectionScreenPanel != null)
        {
            playerSelectionScreenPanel.SetActive(false);
            
        }
        else
        {
            
        }

        if (playerScreenPanel != null)
        {
            playerScreenPanel.SetActive(true);
            
        }
        else
        {
            
        }
    }

    private void SwitchToPlayerSelectionScreen()
    {
        

        if (playerScreenPanel != null)
        {
            playerScreenPanel.SetActive(false);
           
        }
        else
        {
            Debug.LogError("Player Screen Panel reference is missing!");
        }

        if (playerSelectionScreenPanel != null)
        {
            playerSelectionScreenPanel.SetActive(true);
           
        }
        else
        {
            Debug.LogError("Player Selection Screen Panel reference is missing!");
        }
    }
}
