using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeBoard : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] List<TextMeshProUGUI> texts;
    public SaveDataSt dataObject;
    void Start()
    {
        //LevelManager.Instance.CompleteLevelEvent.AddListener(RequestLevelData);
        TimeEvents.dataRequest += RequestLevelData;
    }

    void RequestLevelData(DataManager data)
    {
        //TimeEvents.dataRequest?.Invoke(this);
        dataObject = data.saveDataSt;
        UpdateBoard();
    }
    void UpdateBoard()
    {
        Debug.Log("text " + texts.Count);
        if(texts.Count > 0) 
        {
            Debug.Log("Trying to read the data " + dataObject.times.Count);

            for (int i = 0; i < dataObject.times.Count; i++)
            {
                Debug.Log("read");
                texts[i].text = dataObject.times[i].ToString();
            }
        }
        else
        {
            Debug.LogWarning("Scores not logged to board!!");
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        //LevelManager.Instance.CompleteLevelEvent.RemoveListener(RequestLevelData);
        TimeEvents.dataRequest -= RequestLevelData;
    }
}
