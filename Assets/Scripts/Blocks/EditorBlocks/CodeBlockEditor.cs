using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CodeBlockEditor : BlockEditor
{
    public AttachPoint previousAttachPoint;
    public AttachPoint nextAttachPoint;
    public CodeBlockEditor previousBlock;
    public CodeBlockEditor nextBlock;

    public AttachPoint childAttachPoint;
    public CodeBlockEditor parentBlock;
    public CodeBlockEditor childBlock;

    public VariableAttachPoint[] variableAttachPoints;
    public ConditionAttachPoint conditionalAttachPoint;

    public TMP_Dropdown objectReferenceDropDown;
    public TMP_Dropdown variableReferenceDropDown;
    public TMP_Dropdown messageReferenceDropDown;
    public TMP_Dropdown tagReferenceDropDown;
    public TMP_Dropdown selectionDropDown;

    public Image colorVariable;

    private float resizableStartHeight;
    public float resizableOffsetHeight;
    public GameObject resizableBottom;

    private void Awake()
    {
        if (resizableBottom)
        {
            resizableStartHeight = resizableBottom.GetComponent<RectTransform>().rect.height;
        }
    }

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
        if (variableReferenceDropDown && variableReferenceDropDown.options.Count == 0)
        {
            variableReferenceDropDown.AddOptions(codePanel.blockBoard.GetVariables());
        }
        if (messageReferenceDropDown && messageReferenceDropDown.options.Count == 0)
        {
            messageReferenceDropDown.AddOptions(codePanel.blockBoard.GetMessages());
        }
        if (tagReferenceDropDown && tagReferenceDropDown.options.Count == 0)
        {
            tagReferenceDropDown.AddOptions(codePanel.blockBoard.GetTags());
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
        if (tagReferenceDropDown)
        {
            if (tagReferenceDropDown.options.Count > 0)
            {
                string selectedText = tagReferenceDropDown.options[tagReferenceDropDown.value].text;

                tagReferenceDropDown.ClearOptions();
                tagReferenceDropDown.AddOptions(codePanel.blockBoard.GetTags());

                TMP_Dropdown.OptionData selected = tagReferenceDropDown.options.Find(it => it.text == selectedText);
                tagReferenceDropDown.value = tagReferenceDropDown.options.IndexOf(selected);
            }
            else
            {
                tagReferenceDropDown.AddOptions(codePanel.blockBoard.GetTags());
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
        if (variableAttachPoints.Length > 0)
        {
            for (int i = 0; i < variableAttachPoints.Length; i++)
            {
                if (blockData.attachedBlocks.Length > i && blockData.attachedBlocks[i].blockType == "IntVariable") 
                {
                    variableAttachPoints[i].variableEditText.GetComponent<TMP_InputField>().text = "" + blockData.attachedBlocks[i].paramInt;
                } else if (blockData.attachedBlocks.Length > i && blockData.attachedBlocks[i].blockType == "StringVariable")
                {
                    variableAttachPoints[i].variableEditText.GetComponent<TMP_InputField>().text = "" + blockData.attachedBlocks[i].paramString;
                }
            }
        }
        if (variableReferenceDropDown)
        {
            TMP_Dropdown.OptionData selected = variableReferenceDropDown.options.Find(it => it.text == blockData.paramString);
            variableReferenceDropDown.value = variableReferenceDropDown.options.IndexOf(selected);
        }
        if (messageReferenceDropDown)
        {
            TMP_Dropdown.OptionData selected = messageReferenceDropDown.options.Find(it => it.text == blockData.paramString);
            messageReferenceDropDown.value = messageReferenceDropDown.options.IndexOf(selected);
        }
        if (tagReferenceDropDown)
        {
            TMP_Dropdown.OptionData selected = tagReferenceDropDown.options.Find(it => it.text == blockData.paramString);
            tagReferenceDropDown.value = tagReferenceDropDown.options.IndexOf(selected);
        }
        if (selectionDropDown)
        {
            selectionDropDown.value = blockData.paramInt;
        }
        if (colorVariable)
        {
            Color serializedColor;
            ColorUtility.TryParseHtmlString("#" + blockData.paramString, out serializedColor);
            colorVariable.color = serializedColor;
        }
    }

    public override void ExportData(ref BlockData blockData)
    {
        base.ExportData(ref blockData);
        if (objectReferenceDropDown)
        {
            blockData.objectReference = int.Parse(objectReferenceDropDown.options[objectReferenceDropDown.value].text);
        }
        if (variableAttachPoints.Length > 0)
        {
            List<BlockData> attachedBlocksList = new List<BlockData>();
            foreach (var variableAttachPoint in variableAttachPoints)
            {
                BlockData variableBlockData = new BlockData();
                if (variableAttachPoint.variableReference)
                {
                    variableAttachPoint.variableReference.ExportData(ref variableBlockData);
                } else
                {
                    int intValue;
                    if (int.TryParse(variableAttachPoint.variableEditText.GetComponent<TMP_InputField>().text, out intValue))
                    {
                        variableBlockData.blockType = "IntVariable";
                        variableBlockData.paramInt = intValue;
                    } else
                    {
                        variableBlockData.blockType = "StringVariable";
                        variableBlockData.paramString = variableAttachPoint.variableEditText.GetComponent<TMP_InputField>().text;
                    }
                }
                attachedBlocksList.Add(variableBlockData);
            }
            blockData.attachedBlocks = attachedBlocksList.ToArray();
        }
        if (conditionalAttachPoint)
        {
            List<BlockData> attachedBlocksList = new List<BlockData>();
            BlockData conditionBlockData = new BlockData();
            if (conditionalAttachPoint.conditionReference)
            {
                conditionalAttachPoint.conditionReference.ExportData(ref conditionBlockData);
            }
            attachedBlocksList.Add(conditionBlockData);
            blockData.attachedBlocks = attachedBlocksList.ToArray();
        }
        if (variableReferenceDropDown)
        {
            blockData.paramString = variableReferenceDropDown.options[variableReferenceDropDown.value].text;
        }
        if (messageReferenceDropDown)
        {
            blockData.paramString = messageReferenceDropDown.options[messageReferenceDropDown.value].text;
        }
        if (tagReferenceDropDown)
        {
            blockData.paramString = tagReferenceDropDown.options[tagReferenceDropDown.value].text;
        }
        if (selectionDropDown)
        {
            blockData.paramInt = selectionDropDown.value;
        }
        if (colorVariable)
        {
            blockData.paramString = ColorUtility.ToHtmlStringRGBA(colorVariable.color);
        }
        if (childAttachPoint && childBlock)
        {
            blockData.childBlocks = ExportBlockList(childBlock).ToArray();
        }

        if (nextBlock && !IsAttached())
        {
            blockData.blocks = ExportBlockList(nextBlock).ToArray();
        }
    }

    public List<BlockData> ExportBlockList(CodeBlockEditor codeBlock)
    {
        List<BlockData> attachedBlocksList = new List<BlockData>();
        while (codeBlock != null)
        {
            BlockData attachedBlockData = new BlockData();
            codeBlock.ExportData(ref attachedBlockData);
            attachedBlocksList.Add(attachedBlockData);

            codeBlock = codeBlock.nextBlock;
        }
        return attachedBlocksList;
    }

    public override bool IsAttached()
    {
        return previousBlock != null || parentBlock != null || attachPoint != null;
    }

    public override void AttachBlock(GameObject attachObject, AttachPoint attachPoint = null)
    {
        BlockGroup group = attachObject.GetComponent<BlockEditor>().blockGroup;

        if (group == BlockGroup.BLOCK) {
            attachObject.transform.position = nextAttachPoint.stickPoint.position;
            attachObject.gameObject.transform.SetParent(transform);

            attachObject.GetComponent<CodeBlockEditor>().previousBlock = GetComponent<CodeBlockEditor>();
            nextBlock = attachObject.GetComponent<CodeBlockEditor>();

            nextAttachPoint.Enable(false);
            attachObject.GetComponent<CodeBlockEditor>().previousAttachPoint.Enable(false);

            if (First().parentBlock)
            {
                First().parentBlock.GetComponent<CodeBlockEditor>().Resize(false, 0);
            }
        } else if(group == BlockGroup.VARIABLE)
        {
            VariableAttachPoint variableAttachPoint = attachPoint as VariableAttachPoint;
            attachObject.transform.position = variableAttachPoint.stickPoint.position;
            attachObject.gameObject.transform.SetParent(variableAttachPoint.stickPoint.transform.parent);

            attachObject.gameObject.transform.SetSiblingIndex(variableAttachPoint.stickPoint.transform.transform.GetSiblingIndex() + 1);

            variableAttachPoint.Enable(false);
            variableAttachPoint.variableEditText.SetActive(false);

            variableAttachPoint.variableReference = attachObject.GetComponent<BlockEditor>();
            attachObject.GetComponent<BlockEditor>().attachPoint = variableAttachPoint;


            //refresh parent
            if (this.attachPoint)
            {
                this.attachPoint.GetComponentInParent<CodeBlockEditor>().UpdateHorizontalSize();
                //LayoutRebuilder.ForceRebuildLayoutImmediate(variableAttachPoint.stickPoint.transform.parent.GetComponent<RectTransform>());
            }
        }
        else if (group == BlockGroup.CONDITION)
        {
            ConditionAttachPoint conditionAttachPoint = attachPoint as ConditionAttachPoint;
            attachObject.transform.position = conditionAttachPoint.stickPoint.position;
            attachObject.gameObject.transform.SetParent(conditionAttachPoint.stickPoint.transform.parent);

            attachObject.gameObject.transform.SetSiblingIndex(conditionAttachPoint.stickPoint.transform.transform.GetSiblingIndex() + 1);

            conditionAttachPoint.Enable(false);
            conditionAttachPoint.stickPoint.gameObject.SetActive(false);

            conditionAttachPoint.conditionReference = attachObject.GetComponent<BlockEditor>();
            attachObject.GetComponent<BlockEditor>().attachPoint = conditionAttachPoint;


            //refresh parent
            if (this.attachPoint)
            {
                this.attachPoint.GetComponentInParent<CodeBlockEditor>().UpdateHorizontalSize();
                //LayoutRebuilder.ForceRebuildLayoutImmediate(variableAttachPoint.stickPoint.transform.parent.GetComponent<RectTransform>());
            }
        }
    }

    private void UpdateHorizontalSize()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponentInChildren<HorizontalLayoutGroup>().gameObject.GetComponent<RectTransform>());
        if (this.attachPoint)
        {            
            this.attachPoint.GetComponentInParent<CodeBlockEditor>().UpdateHorizontalSize();
        }
    }

    public override void DetachBlock(GameObject detachObject)
    {
        BlockGroup group = detachObject.GetComponent<BlockEditor>().blockGroup;
        if (group == BlockGroup.BLOCK)
        {
            detachObject.gameObject.transform.SetParent(codePanel.gameObject.transform);
            detachObject.GetComponent<CodeBlockEditor>().previousBlock = null;
            nextBlock = null;

            nextAttachPoint.Enable(true);
            detachObject.GetComponent<CodeBlockEditor>().previousAttachPoint.Enable(true);
        }
        else if (group == BlockGroup.VARIABLE)
        {
            VariableAttachPoint variableAttachPoint = detachObject.GetComponent<BlockEditor>().attachPoint as VariableAttachPoint;
            detachObject.gameObject.transform.SetParent(codePanel.gameObject.transform);

            variableAttachPoint.Enable(true);
            variableAttachPoint.variableEditText.SetActive(true);

            variableAttachPoint.variableReference = null;
            detachObject.GetComponent<BlockEditor>().attachPoint = null;
        }
        else if (group == BlockGroup.CONDITION)
        {
            ConditionAttachPoint conditionAttachPoint = detachObject.GetComponent<BlockEditor>().attachPoint as ConditionAttachPoint;
            detachObject.gameObject.transform.SetParent(codePanel.gameObject.transform);

            conditionAttachPoint.Enable(true);
            conditionAttachPoint.stickPoint.gameObject.SetActive(true);

            conditionAttachPoint.conditionReference = null;
            detachObject.GetComponent<BlockEditor>().attachPoint = null;
        }
    }

    public override void AttachChildBlock(GameObject attachObject)
    {
        if (attachObject.GetComponent<CodeBlockEditor>())
        {
            if (childBlock)
            {
                //go to end of child objects
            }
            else
            {
                attachObject.transform.position = childAttachPoint.stickPoint.position;
                attachObject.gameObject.transform.SetParent(transform);

                attachObject.GetComponent<CodeBlockEditor>().parentBlock = GetComponent<CodeBlockEditor>();
                childBlock = attachObject.GetComponent<CodeBlockEditor>();
                childBlock.previousAttachPoint.Enable(false);

                childAttachPoint.Enable(false);
                //childAttachPoint.heighlight.SetActive(false);
            }

            Resize(false, 0);
        }
    }

    public override void DetachChildBlock(GameObject detachObject)
    {
        if (detachObject.GetComponent<CodeBlockEditor>())
        {
            detachObject.gameObject.transform.SetParent(codePanel.gameObject.transform);
            detachObject.GetComponent<CodeBlockEditor>().parentBlock = null;
            childBlock.previousAttachPoint.Enable(true);
            childBlock = null;

            childAttachPoint.Enable(true);
            Resize(false, 0);
        }
    }

    public void Resize(bool expand, float expandSize)
    {
        float childSize = 0;
        if (childBlock)
        {
            childSize = childBlock.CalculateSize();
        }
        /*CodeBlockEditor childIterator = childBlock.CalculateSize();
        while (childIterator)
        {
            childSize += childIterator.GetComponent<RectTransform>().rect.height;
            childIterator = childIterator.nextBlock;
        }*/

        if (childSize == 0 && !expand)
        {
            childSize = resizableStartHeight;
        } else
        {
            childSize += resizableOffsetHeight;
        }

        RectTransform resizable = resizableBottom.GetComponent<RectTransform>();
        if (expand)
        {
            resizable.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, childSize + expandSize);
            //resizable.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, resizableCurrentHeight + expandSize);
        }
        else
        {
            resizable.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, childSize);
            //resizable.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, resizableCurrentHeight);
        }

        CodeBlockEditor firstBlock = First();
        if (firstBlock.parentBlock)
        {
            firstBlock.parentBlock.Resize(expand, expandSize);
        }
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);

        //unlink from current object if its linked
        if (previousBlock != null)
        {
            previousBlock.DetachBlock(gameObject);
            CalculateOffset(eventData.position);
        }
        else
        {
            //codePanel.blocks.Remove(this);
        }

        if (parentBlock != null)
        {
            parentBlock.DetachChildBlock(gameObject);
            CalculateOffset(eventData.position);
        }
    }

    public void OptionSelected()
    {

    }

    public CodeBlockEditor First()
    {
        if (previousBlock)
        {
            return previousBlock.First();
        }
        else
        {
            return this;
        }
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

    public float CalculateSize()
    {
        float blockSize = 0;
        CodeBlockEditor iterator = this;
        while (iterator)
        {
            if (iterator.childAttachPoint && iterator.childBlock)
            {
                //Debug.Log("Size calc:" + iterator.childBlock.CalculateSize() + iterator.resizableOffsetHeight + 97);
                blockSize += iterator.childBlock.CalculateSize() + iterator.resizableOffsetHeight + 97;
            }
            else
            {
                blockSize += iterator.GetComponent<RectTransform>().rect.height;
            }
            iterator = iterator.nextBlock;
        }
        return blockSize;
    }

    public void OpenColorPicker()
    {
        codePanel.ToggleColorPicker(this);
    }
}
