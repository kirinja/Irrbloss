using UnityEngine;
using System.Collections.Generic;
 

public class WallTransparency : MonoBehaviour
{

    public Transform WatchTarget;
    public LayerMask OccluderMask;
    //This is the material with the Transparent/Diffuse With Shadow shader
    public Material HiderMaterial;

    private Dictionary<Transform, Material> _lastTransforms;
    private Dictionary<Transform, Color> _lastColor;
    private Dictionary<Transform, Texture> _lastTexture;

    void Start()
    {
        _lastTransforms = new Dictionary<Transform, Material>();
        _lastColor = new Dictionary<Transform, Color>();
        _lastTexture = new Dictionary<Transform, Texture>();
    }

    void Update()
    {
        //reset and clear all the previous objects
        if (_lastTransforms.Count > 0)
        {
            foreach (var t in _lastTransforms.Keys)
            {
                t.GetComponent<Renderer>().material = _lastTransforms[t];
            }
            _lastTransforms.Clear();
        }

        if (_lastColor.Count > 0)
        {
            foreach (var t in _lastColor.Keys)
            {
                t.GetComponent<Renderer>().material.color = _lastColor[t];
            }
            _lastColor.Clear();
        }

        if (_lastTexture.Count > 0)
        {
            foreach (var t in _lastTexture.Keys)
            {
                t.GetComponent<Renderer>().material.mainTexture = _lastTexture[t];
            }
            _lastTexture.Clear();
        }

        //Cast a ray from this object's transform the the watch target's transform.
        var hits = Physics.RaycastAll(
            transform.position,
            WatchTarget.transform.position - transform.position,
            Vector3.Distance(WatchTarget.transform.position, transform.position),
            OccluderMask
        );

        //Loop through all overlapping objects and disable their mesh renderer
        if (hits.Length <= 0) return;
        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.transform != WatchTarget && hit.collider.transform.root != WatchTarget)
            {
                _lastTransforms.Add(hit.collider.gameObject.transform, hit.collider.gameObject.GetComponent<Renderer>().material);
                _lastColor.Add(hit.collider.gameObject.transform, hit.collider.gameObject.GetComponent<Renderer>().material.color);
                _lastTexture.Add(hit.collider.gameObject.transform, hit.collider.gameObject.GetComponent<Renderer>().material.mainTexture);
                    
                var c = _lastColor[hit.collider.gameObject.transform];

                hit.collider.gameObject.GetComponent<Renderer>().material = HiderMaterial;
                hit.collider.gameObject.GetComponent<Renderer>().material.color = new Color(c.r, c.g, c.b, HiderMaterial.color.a);
                hit.collider.gameObject.GetComponent<Renderer>().material.mainTexture = _lastTexture[hit.collider.gameObject.transform];
            }
        }
    }
}