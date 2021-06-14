using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PropertiesPanel : XRPanel
{
    private GameObject selectedObject;
    public TextMeshProUGUI modelIdText;
    public TextMeshProUGUI modelNameText;
    public TextMeshProUGUI modelPositionText;
    public TextMeshProUGUI modelRotationText;
    public TextMeshProUGUI modelScaleText;
    public TextMeshProUGUI parentText;
    public Button parentButton;
    public Toggle interactableToggle;
    public InputField tagInputField;
    public List<string> tags = new List<string>();

    public override void Show()
    {
        base.Show();
        selectedObject = EditorManager.instance.selectedObject;
        if (selectedObject.GetComponent<Saveable>().data.tag != null)
        {
            tagInputField.GetComponent<InputField>().text = selectedObject.GetComponent<Saveable>().data.tag;
        }
    }

    void Update()
    {
        UpdateSpecificationText(selectedObject);
    }

    public void DestroyButton()
    {
        Destroy(selectedObject);
        selectedObject = null;
    }
    public void DuplicateButton(GameObject cloneObj)
    {
        selectedObject = cloneObj;
        GameObject cloneObject = Instantiate(selectedObject, selectedObject.transform.position + (Vector3.right * 0.4f), selectedObject.transform.rotation);
        cloneObject = null;
    }
    public void CloseButton()
    {
        selectedObject = null;
    }
    public void UpdateSpecificationText(GameObject obj)
    {
        selectedObject = obj;
        if (selectedObject != null)
        {
            modelIdText.text = selectedObject.GetComponent<Saveable>().data.id.ToString();
            modelNameText.GetComponent<TextMeshProUGUI>().text = selectedObject.name;
            string ObjectPosition = selectedObject.transform.position.ToString();
            modelPositionText.GetComponent<TextMeshProUGUI>().text = ObjectPosition;
            string ObjectRotation = selectedObject.transform.rotation.ToString();
            modelRotationText.GetComponent<TextMeshProUGUI>().text = ObjectRotation;
            string ObjectScale = selectedObject.transform.localScale.ToString();
            modelScaleText.GetComponent<TextMeshProUGUI>().text = ObjectScale;

            interactableToggle.isOn = selectedObject.GetComponent<Saveable>().data.isInteractable;
        
           
            //update parent info
            parentText.text = selectedObject.transform.parent ? "" + selectedObject.transform.parent.GetComponent<Saveable>().data.id : "<NONE>";
        }
    }

    public void SetObject(GameObject selectedObj)
    {
        selectedObject = selectedObj;
    }

    public void SaveState()
    {

    }

    public void EnterSetParentMode()
    {
        EditorManager.instance.SetTransformMode(TransformMode.SET_PARENT);
    }

    public void SetInteractable(bool interactable)
    {
        selectedObject.GetComponent<Saveable>().data.isInteractable = interactable;
    }

    public void AddTagToList()
    {
        //A list should be added to Savable data
        if (tags.Contains(tagInputField.GetComponentInChildren<Text>().text))
        {
            Debug.Log("Tag already exist in list");
        }
        else
        {
            tags.Add(tagInputField.GetComponentInChildren<Text>().text);
            EditorManager.instance.selectedObject.GetComponent<Saveable>().data.tag = tagInputField.GetComponentInChildren<Text>().text;
            SetTag(tagInputField.GetComponentInChildren<Text>());
        }
    }

    public void SetTag(Text tag)
    {
        EditorManager.instance.selectedObject.GetComponent<Saveable>().SetTag(tag);
    }
}
