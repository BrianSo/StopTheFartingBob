using UnityEngine;

[System.Serializable]
public struct CoolDown
{
    public float cooldownValue;
    public float timer;
    public CoolDown(float cooldown){
        cooldownValue = cooldown;
        timer = 0f;
    }
    public void Update(){
        timer -= Time.deltaTime;
    }

    public void Reset(){
        timer = cooldownValue;
    }

    public bool IsReady(){
        return timer <= 0;
    }
}