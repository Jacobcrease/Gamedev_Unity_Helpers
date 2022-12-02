/*
 * Script that ensures that a QuickLabel is always readable above a Mesh
 * Puts label above Game Object
 * Rotates to main camera
 * scales rect transform depending on the distance
 */

using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(QuickLabel))]
public class FloatingLabel : MonoBehaviour
{
    private const float MinScale = 0.05f;
    
    private Bounds _meshBounds;
    private Transform _cameraTransform;

    private QuickLabel _label;
    
    [SerializeField] private float maxRenderDistance = 5.0f;
    [SerializeField] private float distanceToScaleFactor = 0.02f;
    [SerializeField] private float yOffset = 0.05f;

    void Start()
    {
        if (Camera.main == null)
        {
            gameObject.SetActive(false);
            Debug.Log("No Camera: deactivate Floating Labels");
            return;
        }
        
        _cameraTransform = Camera.main.transform;
        _label = GetComponent<QuickLabel>();
        _meshBounds = Helpers.GetMaxBounds(_label.ParentGo);
        _label.SetPosition( new Vector3(_meshBounds.center.x, _meshBounds.center.y + _meshBounds.extents.y + yOffset,
            _meshBounds.center.z));
    }
    
    void Update()
    {
        float distance = Vector3.Distance(transform.position, _cameraTransform.position);
        
        if (distance > maxRenderDistance || !_cameraTransform)
        {
            _label.ShowLabel(false);
            return;
        }

        _label.ShowLabel(true);
        float scale = GetScale(distance);
        _label.SetScale(Vector3.one * scale); 
        _label.SetRotation(Quaternion.LookRotation(transform.position - _cameraTransform.position));

    }

    private float GetScale(float distance)
    {
        
        float calculatedScale = distance * distanceToScaleFactor;
        float scale = Math.Max(MinScale, calculatedScale);

        return scale;
    }
    
}
