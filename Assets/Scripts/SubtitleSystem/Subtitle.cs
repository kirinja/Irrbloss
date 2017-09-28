using System;
using System.Timers;
using UnityEngine;

public class Subtitle
{
    public String Text;
    public float TimeRemaning;
    public Color Color;

    public Subtitle(String text, float time, Color color)
    {
        Text = text;
        TimeRemaning = time;
        Color = color;
    }

    public void Update(float deltaTime)
    {
        TimeRemaning -= deltaTime;
    }

    public bool ShouldBeRemoved()
    {
        return TimeRemaning <= 0.0f;
    }
}
