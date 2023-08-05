using System;
using Unity.Netcode;

namespace PlayerSystem
{
    public struct PlayerData : INetworkSerializable , IEquatable<PlayerData>
    {
        public NetworkObjectReference NetworkObject;
        public ulong ClientId;
        
        public Player Player => GetPlayer();
        private Player GetPlayer()
        {
            return NetworkObject.TryGet(out var networkObject) ? networkObject.GetComponent<Player>() : null;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref NetworkObject);
            serializer.SerializeValue(ref ClientId);;
        }

        public bool Equals(PlayerData other)
        {
            return ClientId == other.ClientId;
        }
    }
}