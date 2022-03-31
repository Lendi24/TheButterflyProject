using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Mirror;
using Mirror.Discovery;

public class NetworkingSelect : MonoBehaviour
{

    Dictionary<long, ServerResponse> newDiscoveredServers = new Dictionary<long, ServerResponse>();
    Dictionary<long, ServerResponse> oldDiscoveredServers = new Dictionary<long, ServerResponse>();
    Vector2 scrollViewPos = Vector2.zero;
    public NetworkDiscovery networkDiscovery;
    float waitTime, timeLeft;

    [SerializeField]
    public ConfigExplorer explorer;

    void OnValidate()
    {
        if (networkDiscovery == null)
        {
            networkDiscovery = GetComponent<NetworkDiscovery>();
        }
    }

    public void ShareConfig()
    {
        NetworkManager.singleton.StartServer();
        networkDiscovery.AdvertiseServer();
    }

    // Start is called before the first frame update
    void Start()
    {
        networkDiscovery.StartDiscovery();
        waitTime = 2.2f;
        timeLeft = 0.5f;
        /*
        if (NetVar.netModeServer)
        {
            newDiscoveredServers.Clear();
            NetworkManager.singleton.StartServer();
            networkDiscovery.AdvertiseServer();
        }

        else
        {
            newDiscoveredServers.Clear();
            networkDiscovery.StartDiscovery();
        }*/
    }

    public void Connect(ServerResponse info)
    {
        NetworkManager.singleton.StartClient(info.uri);
    }

    


    private void Update()
    {
        if (timeLeft < 0)
        {

            if (oldDiscoveredServers.Count != newDiscoveredServers.Count) //This is a bad workaround! Fix a way to compare values, so that some fast dude cannot trick the client
            {
                UpdateList();
            }

            oldDiscoveredServers = new Dictionary<long, ServerResponse>(newDiscoveredServers);
            newDiscoveredServers.Clear();
            timeLeft = waitTime;
        }

        else
        {
            timeLeft = timeLeft - Time.deltaTime;
        }
    }

    private void UpdateList()
    {
        List<ConfigCard> cardsList = new List<ConfigCard>();

        foreach (ServerResponse info in newDiscoveredServers.Values)
        {
            cardsList.Add(new ConfigCard { isLocal=false, name=info.confName, origin=info.uri.ToString(), uri=info.uri});
        }

        explorer.GetRemoteCards(cardsList.ToArray());
    }

    public void OnDiscoveredServer(ServerResponse info)
    {
        // Note that you can check the versioning to decide if you can connect to the server or not using this method
        newDiscoveredServers[info.serverId] = info;
    }
}


