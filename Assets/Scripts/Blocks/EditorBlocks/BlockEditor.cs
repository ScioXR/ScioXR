using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockEditor : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public enum BlockGroup
    {
        BLOCK,
        CONDITION,
        VARIABLE
    }
    public BlockGroup blockGroup;
    public string blockType;
    public CodeBoard codePanel;

    private Vector2 deltaDrag;

    public AttachPoint attachPoint;

    public virtual void AttachBlock(GameObject attachObject, AttachPoint attachPoint = null)
    {

    }

    public virtual void DetachBlock(GameObject attachObject)
    {

    }

    public virtual void AttachChildBlock(GameObject attachObject)
    {

    }

    public virtual void DetachChildBlock(GameObject detachObject)
    {

    }

    public virtual void RefreshReferences()
    {

    }

    public virtual void ImportData(BlockData blockData)
    {

    }

    public virtual void ExportData(ref BlockData blockData)
    {
        blockData.blockType = blockType;
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        CalculateOffset(eventData.position);
        transform.SetAsLastSibling();

        if (attachPoint != null)
        {
            attachPoint.GetComponent<BlockEditor>().DetachBlock(gameObject);
            CalculateOffset(eventData.position);
        }
    }

    public void CalculateOffset(Vector3 screenPos)
    {
        Vector2 pointOnCanvas;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(codePanel.GetComponent<RectTransform>(), screenPos, CameraHelper.main, out pointOnCanvas);

        deltaDrag = (Vector2)transform.localPosition - pointOnCanvas;
    }

    public virtual bool IsAttached()
    {
        return false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Vector3 diff = transform.position - codePanel.position;
        //Debug.Log("OnMouseDrag: " + eventData.position + ", " + diff + ", " + (IsInRect(gameObject, codePanel) ? "YES" : "NO"));

        //Debug.Log("Is below: " + (CanBeAttachedBelow(gameObject, codePanel) ? "YES" : "NO"));

        Vector2 pointOnCanvas;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(codePanel.GetComponent<RectTransform>(), eventData.position, CameraHelper.main, out pointOnCanvas);

        //Camera.main.ScreenToWorldPoint(eventData.position);

        transform.localPosition = pointOnCanvas + deltaDrag;

        codePanel.UpdateHighlights(gameObject);

        //outline.SetActive(CanBeAttachedBelow(gameObject, otherBlock) || CanBeAttachedAbove(gameObject, otherBlock));
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (CodeBoard.IsInRect(gameObject, codePanel.GetComponent<RectTransform>()))
        {
            codePanel.DropBlock(this);
            //codePanel.blocks.Add(block);
        }
        else
        {
            //codePanel.blocks.Remove(block);
            Destroy(gameObject);
        }
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Pointer enter: " + gameObject.name, gameObject);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Pointer exit: " + gameObject.name, gameObject);
    }
}
