using System.Threading;
using TMPro;
using UnityEngine;

public class DisplayTime : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TimeEvents.reciveTime += handelReciveTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void handelReciveTime(float time)
    {
        text.text = (Mathf.Round(time * 100f) / 100f).ToString();
        //time.ToString();
    }
    private void OnDestroy()
    {
        TimeEvents.reciveTime -= handelReciveTime;
    }
}
