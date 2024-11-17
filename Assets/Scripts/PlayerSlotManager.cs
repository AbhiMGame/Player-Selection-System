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

    [Header("Screens")]
    [SerializeField] private GameObject playerSelectionScreenPanel;  // The panel with the list
    [SerializeField] private GameObject playerScreenPanel;          // The panel with the 5 slots

    [Header("References")]
    [SerializeField] private PlayerListManager playerListManager;

    private void Start()
    {
        // Make sure the player selection screen is visible and player screen is hidden initially
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

    private void SwitchToPlayerScreen()
    {
        Debug.Log("Switching screens");  // Debugging line

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
}