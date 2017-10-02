using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SubtitleComponent : MonoBehaviour
{
    private string _subTitleDirectory;
    private string _savePath;
    
    private const string SubtitleDirectory = "Subtitle";


    [System.Serializable]
    public struct SubData
    {
        public string Text;
        public Color Color;
        public float TimeOnScreen;
    }

    public SubData Data;


    public void Awake()
    {
        _subTitleDirectory = Path.Combine(Application.dataPath, SubtitleDirectory);

        if (!Directory.Exists(_subTitleDirectory))
            Directory.CreateDirectory(_subTitleDirectory);
        
    }



    public void LoadData(string Name)
    {
        _savePath = Path.Combine(_subTitleDirectory, Name + ".json");
        // we're gonna use json to find the file with the correct data and load it into this file
        //Debug.Log(_savePath);
        var stringifiedData = File.ReadAllText(_savePath);
        var data = JsonUtility.FromJson<SubData>(stringifiedData);

        Data.Color = data.Color;
        Data.Text = data.Text;
        Data.TimeOnScreen = data.TimeOnScreen;
    }

    public void SaveData(string Name)
    {
        _savePath = Path.Combine(_subTitleDirectory, Name + ".json");
        string s = JsonUtility.ToJson(Data);
        File.WriteAllText(_savePath, s);
    }
}
