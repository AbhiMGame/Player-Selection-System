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
    [SerializeField] private Button modifyButton; // Reference to the Modify button

    [Header("Screens")]
    [SerializeField] private GameObject playerSelectionScreenPanel;  // The panel with the list
    [SerializeField] private GameObject playerScreenPanel;          // The panel with the 5 slots

    [Header("References")]
    [SerializeField] private PlayerListManager playerListManager;

    private void Start()
    {
        // Ensure the player selection screen is visible and player screen is hidden initially
        if (playerSelectionScreenPanel != null)
            playerSelectionScreenPanel.SetActive(true);
        if (playerScreenPanel != null)
            playerScreenPanel.SetActive(false);

        // Add listener to save button
        if (saveButton != null)
        {
            saveButton.onClick.RemoveAllListeners();  // Clear any existing listeners
            saveButton.onClick.AddListener(OnSaveButtonClicked);
        }
        else
        {
            Debug.LogError("Save button reference is missing!");
        }

        // Add listener to modify button
        if (modifyButton != null)
        {
            modifyButton.onClick.RemoveAllListeners();  // Clear any existing listeners
            modifyButton.onClick.AddListener(OnModifyButtonClicked);
        }
        else
        {
            Debug.LogError("Modify button reference is missing!");
        }
    }

    public void OnSaveButtonClicked()
    {
        Debug.Log("Save button clicked");  // Debugging line

        List<GameObject> selectedPlayers = playerListManager.GetSelectedPlayers();
        Debug.Log($"Selected players count: {selectedPlayers.Count}");  // Debugging line

        // Check if we have the correct number of players
        if (selectedPlayers.Count != 5)
        {
            Debug.LogWarning($"Please select exactly 5 players! Currently selected: {selectedPlayers.Count}");
            return;
        }

        // Populate each slot with player data
        for (int i = 0; i < selectedPlayers.Count; i++)
        {
            if (i < playerSlots.Length && playerSlots[i] != null)
            {
                GameObject playerRow = selectedPlayers[i];
                PlayerRowUI rowUI = playerRow.GetComponent<PlayerRowUI>();

                if (rowUI != null && rowUI.PlayerData != null)
                {
                    UpdatePlayerSlot(playerSlots[i], rowUI.PlayerData);
                    Debug.Log($"Updated player slot {i} with player: {rowUI.PlayerData.playerName}");  // Debugging line
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

        // Switch screens
        SwitchToPlayerScreen();
    }

    public void OnModifyButtonClicked()
    {
        Debug.Log("Modify button clicked");  // Debugging line

        // Clear the current selection
        playerListManager.ClearSelectedPlayers();  // Clear selected players in PlayerListManager

        // Reset player slots
        for (int i = 0; i < playerSlots.Length; i++)
        {
            if (playerSlots[i] != null)
            {
                ClearPlayerSlot(playerSlots[i]);
                Debug.Log($"Cleared player slot {i}");  // Debugging line
            }
        }

        // Switch back to player selection screen
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
        Debug.Log("Switching to Player Screen");  // Debugging line

        if (playerSelectionScreenPanel != null)
        {
            playerSelectionScreenPanel.SetActive(false);
            Debug.Log("Player Selection Screen deactivated");  // Debugging line
        }
        else
        {
            Debug.LogError("Player Selection Screen Panel reference is missing!");
        }

        if (playerScreenPanel != null)
        {
            playerScreenPanel.SetActive(true);
            Debug.Log("Player Screen activated");  // Debugging line
        }
        else
        {
            Debug.LogError("Player Screen Panel reference is missing!");
        }
    }

    private void SwitchToPlayerSelectionScreen()
    {
        Debug.Log("Switching to Player Selection Screen");  // Debugging line

        if (playerScreenPanel != null)
        {
            playerScreenPanel.SetActive(false);
            Debug.Log("Player Screen deactivated");  // Debugging line
        }
        else
        {
            Debug.LogError("Player Screen Panel reference is missing!");
        }

        if (playerSelectionScreenPanel != null)
        {
            playerSelectionScreenPanel.SetActive(true);
            Debug.Log("Player Selection Screen activated");  // Debugging line
        }
        else
        {
            Debug.LogError("Player Selection Screen Panel reference is missing!");
        }
    }
}
