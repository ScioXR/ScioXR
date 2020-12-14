using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockSpawner : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private BlockEditor movingObject;
    public GameObject blockPrefab;
    public CodeBoard codePanel;

    public void OnBeginDrag(PointerEventData data)
    {
        if (movingObject == null)
        {
            movingObject = Instantiate(blockPrefab, transform.position, transform.rotation, codePanel.gameObject.transform).GetComponent<BlockEditor>();
            if (movingObject is VariableEditor)
            {
                movingObject.GetComponent<VariableEditor>().variableName = GetComponentInChildren<TextMeshProUGUI>().text;
                movingObject.GetComponentInChildren<TextMeshProUGUI>().text = movingObject.GetComponent<VariableEditor>().variableName;
            }
            movingObject.codePanel = codePanel;
            movingObject.OnBeginDrag(data);
        }
    }
    public void OnDrag(PointerEventData data)
    {
        if (movingObject != null)
        {
            movingObject.OnDrag(data);
        }
    }
    public void OnEndDrag(PointerEventData data)
    {
        if (movingObject != null)
        {
            movingObject.OnEndDrag(data);
            movingObject = null;
        }
    }
}
