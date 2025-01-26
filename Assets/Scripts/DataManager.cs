using UnityEngine;
using System;
using System.IO;
using NUnit.Framework;
using System.Collections.Generic;

public class DataManager : MonoBehaviour
{
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

    string dataDir = Application.persistentDataPath;
    [SerializeField] string dataFileName = "TimeSaveData";
    public SaveDataSt saveDataSt = new SaveDataSt(1);
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TimeEvents.dataNotif += handleDataNotif;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void handleDataNotif(Level level, float time)
    {
        //check to see if the times are out of bounds
        if(saveDataSt.times.Capacity < 0)
        {
            //add the time to the
            saveDataSt.times.Add(time);
            //saveDataSt.levelIds.Add(level.levelID);

        }
        //level id eventualy will be level id
        if (saveDataSt.times[0] >= time)
        {
            //new time was faster
        }
    }

    void SaveData(SaveDataSt data)
    {
        string fullPath = Path.Combine(dataDir, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToStore = JsonUtility.ToJson(data, true);

            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream)) 
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception e) 
        {
            Debug.LogError("File log error " + e);

        }

    }
    SaveDataSt LoadData()
    {
        string fullPath = Path.Combine(dataDir, dataFileName);
        SaveDataSt data = new SaveDataSt(1);

        if(File.Exists(fullPath)) 
        {
           try
            {
                string dataToLoad = "";

                using(FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                data = JsonUtility.FromJson<SaveDataSt>(dataToLoad);
            }
            catch(Exception e) 
            {
                Debug.LogError("Issue reading from file" + e);
            }
        }

        return data;
    }
}
