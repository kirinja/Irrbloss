using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
// TODO
// change so it checks from the camera to the player (go over this code)
public class WallTransparency : MonoBehaviour
{

    public Transform WatchTarget;
    public LayerMask OccluderMask;
    //This is the material with the Transparent/Diffuse With Shadow shader
    public Material HiderMaterial;

    private Dictionary<Transform, Material> _LastTransforms;
    private Dictionary<Transform, Color> _LastColor;
    private Dictionary<Transform, Texture> _LastTexture;

    void Start()
    {
        _LastTransforms = new Dictionary<Transform, Material>();
        _LastColor = new Dictionary<Transform, Color>();
        _LastTexture = new Dictionary<Transform, Texture>();
    }

    void Update()
    {
        //reset and clear all the previous objects
        if (_LastTransforms.Count > 0)
        {
            foreach (Transform t in _LastTransforms.Keys)
            {
                t.GetComponent<Renderer>().material = _LastTransforms[t];
            }
            _LastTransforms.Clear();
        }

        if (_LastColor.Count > 0)
        {
            foreach (Transform t in _LastColor.Keys)
            {
                t.GetComponent<Renderer>().material.color = _LastColor[t];
            }
            _LastColor.Clear();
        }

        if (_LastTexture.Count > 0)
        {
            foreach (Transform t in _LastTexture.Keys)
            {
                t.GetComponent<Renderer>().material.mainTexture = _LastTexture[t];
            }
            _LastTexture.Clear();
        }

        //Cast a ray from this object's transform the the watch target's transform.
        RaycastHit[] hits = Physics.RaycastAll(
            transform.position,
            WatchTarget.transform.position - transform.position,
            Vector3.Distance(WatchTarget.transform.position, transform.position),
            OccluderMask
        );

        //Loop through all overlapping objects and disable their mesh renderer
        if (hits.Length > 0)
        {
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.transform != WatchTarget && hit.collider.transform.root != WatchTarget)
                {
                    _LastTransforms.Add(hit.collider.gameObject.transform, hit.collider.gameObject.GetComponent<Renderer>().material);
                    _LastColor.Add(hit.collider.gameObject.transform, hit.collider.gameObject.GetComponent<Renderer>().material.color);
                    _LastTexture.Add(hit.collider.gameObject.transform, hit.collider.gameObject.GetComponent<Renderer>().material.mainTexture);
                    
                    Color c = _LastColor[hit.collider.gameObject.transform];

                    hit.collider.gameObject.GetComponent<Renderer>().material = HiderMaterial;
                    hit.collider.gameObject.GetComponent<Renderer>().material.color = new Color(c.r, c.g, c.b, HiderMaterial.color.a);
                    hit.collider.gameObject.GetComponent<Renderer>().material.mainTexture = _LastTexture[hit.collider.gameObject.transform];
                }
            }
        }
    }
}