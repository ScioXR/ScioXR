using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PropertiesPanel : XRPanel
{
    private GameObject selectedObject;
    public TextMeshProUGUI modelIdText;
    public TextMeshProUGUI modelNameText;
    public TextMeshProUGUI modelPositionText;
    public TextMeshProUGUI modelRotationText;
    public TextMeshProUGUI modelScaleText;

    public Material[] materials;
    private int selectedMaterial;

    public override void Show()
    {
        base.Show();
        selectedObject = EditorManager.instance.selectedObject;
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
            modelIdText.text = selectedObject.GetComponent<Saveable>().id.ToString();
            modelNameText.GetComponent<TextMeshProUGUI>().text = selectedObject.name;
            string ObjectPosition = selectedObject.transform.position.ToString();
            modelPositionText.GetComponent<TextMeshProUGUI>().text = ObjectPosition;
            string ObjectRotation = selectedObject.transform.rotation.ToString();
            modelRotationText.GetComponent<TextMeshProUGUI>().text = ObjectRotation;
            string ObjectScale = selectedObject.transform.localScale.ToString();
            modelScaleText.GetComponent<TextMeshProUGUI>().text = ObjectScale;
        }
    }
    public void CodeButton()
    {
    }
    public void MaterialsButton(int index)
    {

        selectedMaterial = index;
        selectedObject.GetComponent<MeshRenderer>().material = materials[selectedMaterial];
    }
    public void RevertToDeafultMaterial()
    {
        //selectedObject.gameObject.GetComponent<MeshRenderer>().material = EditorTransformXR.originalMaterial;
    }
    public void SetObject(GameObject selectedObj)
    {
        selectedObject = selectedObj;
    }

    public void SaveState()
    {

    }
}
