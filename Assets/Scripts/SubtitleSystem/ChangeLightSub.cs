using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLightSub : MonoBehaviour {

    private string _subTitleDirectory;
    private string _savePath;

    private const string SubtitleDirectory = "Subtitle";

    public SubtitleComponent.SubData Data;

    public void Awake()
    {
        _subTitleDirectory = Path.Combine(Application.dataPath, SubtitleDirectory);

        if (!Directory.Exists(_subTitleDirectory))
            Directory.CreateDirectory(_subTitleDirectory);

    }

    // Use this for initialization
    void Start()
    {
        LoadData("LightSource");
        Field.text = Data.Text;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadData(string Name)
    {
        _savePath = Path.Combine(_subTitleDirectory, Name + ".json");
        // we're gonna use json to find the file with the correct data and load it into this file
        //Debug.Log(_savePath);
        var stringifiedData = File.ReadAllText(_savePath);
        var data = JsonUtility.FromJson<SubtitleComponent.SubData>(stringifiedData);

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

    public InputField Field;

    public void UpdateText()
    {
        Data.Text = Field.text;
    }
}
