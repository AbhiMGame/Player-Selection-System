using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListManager : MonoBehaviour
{
    public List<PlayerDataScriptableObject> availablePlayers = new List<PlayerDataScriptableObject>();
    public List<PlayerDataScriptableObject> selectedPlayers = new List<PlayerDataScriptableObject>();
    public void AddPlayerToSelection(PlayerDataScriptableObject player)
    {
        if (selectedPlayers.Count < 5)
        {
            selectedPlayers.Add(player);
        }
    }
    public void RemovePlayerFromSelection(PlayerDataScriptableObject player)
    {
        selectedPlayers.Remove(player);
    }
}
