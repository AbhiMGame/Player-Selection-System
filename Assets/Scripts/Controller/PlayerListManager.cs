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
    private List<GameObject> selectedPlayers = new List<GameObject>();

   
    [SerializeField] private TextMeshProUGUI remainingSelectionsText;

    private List<GameObject> selectedPlayerRows = new List<GameObject>();
    private const int maxSelectedPlayers = 5;

    private void Start()
    {
        ResetPlayerSelections();
        PopulatePlayerList();
        UpdateRemainingSelectionsText();
    }


    private void PopulatePlayerList()
    {
        
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        
        foreach (var playerData in playerDataList)
        {
            GameObject newRow = Instantiate(playerRowPrefab, contentParent);
            ConfigurePlayerRow(newRow, playerData);
        }
    }

    public void ClearSelectedPlayers()
    {
        selectedPlayers.Clear();
        Debug.Log("Selected players list has been cleared.");
    }

    private void ConfigurePlayerRow(GameObject row, PlayerDataScriptableObject playerData)
    {
        PlayerRowUI rowUI = row.GetComponent<PlayerRowUI>();

        if (rowUI != null)
        {
            rowUI.SetupRow(playerData);
            AddClickListener(row, playerData);
        }
        else
        {
            
            ConfigurePlayerRowWithoutUI(row, playerData);
        }

        if (playerData.isSelected)
        {
            selectedPlayerRows.Add(row);
            SetRowSelected(row, true, playerData);
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
            button = row.AddComponent<Button>();
            button.onClick.AddListener(() => TogglePlayerSelection(row, playerData));
        }
        UpdateButtonInteractability(button, playerData.isSelected);
    }

    private void ConfigurePlayerRowWithoutUI(GameObject row, PlayerDataScriptableObject playerData)
    {
       
        Image image = row.GetComponentInChildren<Image>();
        if (image != null)
        {
            image.sprite = playerData.playerImage;
        }

       
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
           
            selectedPlayerRows.Remove(row);
            SetRowSelected(row, false, playerData);

           
            UpdateAllButtonsInteractability();
        }
        else
        {
           
            if (selectedPlayerRows.Count < maxSelectedPlayers)
            {
                selectedPlayerRows.Add(row);
                SetRowSelected(row, true, playerData);

                
                if (selectedPlayerRows.Count >= maxSelectedPlayers)
                {
                    DisableUnselectedButtons();
                }
            }
        }

        UpdateRemainingSelectionsText();
    }

    private void SetRowSelected(GameObject row, bool isSelected, PlayerDataScriptableObject playerData)
    {
        playerData.isSelected = isSelected;

        
        Image image = row.GetComponentInChildren<Image>();
        if (image != null)
        {
            image.color = isSelected ? Color.gray : Color.white;
        }

        TextMeshProUGUI[] textComponents = row.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI text in textComponents)
        {
            if (text != null)
            {
                text.color = isSelected ? Color.gray : Color.white;  
            }
        }
    }

    private void UpdateButtonInteractability(Button button, bool isSelected)
    {
       
        if (selectedPlayerRows.Count >= maxSelectedPlayers)
        {
            button.interactable = isSelected;
        }
        else
        {
            button.interactable = true;
        }
    }

    private void DisableUnselectedButtons()
    {
        foreach (Transform child in contentParent)
        {
            Button button = child.GetComponent<Button>();
            if (button != null && !selectedPlayerRows.Contains(child.gameObject))
            {
                button.interactable = false;
            }
        }
    }

    private void UpdateAllButtonsInteractability()
    {
        foreach (Transform child in contentParent)
        {
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                button.interactable = true;
            }
        }
    }

    private void UpdateRemainingSelectionsText()
    {
        if (remainingSelectionsText != null)
        {
            int remaining = maxSelectedPlayers - selectedPlayerRows.Count;
            remainingSelectionsText.text = $"Player Remaining: {remaining}";
        }
    }

    public List<GameObject> GetSelectedPlayers()
    {
        return new List<GameObject>(selectedPlayerRows);
    }

    private void ResetPlayerSelections()
    {
        foreach (var playerData in playerDataList)
        {
            playerData.isSelected = false;
        }
        selectedPlayerRows.Clear();
    }

}