using UnityEngine.Networking;
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
    public delegate void DelegateOnStartHost();
    public delegate void DelegateOnStartClient();
    public delegate void DelegateOnStopHost();
    public delegate void DelegateOnStopClient();

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
}