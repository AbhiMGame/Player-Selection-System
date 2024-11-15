using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedPlayerController : MonoBehaviour
{
    public PlayerListManager playerListManager;
    public SelectedPlayerScreen selectedPlayerScreen;
    void Start()
    {
        selectedPlayerScreen.DisplaySelectedPlayers(playerListManager.selectedPlayers);
    }

}
