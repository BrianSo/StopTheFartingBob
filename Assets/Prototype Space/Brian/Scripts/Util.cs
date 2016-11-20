using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

public static class Util{


    public static IEnumerator MoveCameraTo(Camera viewCamera, Transform transform, float duration){
		Vector3 startingPoint = viewCamera.transform.position;

		float progress = 0f;
		float speed = 1 / duration;
		while(progress < 1f){
            Vector3 destination = transform.position;
            destination.x = Util.RoundToNearestPixel(destination.x, viewCamera);
            destination.y = 10;
            destination.z = Util.RoundToNearestPixel(destination.z, viewCamera);

			Vector3 newPos = Vector3.Lerp(startingPoint, destination, progress);
			Vector3 roundPos = new Vector3(Util.RoundToNearestPixel(newPos.x, viewCamera), 10, Util.RoundToNearestPixel(newPos.z, viewCamera));
			viewCamera.transform.position = roundPos;
			progress += Time.deltaTime * speed; 
			yield return null;
		}
    }
    public static IEnumerator MoveCameraTo(Camera viewCamera, Vector3 destination, float duration){
		destination.x = Util.RoundToNearestPixel(destination.x, viewCamera);
		destination.y = 10;
		destination.z = Util.RoundToNearestPixel(destination.z, viewCamera);

		Vector3 startingPoint = viewCamera.transform.position;

		float progress = 0f;
		float speed = 1 / duration;
		while(progress < 1f){
			Vector3 newPos = Vector3.Lerp(startingPoint, destination, progress);
			Vector3 roundPos = new Vector3(Util.RoundToNearestPixel(newPos.x, viewCamera), 10, Util.RoundToNearestPixel(newPos.z, viewCamera));
			viewCamera.transform.position = roundPos;
			progress += Time.deltaTime * speed; 
			yield return null;
		}
    }

    public static float RoundToNearestPixel(float unityUnits, Camera viewingCamera)
	{
		float valueInPixels = (Screen.height / (viewingCamera.orthographicSize * 2)) * unityUnits;
		valueInPixels = Mathf.Round(valueInPixels);
		float adjustedUnityUnits = valueInPixels / (Screen.height / (viewingCamera.orthographicSize * 2));
		return adjustedUnityUnits;
	}

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

    private static Vector3 mousePos;
    private static float lastUpdateTime;
    public static Vector3 MousePositionInWorld{
        get{
            if(lastUpdateTime == Time.time){
                return mousePos;
            }
            lastUpdateTime = Time.time;
            var viewCamera = Camera.main;
            mousePos = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, viewCamera.transform.position.y));
            return mousePos;
        }
    }
}