using System;
using System.Collections.Generic;
using PlayerSystem;
using UnityEngine;

namespace LobbySystem
{
    [RequireComponent(typeof(LobbyNetwork))]
    public class Lobby : MonoBehaviour
    {
        public Lobby Instance { get; private set; }
        
        private LobbyNetwork LobbyNetwork => GetLobbyNetwork();
        private LobbyNetwork lobbyNetwork;
        
        private void Awake()
        {
            Instance = this;
        }
        
        public Player GetPlayerFromClientId(ulong clientId)
        {
            foreach(var playerData in lobbyNetwork.PlayerList)
            {
                if(playerData.ClientId == clientId)
                {
                    return playerData.Player;
                }
            }
            
            Debug.LogWarning("Player not found.");
            return null;
        }
        private List<Player> GetPlayers()
        {
            List<Player> playerList = new();

            foreach(var playerData in lobbyNetwork.PlayerList)
            {
                playerList.Add(playerData.Player);
            }
            return playerList;
        }

        private LobbyNetwork GetLobbyNetwork()
        {
            return lobbyNetwork ??= GetComponent<LobbyNetwork>();
        }
        
        


    }
}
