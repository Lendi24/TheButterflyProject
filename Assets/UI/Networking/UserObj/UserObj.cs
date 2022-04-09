using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Mirror;


public class UserObj : NetworkBehaviour
{
    [SyncVar]
    public ConfigurationSettings configObj;

    public override void OnStartServer()
    {
        base.OnStartServer();
        configObj = CurrentConfig.conf;
        Mirror.Discovery.ButterDiscoveryInfo.serverName = configObj.confName;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        CurrentConfig.conf = configObj;
        NetworkManager.singleton.StopClient();

        switch (NetMode.mode)
        {
            case 3:
                SceneManager.LoadScene("ButterHunt");
                break;

            case 4:
                ConfigurationFunctions.SaveToFile(CurrentConfig.conf, "NetSave");
                break;

            default:
                break;
        }

    }
}
