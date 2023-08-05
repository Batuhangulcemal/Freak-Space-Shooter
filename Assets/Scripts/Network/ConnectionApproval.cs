using Unity.Netcode;
using UnityEngine;

namespace Network
{
    public class ConnectionApproval
    {
        public static void ConnectionApprovalCallback(NetworkManager.ConnectionApprovalRequest request,
            NetworkManager.ConnectionApprovalResponse response)
        {
            Debug.Log("ConnectionApprovalCallback!");
            response.Approved = true;
        }
    }
}