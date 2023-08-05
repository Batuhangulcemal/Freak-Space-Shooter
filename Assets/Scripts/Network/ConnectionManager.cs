using System;
using Network;
using Scene;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine.SceneManagement;

public enum ConnectionType
{
    Null,
    Client,
    Host,
    Server
}
public static class ConnectionManager
{

    private const string LOCAL_ADDRESS = "127.0.0.1";

    public static void Connect(ConnectionType type, string address = LOCAL_ADDRESS, ushort port = 7777)
    {
        if (NetworkManager.Singleton.IsListening)
        {
            NetworkManager.Singleton.Shutdown();
        }
        
        switch (type)
        {
            case ConnectionType.Client:
                ConnectAsClient(address, port);
                break;
            case ConnectionType.Host:
                ConnectAsHost(address, port);
                break;
            case ConnectionType.Server:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
    
    public static void Disconnect()
    {
        NetworkManager.Singleton.Shutdown();

        if (SceneManager.GetActiveScene().name == UnityScene.Game.ToString())
        {
            Loader.Load(UnityScene.Menu);
        }
        
        NetworkManager.Singleton.ConnectionApprovalCallback -= ConnectionApproval.ConnectionApprovalCallback;
        NetworkManager.Singleton.OnClientDisconnectCallback -= OnLocalClientDisconnectCallback;
    }
    
    private static void ConnectAsClient(string address, ushort port)
    {
        NetworkManager.Singleton.OnClientDisconnectCallback += OnLocalClientDisconnectCallback;
        
        SetConnectionData(address, port);
        NetworkManager.Singleton.StartClient();
    }

    private static void ConnectAsHost(string address, ushort port)
    {
        NetworkManager.Singleton.ConnectionApprovalCallback += ConnectionApproval.ConnectionApprovalCallback;
        
        SetConnectionData(address, port);
        NetworkManager.Singleton.StartHost();

        Loader.Load(UnityScene.Game, true);
    }
    
    private static void OnLocalClientDisconnectCallback(ulong clientId)
    {
        if (NetworkManager.Singleton.IsServer) return;
        Disconnect();
    }
    
    private static void SetConnectionData(string address, ushort port)
    {
        NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = address;
        NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Port = port;
    }
}
