using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace ViewSystem
{
    public class MainMenuView : View
    {
        [SerializeField] private Button clientButton;
        [SerializeField] private Button hostButton;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            clientButton.onClick.AddListener(() =>
            {
                ConnectionManager.Connect(ConnectionType.Client);
            });
            
            hostButton.onClick.AddListener(() =>
            {
                ConnectionManager.Connect(ConnectionType.Host);
            });
            
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnectCallback;
        }
        
        protected override void OnDisable()
        {
            if (NetworkManager.Singleton != null)
            {
                NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnectCallback;
            }
        }
        
        private void OnClientDisconnectCallback(ulong obj)
        {
            var disconnectReason = NetworkManager.Singleton.DisconnectReason;

            if (string.IsNullOrEmpty(disconnectReason))
            {
                Debug.Log("Couldn't connect!");
            }
            else
            {
                Debug.Log(disconnectReason);
            }
        }
    }

}
