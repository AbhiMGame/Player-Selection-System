using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PlayerListManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject playerRowPrefab;
    [SerializeField] private Transform contentParent;
    [SerializeField] private List<PlayerDataScriptableObject> playerDataList;

    private void Start()
    {
        PopulatePlayerList();
    }

    private void PopulatePlayerList()
    {
        // Clear existing content if any
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // Create new rows for each player data
        foreach (var playerData in playerDataList)
        {
            GameObject newRow = Instantiate(playerRowPrefab, contentParent);
            ConfigurePlayerRow(newRow, playerData);
        }
    }

    private void ConfigurePlayerRow(GameObject row, PlayerDataScriptableObject playerData)
    {
        // Get references to UI components in the row prefab
        PlayerRowUI rowUI = row.GetComponent<PlayerRowUI>();

        if (rowUI != null)
        {
            rowUI.SetupRow(playerData);
        }
        else
        {
            Debug.LogError("PlayerRowUI component missing from row prefab!");
        }

        // Add click functionality
        Button button = row.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => OnPlayerSelected(playerData));
        }
    }

    private void OnPlayerSelected(PlayerDataScriptableObject playerData)
    {
        Debug.Log($"Selected player: {playerData.playerName}");
        // Add your selection logic here
    }
}
