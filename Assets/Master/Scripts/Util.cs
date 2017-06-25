using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using CnControls;

public static class Util{


    /// <summary>
	///  Return a normal distribution random double value. Mean will be 0 and standard deviation will be 1.
	/// </summary>
	/// <returns>The random gaussian double.</returns>
	public static double NextGaussianDouble(this System.Random r)
	{
		double u, v, S;

		do
		{
			u = 2.0 * r.NextDouble() - 1.0;
			v = 2.0 * r.NextDouble() - 1.0;
			S = u * u + v * v;
		}
		while (S >= 1.0);

		double fac = Math.Sqrt(-2.0 * Math.Log(S) / S);
		return u * fac;
	}
    /// <summary>
	/// This method made public for unit testing. You normally would not using this method.
	/// </summary>
	public static double _projectGaussianRange(double v, double min, double max, double sd){
		if (v > sd) {
			v = sd;
		} else if (v < -sd) {
			v = -sd;
		}
		v = v/sd/2 + 0.5; // scale v to 0 to 1
		return v * (max - min) + min;
	}
	/// <summary>
	///  Return a normal distribution random double value within the range given
	///  The generation will first make a gaussian double bounded to the given standard deviation, and then projected to the given range.
	///  So that the Mean of the result will be (max + min)/2
	/// </summary>
	/// <returns>The gaussian range.</returns>
	/// <param name="min">Minimum.</param>
	/// <param name="max">Maximun.</param>
	/// <param name="sd">The standard deviation threshold.</param>
	public static double NextGaussianRange(this System.Random r, double min, double max, double sd = 3)
	{
		double v = r.NextGaussianDouble ();
		return _projectGaussianRange (v,min, max, sd);
	}
    /// <summary>
	/// Return a normal distribution random int value within the range given
	/// </summary>
	/// <returns>The int gaussian range.</returns>
	/// <param name="min">Minimum.</param>
	/// <param name="max">Maximun.</param>
	/// <param name="sd">The standard deviation threshold.</param>
	public static int NextIntGaussianRange(this System.Random r, int min, double max, double sd = 3)
	{
		return (int)Math.Round(r.NextGaussianRange (min, max, sd),MidpointRounding.AwayFromZero);
	}

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
	private static Vector2 lastFacing;
    public static Vector3 MousePositionInWorld{
        get{
            if(lastUpdateTime == Time.time){
                return mousePos;
            }
            lastUpdateTime = Time.time;
            var viewCamera = Camera.main;
#if UNITY_ANDROID || UNITY_IOS
			if (BaseCharactor.localCharactor != null){
				var x = CnInputManager.GetAxisRaw("FacingHorizontal");
				var y = CnInputManager.GetAxisRaw("FacingVertical");
				if(Mathf.Abs(x) > 0.01f || Mathf.Abs(y) > 0.01f){
					lastFacing = new Vector2(x,y);
				}
				mousePos = BaseCharactor.localCharactor.transform.position + new Vector3(lastFacing.x, 0, lastFacing.y) * 2;
			}else{
				mousePos = Vector3.zero;
			}

#else
            mousePos = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, viewCamera.transform.position.y));
#endif
            return mousePos;
        }
    }

	public static void updateMousePositionByTouch(Touch touch){
		lastUpdateTime = Time.time;
		mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
	}
}