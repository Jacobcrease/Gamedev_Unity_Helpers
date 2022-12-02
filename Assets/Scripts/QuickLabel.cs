/*
 * Script to automatically assign the correct components and ensures to set some standard parameters
 * Scales so the label does not look odd within the editor
 * Sets pivot to make it easier to work with
 * Gives helper functions to change the RectTransform/TMP Label, it's better if only one script has access
 */
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class QuickLabel : MonoBehaviour
{
    public GameObject ParentGo { get; private set; }
    
    private protected float LabelWidth;
    private Vector2 _initialPivot;
    
    private TextMeshPro _label;
    private RectTransform _textRectTransform;
    
    private const float InitialScale = 0.2f;
    private const float InitialFontsize = 20f;
    private const float InitialViewHeight = 2.5f;

    
    // This is just to have a easy way to set everything in the edit mode
    // HOW TO USE
    // After applying this script to the component, you need to "reset" the script twice
    private void Reset()
    {
        InitGlobalVars();
        InitRectTransform();
        InitLabel();
    }

    private void Awake()
    {
        InitGlobalVars();
    }

    private void InitGlobalVars()
    {
        _label = GetComponent<TextMeshPro>();
        _textRectTransform = GetComponent<RectTransform>();
        ParentGo = transform.parent.gameObject;
        _initialPivot = new Vector2(0.5f, 0f);
    }

    private void InitLabel()
    {
        string parentGoName = ParentGo.name;
        _label.fontSize = InitialFontsize;
        LabelWidth = parentGoName.Length + 1;
        _label.text = parentGoName;
    }

    private void InitRectTransform()
    {
        _textRectTransform.sizeDelta = new Vector2(LabelWidth, InitialViewHeight);
        _textRectTransform.pivot = _initialPivot;
        SetScale(Vector3.one * InitialScale);
    }

    public void SetPosition(Vector3 position)
    {
        _textRectTransform.position = position;
    }

    public void SetScale(Vector3 localScale)
    {
        _textRectTransform.localScale = localScale;
    }
    
    public void SetRotation(Quaternion rotation)
    {
        _textRectTransform.rotation = rotation;
    }

    public void ShowLabel(bool b)
    {
        _label.enabled = b;
    }
}
