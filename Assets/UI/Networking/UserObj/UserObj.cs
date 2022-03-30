using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Mirror
{
    public class UserObj : NetworkBehaviour
    {
        [SyncVar]
        public ConfigurationSettings configObj;

        public override void OnStartServer()
        {
            base.OnStartServer();
            configObj = CurrentConfig.conf;
        }

        public override void OnStartClient()
        {
            base.OnStartClient();
            CurrentConfig.conf = configObj;
            SceneManager.LoadScene("ButterHunt");
             
        }
    }
}