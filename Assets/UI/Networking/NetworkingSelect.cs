using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NetVar
{
    public static bool netModeServer;
}

namespace Mirror.Discovery
{

    public class NetworkingSelect : MonoBehaviour
    {

        readonly Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();
        Vector2 scrollViewPos = Vector2.zero;
        public NetworkDiscovery networkDiscovery;

        void OnValidate()
        {
            if (networkDiscovery == null)
            {
                networkDiscovery = GetComponent<NetworkDiscovery>();
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            if (NetVar.netModeServer)
            {
                discoveredServers.Clear();
                NetworkManager.singleton.StartServer();
                networkDiscovery.AdvertiseServer();
            }

            else
            {
                discoveredServers.Clear();
                networkDiscovery.StartDiscovery();
            }
        }

        void Connect(ServerResponse info)
        {
            networkDiscovery.StopDiscovery();
            NetworkManager.singleton.StartClient(info.uri);
        }

        public void OnDiscoveredServer(ServerResponse info)
        {
            // Note that you can check the versioning to decide if you can connect to the server or not using this method
            discoveredServers[info.serverId] = info;
        }

    }
}

