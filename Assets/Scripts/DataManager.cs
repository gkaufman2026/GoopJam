using UnityEngine;
using System;
using System.IO;
using NUnit.Framework;
using System.Collections.Generic;

public struct SaveDataSt
{
    public List<float> times;
    public List<int> levelIds;

    public SaveDataSt(int dummyData)
    {
        times = new List<float>();
        levelIds = new List<int>();

    }
}

public class DataManager : MonoBehaviour
{
 
    string dataDir;
    [SerializeField] string dataFileName = "TimeSaveData";
    public SaveDataSt saveDataSt = new SaveDataSt(1);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TimeEvents.dataNotif += handleDataNotif;
        //TimeEvents.dataRequest += SendData;
        dataDir = Application.persistentDataPath;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void handleDataNotif(Level level, float time)
    {
        Debug.Log("DataObj created " + level.levelNumber + " cap " + saveDataSt.times.Capacity);
        //if the levelNumber is out of bounds it must mean we have a new level
        
        if (saveDataSt.times.Count < level.levelNumber)
        {
            Debug.Log("DataObject adding to data obj");

            //add the time to the
            saveDataSt.times.Add(time);
            saveDataSt.levelIds.Add(level.levelNumber);

            Debug.Log("DataObject after add times " + saveDataSt.times.Capacity + " Level " + saveDataSt.levelIds.Capacity);
        }

        ////level id eventualy will be level id
        if (saveDataSt.times[level.levelNumber - 1] >= time && saveDataSt.levelIds.Contains(level.levelNumber))
        {
            //new time was faster
            saveDataSt.times[level.levelNumber - 1] = time;
        }
        else
        {
            Debug.LogError("Error with the handleDataNotif");
        }

        TimeEvents.dataRequest?.Invoke(this);
    }

    void SaveData(SaveDataSt data)
    {
        string fullPath = Path.Combine(dataDir, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToStore = JsonUtility.ToJson(data, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("File log error " + e);

        }

    }
    SaveDataSt LoadData()
    {
        string fullPath = Path.Combine(dataDir, dataFileName);
        SaveDataSt data = new SaveDataSt(1);

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                data = JsonUtility.FromJson<SaveDataSt>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Issue reading from file" + e);
            }
        }

        return data;
    }

    void SendData(TimeBoard obj)
    {
        obj.dataObject = saveDataSt;
    }
    private void OnDestroy()
    {
        TimeEvents.dataNotif -= handleDataNotif;
        //TimeEvents.dataRequest -= SendData;
    }
}
