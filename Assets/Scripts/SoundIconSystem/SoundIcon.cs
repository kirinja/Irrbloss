using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundIcon : MonoBehaviour
{
    public Sprite UiSprite;
    public float Scale = 1.0f;
    public Vector2 ScreenPosition;
    public Vector3 WorldPosition;
    public Canvas Canvas;

    public Transform Player;
    public GameObject IconGO;

    // convert from world space to screen space
    // we should probably use the distance in world space to scale the image
    public Vector2 WorldToScreen()
    {
        Vector3 screenPointUnscaled = Camera.main.WorldToScreenPoint(WorldPosition);
        //return new Vector2(screenPointUnscaled.x, screenPointUnscaled.y);
        return new Vector2(screenPointUnscaled.x / Canvas.scaleFactor, screenPointUnscaled.y / Canvas.scaleFactor);
    }

    public float ScaleSprite()
    {
        float distance = Vector3.Distance(Player.position, WorldPosition);
        // we need to normalize this between 0 and 1;
        return distance;
    }


    // Use this for initialization
	void Start ()
	{
        // create the ui object and place under canvas
	    Player = GameObject.FindGameObjectWithTag("Player").transform;
	    IconGO = new GameObject(); //Create the GameObject
	    Image NewImage = IconGO.AddComponent<Image>(); //Add the Image Component script
	    NewImage.sprite = UiSprite; //Set the Sprite of the Image Component on the new GameObject
	    IconGO.GetComponent<RectTransform>().SetParent(Canvas.transform); //Assign the newly created Image GameObject as a Child of the Parent Panel.
        IconGO.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
	    IconGO.SetActive(true); //Activate the GameObject

	   
	    
    }
	
	// Update is called once per frame
	void Update ()
	{
        //WorldPosition = transform.position;
        //ScreenPosition = WorldToScreen();
        //Scale = ScaleSprite();

        //IconGO.GetComponent<RectTransform>().anchoredPosition = ScreenPosition;
        //IconGO.GetComponent<RectTransform>().localScale = new Vector3(Scale, Scale, 1);

        ////this is the ui element
        //RectTransform UI_Element;

        //http://answers.unity3d.com/questions/799616/unity-46-beta-19-how-to-convert-from-world-space-t.html
	    //http://answers.unity3d.com/questions/842616/how-to-place-image-in-world-space-canvas-by-mousec.html
	    //https://www.youtube.com/watch?v=Av8fL2PO2KQ&app=desktop
	    //https://www.google.se/search?q=unity+place+UI+Image+in+world+space&oq=unity+place+UI+Image+in+world+space&aqs=chrome..69i57.14083j0j7&sourceid=chrome&ie=UTF-8
	    //https://gamedev.stackexchange.com/questions/102431/how-to-create-gui-image-with-script

        //first you need the RectTransform component of your canvas
        RectTransform CanvasRect = Canvas.GetComponent<RectTransform>();

	    //then you calculate the position of the UI element
	    //0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.

	    Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(gameObject.transform.position);
	    Vector2 WorldObject_ScreenPosition = new Vector2(
	        ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
	        ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

	    //now you can set the position of the ui element
	    IconGO.GetComponent<RectTransform>().anchoredPosition = WorldObject_ScreenPosition;
    }
}
