using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine;


class MyNetworkManager : NetworkManager{

    public static new MyNetworkManager singleton{
		get{
			return (MyNetworkManager) NetworkManager.singleton;
		}
	}
    public event DelegateOnStartHost delegateOnStartHost;
    public event DelegateOnStartClient delegateOnStartClient;
    public event DelegateOnStartHost delegateOnStopHost;
    public event DelegateOnStartClient delegateOnStopClient;
    public event DelegateOnServerAddPlayer delegateOnServerAddPlayer;
    public event DelegateOnServerRemovePlayer delegateOnServerRemovePlayer;
    public delegate void DelegateOnStartHost();
    public delegate void DelegateOnStartClient();
    public delegate void DelegateOnStopHost();
    public delegate void DelegateOnStopClient();
    public delegate void DelegateOnServerAddPlayer(GameObject player);
    public delegate void DelegateOnServerRemovePlayer(GameObject player);

    public bool isHost;
    

    //Awake() // don't use this

    void Start(){
        // Stop the farting bob is a 2 player game
        maxConnections = 2;
    }

    public override NetworkClient StartHost(){
        isHost = true;
        return base.StartHost();
    }
    public new NetworkClient StartClient(){
        isHost = false;
        return base.StartClient();
    }
    public override void OnStartHost(){
        if(delegateOnStartHost != null)
            delegateOnStartHost();
    }
    public override void OnStartClient(NetworkClient client){
        if(delegateOnStartClient != null)
            delegateOnStartClient();
    }
    public override void OnStopHost(){
        if(delegateOnStopHost != null)
            delegateOnStartHost();
    }
    public override void OnStopClient(){
        if(delegateOnStopClient != null)
            delegateOnStopClient();
    }



    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
        var gameManager = GameManager.singleton;
        var playerGameObject = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity/*, GetStartPosition()*/) as GameObject;
		
        //delegate
        if(delegateOnServerAddPlayer != null)
            delegateOnServerAddPlayer(playerGameObject);
        //spawning        
        NetworkServer.AddPlayerForConnection(conn, playerGameObject, playerControllerId);
        
        //synchronize
        GameManager.singleton.Synchronize();

	}

    public override void OnServerDisconnect(NetworkConnection conn){
        if(delegateOnServerRemovePlayer != null)
            foreach(var player in conn.playerControllers)
                delegateOnServerRemovePlayer(player.gameObject);
        base.OnServerDisconnect(conn);
    }
}