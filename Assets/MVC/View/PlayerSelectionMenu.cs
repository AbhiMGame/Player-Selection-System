using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectionMenu : MonoBehaviour
{
    public GameObject playerRowPrefab;
    public Transform playerListParent;
    public PlayerListManager playerListManager;

    public void PopulatePlayerList(List<PlayerDataScriptableObject> players)
    {
        foreach (var player in players)
        {
            GameObject row = Instantiate(playerRowPrefab, playerListParent);
            row.GetComponent<PlayerRowView>().UpdatePlayerRow(player);
        }
    }
}
