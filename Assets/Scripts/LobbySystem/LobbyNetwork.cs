using PlayerSystem;
using Unity.Netcode;
using UnityEngine;

namespace LobbySystem
{
    public class LobbyNetwork : NetworkBehaviour
    {
        internal NetworkList<PlayerData> PlayerList;
        [SerializeField] private NetworkObject playerPrefab;
        
        private void Awake()
        {
            PlayerList = new NetworkList<PlayerData>();
        }
        
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if (IsServer)
            {
                NetworkManager.Singleton.OnClientConnectedCallback += NetworkManager_OnClientConnectedCallback;
                NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;

                if (IsHost)
                {
                    SpawnPlayerObject(NetworkManager.Singleton.LocalClientId);
                }
            }
        }
        
        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
            
            if (IsServer)
            {
                NetworkManager.Singleton.OnClientConnectedCallback -= NetworkManager_OnClientConnectedCallback;
                NetworkManager.Singleton.OnClientDisconnectCallback -= NetworkManager_OnClientDisconnectCallback;
            }
        }

        private void NetworkManager_OnClientConnectedCallback(ulong clientId)
        {
            if (clientId == NetworkManager.LocalClientId) return; 
            SpawnPlayerObject(clientId);
        }

        private void NetworkManager_OnClientDisconnectCallback(ulong clientId)
        {
            RemovePlayerObject(clientId);
        }



        private void SpawnPlayerObject(ulong clientId)
        {
            NetworkObject playerNetworkObject = Instantiate(playerPrefab);
            playerNetworkObject.SpawnAsPlayerObject(clientId, true);
            PlayerList.Add(new PlayerData
            {
                NetworkObject = playerNetworkObject,
                ClientId = clientId
            });
        }

        private void RemovePlayerObject(ulong clientId)
        {
            for (var i = 0; i < PlayerList.Count; i++)
            {
                PlayerData playerData = PlayerList[i];
                if (playerData.ClientId == clientId)
                {
                    //disconnected
                    PlayerList.RemoveAt(i);
                }
            }
        }
    }
}