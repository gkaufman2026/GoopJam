using UnityEngine;

public class DestroySelfInTime : MonoBehaviour
{
    public float objectLifeTime = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("DestroySelf", objectLifeTime);  
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
