using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundIcon : MonoBehaviour
{
    public Sprite UiSprite;
    //public float Scale = 1.0f;
    //public Vector2 ScreenPosition;
    //public Vector3 WorldPosition;
    public Canvas Canvas;

    private Transform Player;
    private GameObject IconGO;

    public float minDistance = 1.0f;
    public float maxDistance = 10.0f;

    public float minDistanceScale = 1.0f;
    public float maxDistanceScale = 0.1f;

    [Tooltip("If -1 then show forever")]
    public float TimeOnScreen = 3.0f;

    private float _timer;
    private bool _show = true;

    // convert from world space to screen space
    // we should probably use the distance in world space to scale the image
    //public Vector2 WorldToScreen()
    //{
    //    Vector3 screenPointUnscaled = Camera.main.WorldToScreenPoint(WorldPosition);
    //    //return new Vector2(screenPointUnscaled.x, screenPointUnscaled.y);
    //    return new Vector2(screenPointUnscaled.x / Canvas.scaleFactor, screenPointUnscaled.y / Canvas.scaleFactor);
    //}

    //public float GetDistance()
    //{
    //    float distance = Vector3.Distance(Player.position, WorldPosition);
    //    //float distance = (Player.position - WorldPosition).magnitude;
    //    // we need to normalize this between 0 and 1;
    //    return distance;
    //}


    // Use this for initialization
	void Start ()
	{
        // create the ui object and place under canvas
	    Player = GameObject.FindGameObjectWithTag("Player").transform;
	    Canvas = GameObject.Find("UI").GetComponent<Canvas>(); // find the canvas in the scene instead of applying it manually in the editor
	    IconGO = new GameObject("Light Source"); //Create the GameObject
	    Image newImage = IconGO.AddComponent<Image>(); //Add the Image Component script
	    newImage.sprite = UiSprite; //Set the Sprite of the Image Component on the new GameObject
	    IconGO.GetComponent<RectTransform>().SetParent(Canvas.transform); //Assign the newly created Image GameObject as a Child of the Parent Panel.
        IconGO.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
	    IconGO.SetActive(false); // start the UI element as inactive and activate it later
        
    }
	
	// Update is called once per frame
	void Update ()
	{
        //http://answers.unity3d.com/questions/799616/unity-46-beta-19-how-to-convert-from-world-space-t.html
        //http://answers.unity3d.com/questions/842616/how-to-place-image-in-world-space-canvas-by-mousec.html
        //https://www.youtube.com/watch?v=Av8fL2PO2KQ&app=desktop
        //https://www.google.se/search?q=unity+place+UI+Image+in+world+space&oq=unity+place+UI+Image+in+world+space&aqs=chrome..69i57.14083j0j7&sourceid=chrome&ie=UTF-8
        //https://gamedev.stackexchange.com/questions/102431/how-to-create-gui-image-with-script
        //http://answers.unity3d.com/questions/551461/how-to-scale-an-object-between-two-values-over-dis.html

        // Need to make it so this thing activates when we enable the light
        // could just have the component disabled and enable it in the light script?

        // need to take player position into consideration for both scale and correct placement on the screen
        // example: when sound source is behind the player we still want the icon to display, but at the bottom of the screen, indicating it's behind the player
        // it also needs to scale according to distance between icon and player in the interval [0.5, 2.0]
        // we also need to add a little arrow that indicates which direction it's coming from (compare positions in 3d space)
        var distance = (transform.position - Player.position).magnitude;

        //http://answers.unity3d.com/questions/720447/if-game-object-is-in-cameras-field-of-view.html
        // only render the icons when they are on screen, cheap out method of doing it since the real one is buggy
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
	    bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        // we can also make sure this doesnt activate until the subtitle does, and disables itself when the subtitle timer runs out?
        if (distance >= maxDistance || !_show || !onScreen)
	    {
            // disable the icon if out of the given area
            IconGO.SetActive(false);
	    }
	    else if (_show && onScreen)
	    {
            IconGO.SetActive(true);
	        //first you need the RectTransform component of your canvas
	        RectTransform canvasRect = Canvas.GetComponent<RectTransform>();

	        //then you calculate the position of the UI element
	        //0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.

	        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(gameObject.transform.position);

            // here we need to do corret offsets depending on camera rotation and player position
	        Vector2 WorldObject_ScreenPosition = new Vector2(
	            ((ViewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
	            ((ViewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));

            // here we should calculate the correct position depending on where the camera is pointing and where the player is located?;
            // we already have the viewport position saved but we need to take rotation of the camera into consideration

            // this clamps the icons inside the viewport
            var scaler = Canvas.GetComponent<CanvasScaler>();
            var clampX = Mathf.Clamp(WorldObject_ScreenPosition.x, -(scaler.referenceResolution.x / 2f), (scaler.referenceResolution.x / 2f));
	        var clampY = Mathf.Clamp(WorldObject_ScreenPosition.y, -(scaler.referenceResolution.y / 2f), (scaler.referenceResolution.y / 2f));

            
	        //now you can set the position of the ui element
	        //IconGO.GetComponent<RectTransform>().anchoredPosition = WorldObject_ScreenPosition;
	        IconGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(clampX, clampY);

            //var distance = (transform.position - Player.position).magnitude;
            var norm = (distance - minDistance) / (maxDistance - minDistance);
	        norm = Mathf.Clamp01(norm);
            
	        var minScale = Vector3.one * maxDistanceScale;
	        var maxScale = Vector3.one * minDistanceScale;

	        //IconGO.GetComponent<RectTransform>().localScale = new Vector3(Scale, Scale, 1);
	        IconGO.GetComponent<RectTransform>().localScale = Vector3.Lerp(minScale, maxScale, norm);

            // timer for how long the icon should be on screen
	        if (!(TimeOnScreen >= 0)) return;
	        _timer += Time.deltaTime;
	        if (_timer > TimeOnScreen)
	        {
	            _show = false;
	        }
	    }
	}
}
