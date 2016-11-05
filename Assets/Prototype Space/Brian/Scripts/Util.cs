using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

public static class Util{

    public static bool IsOwnByLocalPlayer(this NetworkBehaviour nb){
        return GetPlayer(nb) == Player.localPlayer;
    }
    public static Player GetPlayer(this NetworkBehaviour nb){
        NetworkUnit unit = nb.gameObject.GetComponent<NetworkUnit>();
        return unit==null ? null : unit.player;
    }

    public static void Singleton<T>(this T mb,ref T singletion) where T : MonoBehaviour{
        if(singletion == null){
			singletion = mb;
		}else{
			UnityEngine.Object.Destroy(mb);
		}
    }
    public static void RemoveSingleton<T>(this T mb, ref T singletion) where T : MonoBehaviour{
        if(mb == singletion){
            singletion = null;
        }
    }
}