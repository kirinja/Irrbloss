using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleSystem : MonoBehaviour
{
    
    // need to poll what stuff should display subtitles somehow
    public int MaxSubtitlesOnScreen = 4;
    private Text _subtitleText;
    
    private List<Subtitle> _subtitles;
    
	// Use this for initialization
	void Start ()
	{
	    _subtitleText = GameObject.Find("Subtitles").GetComponent<Text>(); // UI element
        
        _subtitles = new List<Subtitle>(MaxSubtitlesOnScreen);
	}
	
	// Update is called once per frame
	void Update ()
	{
        foreach (var s in _subtitles)
        {
            s.Update(Time.deltaTime);
            if (!s.ShouldBeRemoved()) continue;

            Remove(s);
            break;
        }

        _subtitleText.text = ""; // clear the text every frame
        // inefficient to update UI every frame when we only need to update on certain events
	    foreach (var s in _subtitles)
	    {
	        var c = ColorUtility.ToHtmlStringRGB(s.Color);
	        _subtitleText.text += "<color=#" + c + ">" + "[" +  s.Text + "] </color>" + "\n";
	    }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyAI>())
        {
            // this should only happen with enemies basically
            var comp = other.GetComponent<SubtitleComponent>();
            AddSubtitle(comp);
        }
    }

    public void AddSubtitle(Subtitle subtitle)
    {
        if (_subtitles.Count >= MaxSubtitlesOnScreen)
            RemoveFirst();
        
        _subtitles.Add(subtitle);
    }

    public void AddSubtitle(SubtitleComponent subComp)
    {
        if (_subtitles.Count >= MaxSubtitlesOnScreen)
            RemoveFirst();
        _subtitles.Add(new Subtitle(subComp.Data.Text, subComp.Data.TimeOnScreen, subComp.Data.Color));
    }

    private void RemoveFirst()
    {
        _subtitles.RemoveAt(0);
    }

    private void Remove(Subtitle subtitle)
    {
        _subtitles.Remove(subtitle);
    }


}
