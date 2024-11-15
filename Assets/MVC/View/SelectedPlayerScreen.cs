using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedPlayerScreen : MonoBehaviour
{
    public Transform selectedPlayersParent;
    public GameObject selectedPlayerRowPrefab;

    public void DisplaySelectedPlayers(List<PlayerDataScriptableObject> selectedPlayers)
    {
        foreach (var player in selectedPlayers)
        {
            GameObject row = Instantiate(selectedPlayerRowPrefab, selectedPlayersParent);
            row.GetComponent<PlayerRowView>().UpdatePlayerRow(player);
        }
    }
}
