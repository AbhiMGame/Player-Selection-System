using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectionController : MonoBehaviour
{
    public PlayerListManager playerListManager;
    public PlayerSelectionMenu playerSelectionMenu;

    private void Start()
    {
        playerSelectionMenu.PopulatePlayerList(playerListManager.availablePlayers);
    }

    public void SelectPlayer(PlayerDataScriptableObject player)
    {
        playerListManager.AddPlayerToSelection(player);
    }
    public void DeselectPlayer(PlayerDataScriptableObject player)
    {
        playerListManager.RemovePlayerFromSelection(player);
    }

    public void ConfirmSelection()
    {

    }

}
