using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject playerRowPrefab;
    [SerializeField] private Transform contentParent;
    [SerializeField] private List<PlayerDataScriptableObject> playerDataList;

    private List<GameObject> selectedPlayerRows = new List<GameObject>();
    private const int maxSelectedPlayers = 5;

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
            AddClickListener(row, playerData);
        }
        else
        {
            // Handle the case where the PlayerRowUI component is missing
            ConfigurePlayerRowWithoutUI(row, playerData);
        }
    }

    private void AddClickListener(GameObject row, PlayerDataScriptableObject playerData)
    {
        Button button = row.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => TogglePlayerSelection(row, playerData));
        }
        else
        {
            // Add a new Button component to the row
            button = row.AddComponent<Button>();
            button.onClick.AddListener(() => TogglePlayerSelection(row, playerData));
        }

        // Set the background color based on the selected state
        SetRowSelected(row, playerData.isSelected, playerData);
    }

    private void ConfigurePlayerRowWithoutUI(GameObject row, PlayerDataScriptableObject playerData)
    {
        // Get the Image component
        Image image = row.GetComponentInChildren<Image>();
        if (image != null)
        {
            image.sprite = playerData.playerImage;
        }

        // Get the TextMeshProUGUI components
        TextMeshProUGUI[] textComponents = row.GetComponentsInChildren<TextMeshProUGUI>();
        if (textComponents.Length >= 2)
        {
            textComponents[0].text = playerData.playerName;
            textComponents[1].text = playerData.playerAttributes.ToString();
            textComponents[2].text = playerData.playerExperience;
        }

        AddClickListener(row, playerData);
    }

    private void TogglePlayerSelection(GameObject row, PlayerDataScriptableObject playerData)
    {
        if (selectedPlayerRows.Contains(row))
        {
            // Deselect the row
            selectedPlayerRows.Remove(row);
            SetRowSelected(row, false, playerData);
        }
        else
        {
            // Select the row
            if (selectedPlayerRows.Count < maxSelectedPlayers)
            {
                selectedPlayerRows.Add(row);
                SetRowSelected(row, true, playerData);
            }
            else
            {
                Debug.Log("Maximum number of players selected.");
            }
        }
    }

    private void SetRowSelected(GameObject row, bool isSelected, PlayerDataScriptableObject playerData)
    {
        playerData.isSelected = isSelected;

        // Get the Image component
        Image image = row.GetComponentInChildren<Image>();
        if (image != null)
        {
            image.color = isSelected ? Color.gray : Color.white;
        }
    }
}