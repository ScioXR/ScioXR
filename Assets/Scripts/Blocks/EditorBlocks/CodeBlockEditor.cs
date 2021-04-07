using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CodeBlockEditor : BlockEditor
{
    public AttachPoint previousAttachPoint;
    public AttachPoint nextAttachPoint;

    public CodeBlockEditor previousBlock;
    public CodeBlockEditor nextBlock;

    public VariableAttachPoint variableAttachPoint;
    public VariableEditor variableReference;
    public GameObject variableEditText;

    public TMP_Dropdown objectReferenceDropDown;
    public TMP_Dropdown variableReferenceDropDown;
    public TMP_Dropdown messageReferenceDropDown;
    public TMP_Dropdown selectionDropDown;

    private void Start()
    {
        //RefreshReferences();
        if (objectReferenceDropDown && objectReferenceDropDown.options.Count == 0)
        {
            Saveable[] allObjects = GameObject.FindObjectsOfType<Saveable>();
            objectReferenceDropDown.options.Clear();
            foreach (Saveable saveable in allObjects)
            {
                objectReferenceDropDown.options.Add(new TMP_Dropdown.OptionData() { text = saveable.data.id.ToString() });
            }
        }
        if (variableReferenceDropDown && messageReferenceDropDown.options.Count == 0)
        {
            variableReferenceDropDown.AddOptions(codePanel.blockBoard.GetVariables());
        }
        if (messageReferenceDropDown && messageReferenceDropDown.options.Count == 0)
        {
            messageReferenceDropDown.AddOptions(codePanel.blockBoard.GetMessages());
        }
    }

    public override void RefreshReferences()
    {
        if (objectReferenceDropDown)
        {
            if (objectReferenceDropDown.options.Count > 0)
            {
                string selectedText = objectReferenceDropDown.options[objectReferenceDropDown.value].text;

                Saveable[] allObjects = GameObject.FindObjectsOfType<Saveable>();
                objectReferenceDropDown.options.Clear();
                foreach (Saveable saveable in allObjects)
                {
                    objectReferenceDropDown.options.Add(new TMP_Dropdown.OptionData() { text = saveable.data.id.ToString() });
                }

                TMP_Dropdown.OptionData selected = objectReferenceDropDown.options.Find(it => it.text == selectedText);
                objectReferenceDropDown.value = objectReferenceDropDown.options.IndexOf(selected);
            } else
            {
                Saveable[] allObjects = GameObject.FindObjectsOfType<Saveable>();
                objectReferenceDropDown.options.Clear();
                foreach (Saveable saveable in allObjects)
                {
                    objectReferenceDropDown.options.Add(new TMP_Dropdown.OptionData() { text = saveable.data.id.ToString() });
                }
            }
        }
        if (variableReferenceDropDown)
        {
            if (variableReferenceDropDown.options.Count > 0)
            {
                string selectedText = variableReferenceDropDown.options[variableReferenceDropDown.value].text;

                variableReferenceDropDown.ClearOptions();
                variableReferenceDropDown.AddOptions(codePanel.blockBoard.GetVariables());

                TMP_Dropdown.OptionData selected = variableReferenceDropDown.options.Find(it => it.text == selectedText);
                variableReferenceDropDown.value = variableReferenceDropDown.options.IndexOf(selected);
            }
            else
            {
                variableReferenceDropDown.AddOptions(codePanel.blockBoard.GetVariables());
            }
        }
        if (messageReferenceDropDown)
        {
            if (messageReferenceDropDown.options.Count > 0) {
                string selectedText = messageReferenceDropDown.options[messageReferenceDropDown.value].text;

                messageReferenceDropDown.ClearOptions();
                messageReferenceDropDown.AddOptions(codePanel.blockBoard.GetMessages());

                TMP_Dropdown.OptionData selected = messageReferenceDropDown.options.Find(it => it.text == selectedText);
                messageReferenceDropDown.value = messageReferenceDropDown.options.IndexOf(selected);
            } else
            {
                messageReferenceDropDown.AddOptions(codePanel.blockBoard.GetMessages());
            }            
        }
    }

    public override void ImportData(BlockData blockData)
    {
        base.ImportData(blockData);

        RefreshReferences();

        if (objectReferenceDropDown)
        {
            TMP_Dropdown.OptionData selected = objectReferenceDropDown.options.Find(it => it.text == "" + blockData.objectReference);
            objectReferenceDropDown.value = objectReferenceDropDown.options.IndexOf(selected);
        }
        if (variableAttachPoint)
        {
            if (blockData.paramString != null && blockData.paramString != "")
            {
                variableReference.GetComponentInChildren<TextMeshProUGUI>().text = blockData.paramString;
            } else
            {
                variableEditText.GetComponent<TMP_InputField>().text = "" + blockData.paramInt;
            }
        }
        if (messageReferenceDropDown)
        {
            TMP_Dropdown.OptionData selected = messageReferenceDropDown.options.Find(it => it.text == blockData.paramString);
            messageReferenceDropDown.value = messageReferenceDropDown.options.IndexOf(selected);
        }
        if (selectionDropDown)
        {
            selectionDropDown.value = blockData.paramInt;
        }
    }

    public override void ExportData(ref BlockData blockData)
    {
        base.ExportData(ref blockData);
        if (objectReferenceDropDown)
        {
            blockData.objectReference = int.Parse(objectReferenceDropDown.options[objectReferenceDropDown.value].text);
        }
        if (variableAttachPoint)
        {
            if (variableReference)
            {
                blockData.paramString = GetComponentInChildren<VariableEditor>().variableName;
            }
            else
            {
                blockData.paramInt = int.Parse(variableEditText.GetComponent<TMP_InputField>().text);
            }
        }
        if (messageReferenceDropDown)
        {
            blockData.paramString = messageReferenceDropDown.options[messageReferenceDropDown.value].text;
        }
        if (selectionDropDown)
        {
            blockData.paramInt = selectionDropDown.value;
        }
    }

    public override bool IsAttached()
    {
        return previousBlock != null;
    }

    public override void AttachBlock(GameObject attachObject)
    {
        if (attachObject.GetComponent<CodeBlockEditor>()) {
            attachObject.transform.position = nextAttachPoint.stickPoint.position;
            attachObject.gameObject.transform.SetParent(transform);

            attachObject.GetComponent<CodeBlockEditor>().previousBlock = GetComponent<CodeBlockEditor>();
            nextBlock = attachObject.GetComponent<CodeBlockEditor>();

            nextAttachPoint.Enable(false);
            attachObject.GetComponent<CodeBlockEditor>().previousAttachPoint.Enable(false);
        } else if(attachObject.GetComponent<VariableEditor>())
        {
            attachObject.transform.position = variableAttachPoint.stickPoint.position;
            attachObject.gameObject.transform.SetParent(transform);

            variableAttachPoint.Enable(false);
            variableEditText.SetActive(false);

            variableReference = attachObject.GetComponent<VariableEditor>();
            attachObject.GetComponent<VariableEditor>().attachPoint = variableAttachPoint;
        }
    }

    public override void DetachBlock(GameObject detachObject)
    {
        if (detachObject.GetComponent<CodeBlockEditor>())
        {
            detachObject.gameObject.transform.SetParent(codePanel.gameObject.transform);
            detachObject.GetComponent<CodeBlockEditor>().previousBlock = null;
            nextBlock = null;

            nextAttachPoint.Enable(true);
            detachObject.GetComponent<CodeBlockEditor>().previousAttachPoint.Enable(true);
        }
        else if (detachObject.GetComponent<VariableEditor>())
        {
            detachObject.gameObject.transform.SetParent(codePanel.gameObject.transform);

            variableAttachPoint.Enable(true);
            variableEditText.SetActive(true);

            variableReference = null;
            detachObject.GetComponent<VariableEditor>().attachPoint = null;
        }
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);

        //unlink from current object if its linked
        if (previousBlock != null)
        {
            previousBlock.DetachBlock(gameObject);         
        }
        else
        {
            //codePanel.blocks.Remove(this);
        }
    }

    public void OptionSelected()
    {

    }

    public CodeBlockEditor Last()
    {
        if (nextBlock)
        {
            return nextBlock.Last();
        } else
        {
            return this;
        }
    }
}
