using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public struct Subtitle
{
    public String Text;
    public float TimeRemaning;

    public Subtitle(String text, float time)
    {
        Text = text;
        TimeRemaning = time;
    }

    public void Update(float deltaTime)
    {
        
    }

    public bool ShouldBeRemoved()
    {
        return TimeRemaning <= 0.0f;
    }
}
