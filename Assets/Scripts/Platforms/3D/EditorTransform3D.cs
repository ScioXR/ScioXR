using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EditorTransform3D : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    //void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    //{
    //    Debug.Log("Down");
    //}

    //void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    //{
    //    Debug.Log("Up");
    //}

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
    }

    public float moveSpeed = 0.003f;
    public float rotateSpeed = 0.1f;
    public float scaleSpeed = 0.01f;
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        switch (EditorManager.mode)
        {
            case TransformMode.MOVE_XY:
                transform.position += CameraHelper.main.transform.right * eventData.delta.x * moveSpeed;
                transform.position += CameraHelper.main.transform.up * eventData.delta.y * moveSpeed;
                break;
            case TransformMode.MOVE_XZ:
                transform.position += CameraHelper.main.transform.right * eventData.delta.x * moveSpeed;
                transform.position += CameraHelper.main.transform.forward * eventData.delta.y * moveSpeed;
                break;
            case TransformMode.ROTATE_XY:
                transform.Rotate(CameraHelper.main.transform.up, eventData.delta.x * rotateSpeed);
                transform.Rotate(CameraHelper.main.transform.right, eventData.delta.y * rotateSpeed);
                break;
            case TransformMode.ROTATE_XZ:
                transform.Rotate(CameraHelper.main.transform.up, eventData.delta.x * rotateSpeed);
                transform.Rotate(CameraHelper.main.transform.forward, eventData.delta.y * rotateSpeed);
                break;
            case TransformMode.SCALE:
                float newScale = 1.0f + (eventData.delta.y * scaleSpeed);
                transform.localScale *= newScale;
                break;
        }
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (EditorManager.mode == TransformMode.PROPERTIES)
        {
            EditorManager.instance.ToggleProperties(gameObject);
        }
        if (EditorManager.mode == TransformMode.SET_PARENT)
        {
            if (EditorManager.instance.selectedObject == gameObject)
            {
                EditorManager.instance.selectedObject.transform.SetParent(null);
            }
            else
            {
                EditorManager.instance.selectedObject.transform.SetParent(transform);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Outline>().enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Outline>().enabled = false;
    }
}
