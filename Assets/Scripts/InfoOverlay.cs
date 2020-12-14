using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoOverlay : MonoBehaviour
{
    public Text transformModeText;
    public TextMeshPro transformModeTextMeshPro;
    // Start is called before the first frame update
    void Awake()
    {
        EditorManager.OnTransformModeChanged += UpdateTransformMode;
    }

    private void OnDestroy()
    {
        EditorManager.OnTransformModeChanged -= UpdateTransformMode;
    }

    void UpdateTransformMode(TransformMode newMode)
    {
        if (transformModeText != null)
        {
            transformModeText.text = newMode.ToString();
        }
        
        if (transformModeTextMeshPro != null)
        {
            transformModeTextMeshPro.text = newMode.ToString();
        }
        
    }
}
