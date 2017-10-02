using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleSystem : MonoBehaviour {

    // need a timer so we know how long we should display each subtitle
    // need an array of strings we can display and if we have more than that then remove the oldest one (should be the top one at all times)
    // need an array of timers, one for each subtitle (Make seperate class that holds a time and a string and counts down)

    // need to poll what stuff should display subtitles somehow
    public float SubtitleTimer = 6.0f;
    public int MaxSubtitlesOnScreen = 4;
    private Text _subtitleText;

    private Queue<Subtitle> _subtitles;
    
	// Use this for initialization
	void Start ()
	{
	    _subtitleText = GameObject.Find("Subtitles").GetComponent<Text>(); // UI element

        _subtitles = new Queue<Subtitle>(MaxSubtitlesOnScreen);
	}
	
	// Update is called once per frame
	void Update ()
	{
        foreach (var s in _subtitles)
        {
            // cant use a queue to update the values inside, can only read
            s.Update(Time.deltaTime);
            if (!s.ShouldBeRemoved()) continue;
            _subtitles.Dequeue();
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
            _subtitles.Dequeue();

        _subtitles.Enqueue(subtitle);
    }

    public void AddSubtitle(String text, float time, Color color)
    {
        if (_subtitles.Count >= MaxSubtitlesOnScreen)
            _subtitles.Dequeue();

        _subtitles.Enqueue(new Subtitle(text, SubtitleTimer, color));
    }

    public void AddSubtitle(SubtitleComponent subComp)
    {
        if (_subtitles.Count >= MaxSubtitlesOnScreen)
            _subtitles.Dequeue();
        _subtitles.Enqueue(new Subtitle(subComp.Data.Text, subComp.Data.TimeOnScreen, subComp.Data.Color));
    }

    private void RemoveSubtitle()
    {
        
    }


}
