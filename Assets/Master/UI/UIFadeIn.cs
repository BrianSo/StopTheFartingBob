using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIFadeIn : MonoBehaviour {

    public Text uiText; 

	void OnEnable(){
        Color c = uiText.color;
        c.a = 0;
        uiText.color = c;
        StartCoroutine(FadeIn());
    }
    IEnumerator FadeIn(){
        Color c = uiText.color;
        while(c.a < 1f){
            uiText.color = c;
            c.a += Time.deltaTime;
            yield return null;
        }
    }
}
